using CHEExportsDataObjects;
using CHEExportsProto;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALOrderDetails : DALBase
    {
        [DataMember]
        public OrderDetails iOrderDetails { get; private set; }

        public DALOrderDetails(OrderDetails aOrderDetails) : base(aOrderDetails)
        {
            iOrderDetails = aOrderDetails;
        }

        public void CreateNewOrderDetails()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public void GenerateRefNo()
        {
            if (string.IsNullOrEmpty(iOrderDetails.order_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "ORDR" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iOrderDetails.order_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        public void setCusttomerAndWareHouseDetails()
        {
            if (iOrderDetails.customer_id > 0)
            {
                Customer lCustomer = CommonDAL.SelectDataFromDataBase<Customer>(new string[] { "CUSTOMER_ID" }, new string[] { "=" },
                    new object[] { iOrderDetails.customer_id }).FirstOrDefault();
                if(lCustomer!=null)
                iOrderDetails.customer_Details = string.Join("# ", lCustomer.customer_name, CommonDAL.ConcatAddressLine(lCustomer.address_line_1, lCustomer.address_line_2, lCustomer.address_line_3));
            }
            if (iOrderDetails.warehouse_id > 0)
            {
                WareHouse lWareHouse = CommonDAL.SelectDataFromDataBase<WareHouse>(new string[] { "WAREHOUSE_ID" }, new string[] { "=" },
           new object[] { iOrderDetails.warehouse_id }).FirstOrDefault();
                if(lWareHouse!=null)
                iOrderDetails.WareHouse_details = string.Join("# ", lWareHouse.warehouse_name, CommonDAL.ConcatAddressLine(lWareHouse.address_line_1, lWareHouse.address_line_2, lWareHouse.address_line_3));
            }
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Region_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iOrderDetails.status_description = lstSubConfig.Where(x => x.s_config_value == iOrderDetails.status_value).Select(x => x.s_config_description).FirstOrDefault();
            iOrderDetails.region_description = lstSubConfig.Where(x => x.s_config_value == iOrderDetails.region_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void SaveOrderDetails(string token)
        {
            try
            {
                //ValidateOrderDetailsSave();
                if (iOrderDetails != null && (iOrderDetails.errorMsg_lsit == null || iOrderDetails.errorMsg_lsit.Count == 0))
                {
                    if (iOrderDetails.order_detail_id == 0)
                    {
                        iOrderDetails.entered_date = DateTime.Now;
                        iOrderDetails.entered_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                        iOrderDetails.changed_date = DateTime.Now;
                        iOrderDetails.changed_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                        //GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iOrderDetails.changed_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                        iOrderDetails.changed_date = DateTime.Now;
                        UpdateOrderDetails(token);
                    }
                    setCusttomerAndWareHouseDetails();
                    SetAndCompletePreOrder();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void SetAndCompletePreOrder()
        {
            if(iOrderDetails.preorder_id>0)
            {
                Preorder lpreorder = new Preorder();
                lpreorder.preorder_id = iOrderDetails.preorder_id;
                DALPreorder lDALPreorder = new DALPreorder(lpreorder);
                lDALPreorder.SetAndCompletePreOrder();
            }
        }

        private void ValidateExistingDetails()
        {
            iOrderDetails.errorMsg_lsit = new List<string>();

            if (iOrderDetails.lstOrderDeliverySlipDetails != null && iOrderDetails.lstOrderDeliverySlipDetails.Count > 0)
            {
                foreach (OrderDeliverySlipDetails lOrderDeliverySlipDetails in iOrderDetails.lstOrderDeliverySlipDetails)
                {
                    OrderDeliverySlipDetails llOrderDeliverySlipDetails = CommonDAL.SelectDataFromDataBase<OrderDeliverySlipDetails>
                        (new string[] { "PRODUCT_ID", "ORDER_DELIVERY_SLIP_DETAIL_ID", "ORDER_DETAIL_ID" }, new string[] { "=", "!=", "=" }, new object[] { lOrderDeliverySlipDetails.product_id,lOrderDeliverySlipDetails.order_delivery_slip_detail_id
                        ,lOrderDeliverySlipDetails.order_detail_id}).FirstOrDefault();
                    if (llOrderDeliverySlipDetails != null && llOrderDeliverySlipDetails.order_delivery_slip_detail_id > 0)
                    {
                        Product lProduct = CommonDAL.SelectDataFromDataBase<Product>
                       (new string[] { "PRODUCT_ID" }, new string[] { "=" }, new object[] { lOrderDeliverySlipDetails.product_id }).FirstOrDefault();
                        if (lProduct != null)
                            iOrderDetails.errorMsg_lsit.Add("Selected " + lProduct.product_name + " Already exists for this order");
                    }
                }
            }
        }

        public void UpdateOrderDetails(string token)
        {
            try
            {
                ValidateExistingDetails();
                if (iOrderDetails != null && (iOrderDetails.errorMsg_lsit == null || iOrderDetails.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                    setCusttomerAndWareHouseDetails();
                    if (iOrderDetails.iInvoiceDetails != null && !string.IsNullOrEmpty(iOrderDetails.iInvoiceDetails.invoice_no))
                    {
                        DALInvoiceDetails lDALInvoiceDetails = new DALInvoiceDetails(iOrderDetails.iInvoiceDetails);
                        if (iOrderDetails.iInvoiceDetails.invoice_detail_id == 0)
                        {
                            iOrderDetails.iInvoiceDetails.changed_date = DateTime.Now;
                            iOrderDetails.iInvoiceDetails.changed_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                            iOrderDetails.iInvoiceDetails.entered_date = DateTime.Now;
                            iOrderDetails.iInvoiceDetails.entered_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                            lDALInvoiceDetails.SaveInvoiceDetails(token);
                        }
                        else if (iOrderDetails.iInvoiceDetails.invoice_detail_id > 0)
                        {
                            iOrderDetails.iInvoiceDetails.changed_date = DateTime.Now;
                            iOrderDetails.iInvoiceDetails.changed_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                            lDALInvoiceDetails.UpdateInvoiceDetails(token);
                        }
                    }
                    if (iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice != null && iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice.order_detail_id > 0)
                    {
                        DALOrderDeliverySlipDetailWithoutInvoice lDALOrderDeliverySlipDetailWithoutInvoice = new DALOrderDeliverySlipDetailWithoutInvoice(iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice);
                        if (iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice.order_delivery_slip_without_id == 0)
                        {
                            iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice.changed_date = DateTime.Now;
                            iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice.changed_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                            iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice.entered_date = DateTime.Now;
                            iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice.entered_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                            lDALOrderDeliverySlipDetailWithoutInvoice.SaveOrderDeliverySlipDetailWithoutInvoice(token);
                        }
                        else if (iOrderDetails.iInvoiceDetails.invoice_detail_id > 0)
                        {
                            iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice.changed_date = DateTime.Now;
                            iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice.changed_by = iOrderDetails.iLoggedInUserDetails.user_login_id;
                            lDALOrderDeliverySlipDetailWithoutInvoice.UpdateOrderDeliverySlipDetailWithoutInvoice(token);
                        }
                    }
                    if (iOrderDetails.lstOrderDeliverySlipDetails != null && iOrderDetails.lstOrderDeliverySlipDetails.Count > 0)
                    {
                        foreach (OrderDeliverySlipDetails lOrderDeliverySlipDetails in iOrderDetails.lstOrderDeliverySlipDetails)
                        {
                            DALOrderDeliverySlipDetails lDALOrderDeliverySlipDetails = new DALOrderDeliverySlipDetails(lOrderDeliverySlipDetails);
                            if (lOrderDeliverySlipDetails.order_delivery_slip_detail_id == 0)
                            {
                                lDALOrderDeliverySlipDetails.SaveOrderDeliverySlipDetails(token);
                            }
                            else if (lOrderDeliverySlipDetails.order_delivery_slip_detail_id > 0)
                            {
                                lDALOrderDeliverySlipDetails.UpdateOrderDeliverySlipDetails(token);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeleteOrderDetails(string token)
        {
            try
            {
                //ValidateOrderDetailsDelete();
                if (iOrderDetails != null && (iOrderDetails.errorMsg_lsit == null || iOrderDetails.errorMsg_lsit.Count == 0) && iOrderDetails.order_detail_id > 0)
                {
                    Delete(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void OpenOrderDetails(string token)
        {
            try
            {
                if (iOrderDetails != null && (iOrderDetails.errorMsg_lsit == null || iOrderDetails.errorMsg_lsit.Count == 0) && iOrderDetails.order_detail_id > 0)
                {
                    Open(token);
                    DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_LoadOrderDetailsChilds", new string[] { "@Order_Details_id" }, new object[] { iOrderDetails.order_detail_id });
                    if (lDataSet != null)
                    {
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                        {
                            iOrderDetails.iLRDetails = CommonDAL.SetListFromDataTable<LRDetails>(lDataSet.Tables[0]).FirstOrDefault();
                            DALLRDetails lDALLRDetails = new DALLRDetails(iOrderDetails.iLRDetails);
                            lDALLRDetails.SetDescription();
                        }
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[1] != null && lDataSet.Tables[1].Rows.Count > 0)
                        {
                            iOrderDetails.iInvoiceDetails = CommonDAL.SetListFromDataTable<InvoiceDetails>(lDataSet.Tables[1]).FirstOrDefault();
                            DALInvoiceDetails lDALInvoiceDetails = new DALInvoiceDetails(iOrderDetails.iInvoiceDetails);
                            lDALInvoiceDetails.SetVendorAndDescriptionDetails();
                        }
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[2] != null && lDataSet.Tables[2].Rows.Count > 0)
                        {
                            iOrderDetails.lstOrderDeliverySlipDetails = CommonDAL.SetListFromDataTable<OrderDeliverySlipDetails>(lDataSet.Tables[2]).OrderByDescending(x => x.order_delivery_slip_detail_id).ToList();
                            foreach (OrderDeliverySlipDetails lOrderDeliverySlipDetails in iOrderDetails.lstOrderDeliverySlipDetails)
                            {
                                DALOrderDeliverySlipDetails lDALOrderDeliverySlipDetails = new DALOrderDeliverySlipDetails(lOrderDeliverySlipDetails);
                                lDALOrderDeliverySlipDetails.SetDescriptionAndChildDetails();
                            }
                        }
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[3] != null && lDataSet.Tables[3].Rows.Count > 0)
                        {
                            iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice = CommonDAL.SetListFromDataTable<OrderDeliverySlipDetailWithoutInvoice>(lDataSet.Tables[3]).FirstOrDefault();
                            DALOrderDeliverySlipDetailWithoutInvoice lDALOrderDeliverySlipDetailWithoutInvoice = new DALOrderDeliverySlipDetailWithoutInvoice(iOrderDetails.iOrderDeliverySlipDetailWithoutInvoice);
                            lDALOrderDeliverySlipDetailWithoutInvoice.SetDescription();
                        }
                    }
                    setCusttomerAndWareHouseDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public void GetOrderDetailsBasedOnValues(RepackingDetail lRepackingDetail, string token, Common aCommon)
        {
            try
            {
                List<InvoiceDetails> lstInvoiceDetails = new List<InvoiceDetails>();
                List<OrderDeliverySlipDetails> lstOrderDeliverySlipDetails = new List<OrderDeliverySlipDetails>();
                List<OrderDeliverySlipDetailWithoutInvoice> lstOrderDeliverySlipDetailWithoutInvoice = new List<OrderDeliverySlipDetailWithoutInvoice>();
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_LoadOrderDetailsBasedOnRepakingDetails", new string[] { "@customer_id", "@fromdate", "@todate" },
                    new object[] { aCommon.long1, aCommon.string1, aCommon.string2 });
                if (lDataSet != null)
                {
                    if (lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                    {
                        lRepackingDetail.lstOrderDetails = CommonDAL.SetListFromDataTable<OrderDetails>(lDataSet.Tables[0]).OrderByDescending(x => x.order_detail_id).ToList();
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[1] != null && lDataSet.Tables[1].Rows.Count > 0)
                        {
                            lstInvoiceDetails = CommonDAL.SetListFromDataTable<InvoiceDetails>(lDataSet.Tables[1]).OrderByDescending(x => x.invoice_detail_id).ToList();
                        }
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[2] != null && lDataSet.Tables[2].Rows.Count > 0)
                        {
                            lstOrderDeliverySlipDetails = CommonDAL.SetListFromDataTable<OrderDeliverySlipDetails>(lDataSet.Tables[2]).OrderByDescending(x => x.order_delivery_slip_detail_id).ToList();
                        }
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[3] != null && lDataSet.Tables[3].Rows.Count > 0)
                        {
                            lstOrderDeliverySlipDetailWithoutInvoice = CommonDAL.SetListFromDataTable<OrderDeliverySlipDetailWithoutInvoice>(lDataSet.Tables[3]).OrderByDescending(x => x.order_delivery_slip_without_id).ToList();
                        }
                        foreach (OrderDetails item in lRepackingDetail.lstOrderDetails)
                        {
                            lRepackingDetail.lOrderDetails = setCusttomerAndWareHouseDetails(item);
                            if (lstInvoiceDetails != null && lstInvoiceDetails.Count > 0)
                            {
                                item.iInvoiceDetails = lstInvoiceDetails.Where(x => x.order_detail_id == item.order_detail_id).FirstOrDefault();
                                if (item.iInvoiceDetails != null)
                                {
                                    DALInvoiceDetails lDALInvoiceDetails = new DALInvoiceDetails(item.iInvoiceDetails);
                                    lDALInvoiceDetails.SetVendorAndDescriptionDetails();
                                }
                            }
                            if (lstOrderDeliverySlipDetailWithoutInvoice != null && lstOrderDeliverySlipDetailWithoutInvoice.Count > 0)
                            {
                                item.iOrderDeliverySlipDetailWithoutInvoice = lstOrderDeliverySlipDetailWithoutInvoice.Where(x => x.order_detail_id == item.order_detail_id).FirstOrDefault();
                                    if (item.iOrderDeliverySlipDetailWithoutInvoice != null)
                                {
                                    DALOrderDeliverySlipDetailWithoutInvoice lDALOrderDeliverySlipDetailWithoutInvoice = new DALOrderDeliverySlipDetailWithoutInvoice(item.iOrderDeliverySlipDetailWithoutInvoice);
                                    lDALOrderDeliverySlipDetailWithoutInvoice.SetDescription();
                                }
                            }
                            if (lstOrderDeliverySlipDetails != null && lstOrderDeliverySlipDetails.Count > 0)
                            {
                                item.lstOrderDeliverySlipDetails = lstOrderDeliverySlipDetails.Where(x => x.order_detail_id == item.order_detail_id).OrderByDescending(x => x.order_delivery_slip_detail_id).ToList();
                                if (item.lstOrderDeliverySlipDetails != null)
                                {
                                    foreach (OrderDeliverySlipDetails lOrderDeliverySlipDetails in item.lstOrderDeliverySlipDetails)
                                    {
                                        DALOrderDeliverySlipDetails lDALOrderDeliverySlipDetails = new DALOrderDeliverySlipDetails(lOrderDeliverySlipDetails);
                                        lDALOrderDeliverySlipDetails.SetDescriptionAndChildDetails();
                                    }
                                }
                            }
                        }
                        setCusttomerAndWareHouseDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            //return lRepackingDetail;
        }
        public void GetOrderDetailsForRepaking(RepackingDetail lRepackingDetail, string token)
        {

            try
            {
                if (lRepackingDetail.lOrderDetails.order_detail_id > 0)
                {
                    lRepackingDetail.errorMsg_lsit = new List<string>();

                    Open(token);
                    DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_LoadOrderDetailsChilds", new string[] { "@Order_Details_id" }, new object[] { lRepackingDetail.lOrderDetails.order_detail_id });
                    if (lDataSet != null)
                    {
                        //if (lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                        //{
                        //    lRepackingDetail.lOrderDetails.iLRDetails = CommonDAL.SetListFromDataTable<LRDetails>(lDataSet.Tables[0]).FirstOrDefault();
                        //    DALLRDetails lDALLRDetails = new DALLRDetails(lRepackingDetail.lOrderDetails.iLRDetails);
                        //    lDALLRDetails.SetDescription();
                        //}
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[4] != null && lDataSet.Tables[4].Rows.Count > 0 && lRepackingDetail.repacking_detail_id == 0)
                        {

                            lRepackingDetail.errorMsg_lsit.Add("Selected " + lRepackingDetail.lOrderDetails.order_ref_no + " Already repacked.");
                            return;
                        }
                        else
                        {
                            if (lDataSet.Tables.Count > 0 && lDataSet.Tables[1] != null && lDataSet.Tables[1].Rows.Count > 0)
                            {
                                lRepackingDetail.lOrderDetails.iInvoiceDetails = CommonDAL.SetListFromDataTable<InvoiceDetails>(lDataSet.Tables[1]).FirstOrDefault();
                                DALInvoiceDetails lDALInvoiceDetails = new DALInvoiceDetails(lRepackingDetail.lOrderDetails.iInvoiceDetails);
                                lDALInvoiceDetails.SetVendorAndDescriptionDetails();
                            }
                            if (lDataSet.Tables.Count > 0 && lDataSet.Tables[2] != null && lDataSet.Tables[2].Rows.Count > 0)
                            {
                                lRepackingDetail.lOrderDetails.lstOrderDeliverySlipDetails = CommonDAL.SetListFromDataTable<OrderDeliverySlipDetails>(lDataSet.Tables[2]).OrderByDescending(x => x.order_delivery_slip_detail_id).ToList();
                                foreach (OrderDeliverySlipDetails lOrderDeliverySlipDetails in lRepackingDetail.lOrderDetails.lstOrderDeliverySlipDetails)
                                {
                                    DALOrderDeliverySlipDetails lDALOrderDeliverySlipDetails = new DALOrderDeliverySlipDetails(lOrderDeliverySlipDetails);
                                    lDALOrderDeliverySlipDetails.SetDescriptionAndChildDetails();
                                }
                            }
                            if (lDataSet.Tables.Count > 0 && lDataSet.Tables[3] != null && lDataSet.Tables[3].Rows.Count > 0)
                            {
                                lRepackingDetail.lOrderDetails.iOrderDeliverySlipDetailWithoutInvoice = CommonDAL.SetListFromDataTable<OrderDeliverySlipDetailWithoutInvoice>(lDataSet.Tables[3]).FirstOrDefault();
                                DALOrderDeliverySlipDetailWithoutInvoice lDALOrderDeliverySlipDetailWithoutInvoice = new DALOrderDeliverySlipDetailWithoutInvoice(lRepackingDetail.lOrderDetails.iOrderDeliverySlipDetailWithoutInvoice);
                                lDALOrderDeliverySlipDetailWithoutInvoice.SetDescription();
                            }
                        }

                        


                    }
                    lRepackingDetail.lOrderDetails = lRepackingDetail.lOrderDetails;
                    lRepackingDetail.lOrderDetails = setCusttomerAndWareHouseDetails(lRepackingDetail.lOrderDetails);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            //return lRepackingDetail;
        }
        public OrderDetails setCusttomerAndWareHouseDetails(OrderDetails lOrderDetails)
        {
            try
            {
                if (lOrderDetails.customer_id > 0)
                {
                    Customer lCustomer = CommonDAL.SelectDataFromDataBase<Customer>(new string[] { "CUSTOMER_ID" }, new string[] { "=" },
                        new object[] { lOrderDetails.customer_id }).FirstOrDefault();
                    if (lCustomer != null)
                    {
                        lOrderDetails.customer_Details = string.Join("# ", lCustomer.customer_name, CommonDAL.ConcatAddressLine(lCustomer.address_line_1, lCustomer.address_line_2, lCustomer.address_line_3));
                    }
                }
                if (lOrderDetails.warehouse_id > 0)
                {
                    WareHouse lWareHouse = CommonDAL.SelectDataFromDataBase<WareHouse>(new string[] { "WAREHOUSE_ID" }, new string[] { "=" },
               new object[] { lOrderDetails.warehouse_id }).FirstOrDefault();
                    if (lWareHouse != null)
                    {
                        lOrderDetails.WareHouse_details = string.Join("# ", lWareHouse.warehouse_name, CommonDAL.ConcatAddressLine(lWareHouse.address_line_1, lWareHouse.address_line_2, lWareHouse.address_line_3));
                    }
                }
                string config_ids = Constants.Application.Status_id + "," + Constants.Application.Region_id;
                List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                lOrderDetails.status_description = lstSubConfig.Where(x => x.s_config_value == lOrderDetails.status_value).Select(x => x.s_config_description).FirstOrDefault();
                lOrderDetails.region_description = lstSubConfig.Where(x => x.s_config_value == lOrderDetails.region_value).Select(x => x.s_config_description).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return lOrderDetails;

        }
        public SearchResultBase<OrderSearchResultset> SearchOrderDetails(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<OrderSearchResultset> searchResult = new SearchResultBase<OrderSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchOrder", new string[]
                { "@SearchParam","@PageNumber","@RowsPerPage","@RegionValue","@CustomerType"},new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage,aprotoSearchParams.RegionValue,aprotoSearchParams.CustomerType});
                if(lDataSet!=null)
                { 
                searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<OrderSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.order_detail_id).ToList();
                    searchResult.total_count = Convert.ToInt32(lDataSet.Tables[1].Rows[0][0]);
                    searchResult.page_number  =Convert.ToInt32( lDataSet.Tables[1].Rows[0][1]);
                   searchResult.page_size = Convert.ToInt32(lDataSet.Tables[1].Rows[0][2]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return searchResult;
        }
    }
}



