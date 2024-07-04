
using CHEExportsDataObjects;
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
    public class DALExportInvoiceDetail : DALBase
    {
        [DataMember]
        public ExportInvoiceDetail iExportInvoiceDetail { get; private set; }

        public DALExportInvoiceDetail(ExportInvoiceDetail aExportInvoiceDetail) : base(aExportInvoiceDetail)
        {
            iExportInvoiceDetail = aExportInvoiceDetail;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Unit_type_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iExportInvoiceDetail.unit_type_description = lstSubConfig.Where(x => x.s_config_value == iExportInvoiceDetail.unit_type_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewExportInvoiceDetail()
        {
            try
            {
                //iExportInvoiceDetail.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        

        public void SaveExportInvoiceDetail(string token)
        {
            try
            {
                //ValidateExportInvoiceDetailSave();
                iExportInvoiceDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                if (iExportInvoiceDetail.export_invoice_detail_id==0)
                {
                    iExportInvoiceDetail.changed_date = DateTime.Now;
                    iExportInvoiceDetail.entered_date = DateTime.Now;
                    iExportInvoiceDetail.entered_by = iExportInvoiceDetail.iLoggedInUserDetails.user_login_id;
                    iExportInvoiceDetail.changed_by = iExportInvoiceDetail.iLoggedInUserDetails.user_login_id;
                    Save(token);
                }
                else
                {
                    iExportInvoiceDetail.changed_date = DateTime.Now;
                    iExportInvoiceDetail.entered_date = DateTime.Now;
                    iExportInvoiceDetail.entered_by = iExportInvoiceDetail.iLoggedInUserDetails.user_login_id;
                    iExportInvoiceDetail.changed_by = iExportInvoiceDetail.iLoggedInUserDetails.user_login_id;
                    Update(token);
                }
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void ValidateExportInvoiceDetailSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateExportInvoiceDetail(string token)
        {
            try
            {
                //ValidateExportInvoiceDetailSave();
                if (iExportInvoiceDetail != null && (iExportInvoiceDetail.errorMsg_lsit == null || iExportInvoiceDetail.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                    Setdescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public List<ExportInvoiceDetail> DeleteExportInvoiceDetail(string token)
        {
            List<ExportInvoiceDetail > lstExportInvoiceDetail=new List<ExportInvoiceDetail>();
            try
            {
                //ValidateExportInvoiceDetailDelete();
                if (iExportInvoiceDetail != null && (iExportInvoiceDetail.errorMsg_lsit == null || iExportInvoiceDetail.errorMsg_lsit.Count == 0) && iExportInvoiceDetail.export_invoice_detail_id > 0)
                {
                    Delete(token);
                }
                lstExportInvoiceDetail= GetListofExportInvoiceDetail();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return lstExportInvoiceDetail;
        }
        private List<ExportInvoiceDetail> GetListofExportInvoiceDetail()
        {
            List<ExportInvoiceDetail> lstExportInvoiceDetail = new List<ExportInvoiceDetail>();
            if (iExportInvoiceDetail.export_invoice_id > 0)
            {
                lstExportInvoiceDetail = CommonDAL.SelectDataFromDataBase<ExportInvoiceDetail>(new string[] { "EXPORT_INVOICE_ID" }, new string[] { "=" },
                    new object[] { iExportInvoiceDetail.export_invoice_id }).ToList();
                foreach (ExportInvoiceDetail lExportInvoiceDetail in lstExportInvoiceDetail)
                {
                    Setdescription(lExportInvoiceDetail);
                }
            }
            return lstExportInvoiceDetail;
        }


        public void OpenExportInvoiceDetail(string token)
        {
            try
            {
                if (iExportInvoiceDetail != null && (iExportInvoiceDetail.errorMsg_lsit == null || iExportInvoiceDetail.errorMsg_lsit.Count == 0) && iExportInvoiceDetail.export_invoice_detail_id > 0)
                {
                    Open(token);
                    Setdescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public ExportInvoiceDetail Setdescription(ExportInvoiceDetail lExportInvoiceDetail)
        {
            string config_ids = Constants.Application.Unit_type_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            lExportInvoiceDetail.unit_type_description = lstSubConfig.Where(x => x.s_config_value == lExportInvoiceDetail.unit_type_value).Select(x => x.s_config_description).FirstOrDefault();
            return lExportInvoiceDetail;
        }
        public List<ExportInvoiceDetail> GetInStockInvoiceDetailBasedPakgNO(string pakageno)
        {
            List<ExportInvoiceDetail> lstExportInvoiceDetail = new List<ExportInvoiceDetail>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_GetInStockInvoiceDetailBasedPakgNO", new string[] { "@pakageno" }, new object[] { pakageno });
                if (lDataSet != null)
                {
                    if (lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FinalPackingDetail> lstFinalPackingDetail = CommonDAL.SetListFromDataTable<FinalPackingDetail>(lDataSet.Tables[0]).OrderByDescending(x => x.final_packing_detail_id).ToList();
                        if (lstFinalPackingDetail != null && lstFinalPackingDetail.Count > 0)
                        {
                            foreach (FinalPackingDetail lFinalPackingDetail in lstFinalPackingDetail)
                            {
                                ExportInvoiceDetail lExportInvoiceDetail = new ExportInvoiceDetail();
                                OrderDeliverySlipDetails lOrderDeliverySlipDetails = CommonDAL.SelectDataFromDataBase<OrderDeliverySlipDetails>(new string[] { "PRODUCT_ID", "ORDER_DETAIL_ID" }, new string[] { "=", "=" },
                                new object[] { lFinalPackingDetail.product_id, lFinalPackingDetail.order_id }).FirstOrDefault();
                                if (lOrderDeliverySlipDetails != null)
                                {
                                    lExportInvoiceDetail.invoice_rate = lOrderDeliverySlipDetails.rate;
                                    lExportInvoiceDetail.unit_type_value = lOrderDeliverySlipDetails.unit_type_value;
                                    lExportInvoiceDetail.package_no = lFinalPackingDetail.package_no;
                                    lExportInvoiceDetail.final_packing_detail_id = lFinalPackingDetail.final_packing_detail_id;
                                    lExportInvoiceDetail.product_id = lFinalPackingDetail.product_id;
                                    lExportInvoiceDetail.quantity = lFinalPackingDetail.quantity;
                                    lExportInvoiceDetail = Setdescription(lExportInvoiceDetail);
                                    lExportInvoiceDetail.gross_weight = lFinalPackingDetail.gross_weight;
                                    lExportInvoiceDetail.net_weight= lFinalPackingDetail.net_weight;
                                    //grsweight
                                    //   netweight
                                    lstExportInvoiceDetail.Add(lExportInvoiceDetail);
                                }
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return lstExportInvoiceDetail;
        }
    }
}




