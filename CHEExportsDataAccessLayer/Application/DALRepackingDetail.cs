
using CHEExportsDataObjects;
using CHEExportsProto;
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
    public class DALRepackingDetail : DALBase
    {
        [DataMember]
        public RepackingDetail iRepackingDetail { get; private set; }

        public DALRepackingDetail(RepackingDetail aRepackingDetail) : base(aRepackingDetail)
        {
            iRepackingDetail = aRepackingDetail;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.repaking_status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iRepackingDetail.status_description = lstSubConfig.Where(x => x.s_config_value == iRepackingDetail.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if (iRepackingDetail.customer_id > 0)
            {
                iRepackingDetail.iCustomer = CommonDAL.SelectDataFromDataBase<Customer>
                         (new string[] { "CUSTOMER_ID" }, new string[] { "=" }, new object[] { iRepackingDetail.customer_id }).FirstOrDefault();
            }
        }
        public void CreateNewRepackingDetail()
        {
            try
            {
                iRepackingDetail.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void CheckOrderRepackingStatus()
        {

            try
            {
                if (iRepackingDetail.order_id > 0)
                {
                    iRepackingDetail.errorMsg_lsit = new List<string>();

                    DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_LoadOrderDetailsChilds", new string[] { "@Order_Details_id" }, new object[] { iRepackingDetail.order_id });
                    if (lDataSet != null)
                    {
                        if (lDataSet.Tables.Count > 0 && lDataSet.Tables[4] != null && lDataSet.Tables[4].Rows.Count > 0 && iRepackingDetail.repacking_detail_id == 0)
                        {

                            iRepackingDetail.errorMsg_lsit.Add("Selected " + iRepackingDetail.lOrderDetails.order_ref_no + " Already repacked.");
                            return;
                        }
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

        public void SaveRepackingDetail(string token)
        {
            try
            {
                //ValidateRepackingDetailSave();
                iRepackingDetail.test_flag = false;
                List<RepackingListDetail> lstList = new List<RepackingListDetail>();

                if (iRepackingDetail != null && (iRepackingDetail.errorMsg_lsit == null || iRepackingDetail.errorMsg_lsit.Count == 0))
                {
                    
                    if (iRepackingDetail.lstRepackingListDetail != null && iRepackingDetail.lstRepackingListDetail.Count > 0)
                    {
                        lstList= GetProductQuantityAndValidation(token);
                        
                        if(iRepackingDetail.test_flag == false)
                        {
                            iRepackingDetail.lstRepackingListDetail = lstList;
                            if (iRepackingDetail.repacking_detail_id == 0)
                            {
                                iRepackingDetail.changed_date = DateTime.Now;
                                iRepackingDetail.entered_date = DateTime.Now;
                                iRepackingDetail.entered_by = iRepackingDetail.iLoggedInUserDetails.user_login_id;
                                iRepackingDetail.changed_by = iRepackingDetail.iLoggedInUserDetails.user_login_id;
                                Save(token);
                            }
                            else
                            {
                                iRepackingDetail.changed_date = DateTime.Now;
                                iRepackingDetail.changed_by = iRepackingDetail.iLoggedInUserDetails.user_login_id;
                                Update(token);
                            }

                            foreach (RepackingListDetail lRepackingListDetail in iRepackingDetail.lstRepackingListDetail)
                            {
                                if (lRepackingListDetail.repacking_list_detail_id == 0)
                                {
                                    lRepackingListDetail.repacking_detail_id = iRepackingDetail.repacking_detail_id;
                                    DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                                    lDALRepackingListDetail.SaveRepackingListDetail(token);
                                }
                                else
                                {
                                    DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                                    lDALRepackingListDetail.UpdateRepackingListDetail(token);
                                }
                            }
                        }
                        
                    }
                    Setdescription();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private List<RepackingListDetail> GetProductQuantityAndValidation(string token)
        {
            List<RepackingListDetail> lstRepackingListDetail = new List<RepackingListDetail>();
            List<RepackingListDetail> Previous_lstRepackingListDetail = new List<RepackingListDetail>();
            try
            {
                List<OrderDeliverySlipDetails> lstOrderDeliverySlipDetails = CommonDAL.SelectDataFromDataBase<OrderDeliverySlipDetails>
                           (new string[] { "ORDER_DETAIL_ID" }, new string[] { "=" }, new object[] { iRepackingDetail.order_id }).ToList();
                if (lstOrderDeliverySlipDetails != null && lstOrderDeliverySlipDetails.Count > 0)
                {
                    foreach (OrderDeliverySlipDetails lOrderDeliverySlipDetails in lstOrderDeliverySlipDetails)
                    {
                        if (lOrderDeliverySlipDetails.product_id > 0)
                        {
                            List<RepackingListDetail> lstRepackingListDetail1 = iRepackingDetail.lstRepackingListDetail.Where(x => x.product_id == lOrderDeliverySlipDetails.product_id).ToList();
                            if (lstRepackingListDetail1 != null && lstRepackingListDetail1.Count > 0)
                            {
                                decimal total_previous_quantity = 0M;
                                decimal total_quantity = lstRepackingListDetail1.Sum(x => x.quantity);
                                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_CheckOrderAlreadyRepackedorNot", new string[] { "@orderId", "@productId", "@Repackingid" }, new object[]
                                { iRepackingDetail.order_id, lOrderDeliverySlipDetails.product_id ,iRepackingDetail.repacking_detail_id });
                                if (lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                                {
                                    Previous_lstRepackingListDetail = CommonDAL.SetListFromDataTable<RepackingListDetail>(lDataSet.Tables[0]).ToList();
                                }
                                if (Previous_lstRepackingListDetail != null && Previous_lstRepackingListDetail.Count > 0)
                                {
                                    total_previous_quantity = Previous_lstRepackingListDetail.Sum(x => x.quantity);
                                    total_quantity = total_quantity + total_previous_quantity;
                                }
                                if (total_quantity == lOrderDeliverySlipDetails.quantity)
                                {
                                    foreach (RepackingListDetail lRepackingListDetail in lstRepackingListDetail1)
                                    {
                                        lRepackingListDetail.is_verifird = "Y";
                                    }
                                }
                                else
                                {
                                    foreach (RepackingListDetail lRepackingListDetail in lstRepackingListDetail1)
                                    {
                                        lRepackingListDetail.is_verifird = "N";
                                    }
                                    lOrderDeliverySlipDetails.lProduct = CommonDAL.SelectDataFromDataBase<Product>
                                    (new string[] { "PRODUCT_ID" }, new string[] { "=" }, new object[] { lOrderDeliverySlipDetails.product_id }).FirstOrDefault();
                                    
                                    if(iRepackingDetail.infoMsgList == null || iRepackingDetail.infoMsgList.Count == 0)
                                    {
                                        iRepackingDetail.infoMsgList = new List<string>();
                                    }
                                    if(lOrderDeliverySlipDetails.lProduct!=null)
                                    iRepackingDetail.infoMsgList.Add("Total quantity mismatched for this " + lOrderDeliverySlipDetails.lProduct.product_name + ".");
                                }
                                lstRepackingListDetail.AddRange(lstRepackingListDetail1);
                            }
                            else
                            {
                                if (iRepackingDetail.infoMsgList == null || iRepackingDetail.infoMsgList.Count == 0)
                                {
                                    iRepackingDetail.infoMsgList = new List<string>();
                                }
                                if(lOrderDeliverySlipDetails.product_id != null)
                                {
                                    Product lproduct = new Product();
                                    lproduct.product_id = lOrderDeliverySlipDetails.product_id;
                                    DALProduct lDALProduct = new DALProduct(lproduct);
                                    lDALProduct.OpenProduct(token);
                                    if (lproduct != null)
                                    {
                                        iRepackingDetail.test_flag = true;
                                        iRepackingDetail.infoMsgList.Add(lproduct.product_name + " - this product is missing in repacking list. ");
                                        
                                    }
                                }                               

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstRepackingListDetail;
        }

        private void ValidateRepackingDetailSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateApprovedRepackingDetail(string token)
        {
            try
            {
                if (iRepackingDetail.repacking_detail_id > 0)
                {
                    if (iRepackingDetail.lstRepackingListDetail != null && iRepackingDetail.lstRepackingListDetail.Count > 0)
                    {
                        //iRepackingDetail.lstRepackingListDetail = GetProductQuantityAndValidation();
                        foreach (RepackingListDetail lRepackingListDetail in iRepackingDetail.lstRepackingListDetail)
                        {
                            if (lRepackingListDetail.repacking_list_detail_id > 0)
                            {
                                if (lRepackingListDetail.status_value == Constants.Application.Approved_status)
                                {
                                    DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                                    lDALRepackingListDetail.UpdateRepackingListDetail(token);
                                    UpdateFinalRepaking(lRepackingListDetail, token);
                                }
                            }
                        }
                    }
                    chechAllItemAreApprovedOrNot(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void chechAllItemAreApprovedOrNot(string token)
        {
            RepackingListDetail lRepackingListDetail = CommonDAL.SelectDataFromDataBase<RepackingListDetail>
           (new string[] { "REPACKING_DETAIL_ID", "STATUS_VALUE" }, new string[] { "=", "=" }, new object[]
           { iRepackingDetail.repacking_detail_id,Constants.Application.Pending_Approved_status }).FirstOrDefault();
            if (lRepackingListDetail == null || lRepackingListDetail.repacking_list_detail_id == 0)
            {
                iRepackingDetail.status_value = Constants.Application.Approved_status;
                Update(token);
                Setdescription();
            }
        }

        public void UpdateRepackingDetail(string token)
        {
            try
            {
                iRepackingDetail.test_flag = false;
                //ValidateRepackingDetailSave();

                List<RepackingListDetail> lstList = new List<RepackingListDetail>();
                if (iRepackingDetail.repacking_detail_id > 0)
                {
                    if (iRepackingDetail.lstRepackingListDetail != null && iRepackingDetail.lstRepackingListDetail.Count > 0)
                    {
                        lstList = GetProductQuantityAndValidation(token);
                        if (iRepackingDetail.test_flag == false)
                        {

                            iRepackingDetail.lstRepackingListDetail = lstList;
                            Update(token);
                            Setdescription();
                            foreach (RepackingListDetail lRepackingListDetail in iRepackingDetail.lstRepackingListDetail)
                            {
                                if (lRepackingListDetail.repacking_list_detail_id == 0)
                                {
                                    lRepackingListDetail.repacking_detail_id = iRepackingDetail.repacking_detail_id;
                                    DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                                    lDALRepackingListDetail.SaveRepackingListDetail(token);
                                }
                                else
                                {
                                    DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                                    lDALRepackingListDetail.UpdateRepackingListDetail(token);
                                }
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

        public void UpdateFinalRepaking(RepackingListDetail lRepackingListDetail, string token)
        {
            FinalPackingDetail lFinalPackingDetail1 = CommonDAL.SelectDataFromDataBase<FinalPackingDetail>(new string[] { "REPACKING_DETAIL_ID", "PACKAGE_NO" }, new string[] { "=","=" },
           new object[] { lRepackingListDetail.repacking_detail_id,lRepackingListDetail.package_no}).FirstOrDefault();
            if (lFinalPackingDetail1 == null || lFinalPackingDetail1.final_packing_detail_id==0)
            {
                FinalPackingDetail lFinalPackingDetail = new FinalPackingDetail();
                lFinalPackingDetail.repacking_detail_id = lRepackingListDetail.repacking_detail_id;
                lFinalPackingDetail.order_id = iRepackingDetail.order_id;
                lFinalPackingDetail.product_id = lRepackingListDetail.product_id;
                lFinalPackingDetail.package_type_id = lRepackingListDetail.package_type_id;
                lFinalPackingDetail.package_no = lRepackingListDetail.package_no;
                lFinalPackingDetail.package_type_value = lRepackingListDetail.package_type_value;
                lFinalPackingDetail.quantity = lRepackingListDetail.quantity;
                lFinalPackingDetail.unit_type_id = lRepackingListDetail.unit_type_id;
                lFinalPackingDetail.unit_type_value = lRepackingListDetail.unit_type_value;
                lFinalPackingDetail.net_weight = lRepackingListDetail.net_weight;
                lFinalPackingDetail.gross_weight = lRepackingListDetail.gross_weight;
                lFinalPackingDetail.status_id = Constants.Application.Stock_status_id;
                lFinalPackingDetail.status_value = Constants.Application.InStock;
                DALFinalPackingDetail lDALFinalPackingDetail = new DALFinalPackingDetail(lFinalPackingDetail);
                lDALFinalPackingDetail.SaveFinalPackingDetail(token);
            }
        }
        public void DeleteRepackingDetail(string token)
        {
            try
            {
                //ValidateRepackingDetailDelete();
                if (iRepackingDetail != null && (iRepackingDetail.errorMsg_lsit == null || iRepackingDetail.errorMsg_lsit.Count == 0) && iRepackingDetail.repacking_detail_id > 0)
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
        public RepackingListDetail SetRepakingDescription(RepackingListDetail lRepackingListDetail)
        {
            string config_ids = Constants.Application.package_type_id + "," + Constants.Application.Unit_type_id + "," + Constants.Application.repaking_status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            lRepackingListDetail.status_description = lstSubConfig.Where(x => x.s_config_value == lRepackingListDetail.status_value).Select(x => x.s_config_description).FirstOrDefault();
            lRepackingListDetail.package_type_description = lstSubConfig.Where(x => x.s_config_value == lRepackingListDetail.package_type_value).Select(x => x.s_config_description).FirstOrDefault();
            lRepackingListDetail.unit_type_description = lstSubConfig.Where(x => x.s_config_value == lRepackingListDetail.unit_type_value).Select(x => x.s_config_description).FirstOrDefault();
            return lRepackingListDetail;
        }
        public void OpenRepackingDetail(string token)
        {
            try
            {
                if (iRepackingDetail != null && (iRepackingDetail.errorMsg_lsit == null || iRepackingDetail.errorMsg_lsit.Count == 0) && iRepackingDetail.repacking_detail_id > 0)
                {
                    Open(token);
                    Setdescription();
                    GetOrderDetails();
                    iRepackingDetail.lstRepackingListDetail = CommonDAL.SelectDataFromDataBase<RepackingListDetail>(new string[] { "REPACKING_DETAIL_ID" }, new string[] { "=" },
                   new object[] { iRepackingDetail.repacking_detail_id }).OrderByDescending(x => x.repacking_list_detail_id).ToList();
                    foreach (RepackingListDetail lRepackingListDetail in iRepackingDetail.lstRepackingListDetail)
                    {
                        SetRepakingDescription(lRepackingListDetail);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public void GetOrderDetails()
        {
            try
            {
                iRepackingDetail.status_value = Constants.Application.Pending_Approved_status;
                iRepackingDetail.lOrderDetails.order_detail_id = iRepackingDetail.order_id;
                DALOrderDetails lDALOrderDetails = new DALOrderDetails(iRepackingDetail.lOrderDetails);
                lDALOrderDetails.GetOrderDetailsForRepaking(iRepackingDetail, "");
                Setdescription();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void GetOrderDetailsBasedOnValues(Common aCommon)
        {
            try
            {
                iRepackingDetail.status_value = Constants.Application.Pending_Approved_status;
                OrderDetails lOrderDetails = new OrderDetails();
                DALOrderDetails lDALOrderDetails = new DALOrderDetails(lOrderDetails);
                lDALOrderDetails.GetOrderDetailsBasedOnValues(iRepackingDetail, "", aCommon);
                Setdescription();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public SearchResultBase<RepakingDetailsSearchResultset> SearchRepackingDetail(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<RepakingDetailsSearchResultset> searchResult = new SearchResultBase<RepakingDetailsSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchRepackingDetail", new string[]
                { "@SearchParam","@PageNumber","@RowsPerPage","@RegionValue","@CustomerType"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber,aprotoSearchParams.RowPerPage,aprotoSearchParams.RegionValue,aprotoSearchParams.CustomerType});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<RepakingDetailsSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.repacking_detail_id).ToList(); ;
                    searchResult.total_count = Convert.ToInt32(lDataSet.Tables[1].Rows[0][0]);
                    searchResult.page_number = Convert.ToInt32(lDataSet.Tables[1].Rows[0][1]);
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




