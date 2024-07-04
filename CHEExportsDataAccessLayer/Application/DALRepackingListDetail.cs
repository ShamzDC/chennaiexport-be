
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
    public class DALRepackingListDetail : DALBase
    {
        [DataMember]
        public RepackingListDetail iRepackingListDetail { get; private set; }

        public DALRepackingListDetail(RepackingListDetail aRepackingListDetail) : base(aRepackingListDetail)
        {
            iRepackingListDetail = aRepackingListDetail;
        }
        public RepackingListDetail SetdescriptionAndReturn(RepackingListDetail aRepackingListDetail)
        {
            string config_ids = Constants.Application.repaking_status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            aRepackingListDetail.status_description = lstSubConfig.Where(x => x.s_config_value == aRepackingListDetail.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if (aRepackingListDetail.product_id > 0)
            {
                aRepackingListDetail.iProduct = CommonDAL.SelectDataFromDataBase<Product>(new string[] { "PRODUCT_ID" }, new string[] { "=" },
                    new object[] { aRepackingListDetail.product_id }).FirstOrDefault();
            }
            return aRepackingListDetail;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.repaking_status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iRepackingListDetail.status_description = lstSubConfig.Where(x => x.s_config_value == iRepackingListDetail.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if (iRepackingListDetail.product_id > 0)
            {
                iRepackingListDetail.iProduct = CommonDAL.SelectDataFromDataBase<Product>(new string[] { "PRODUCT_ID" }, new string[] { "=" },
                    new object[] { iRepackingListDetail.product_id }).FirstOrDefault();
            }
        }
        public void CreateNewRepackingListDetail(long OrderId,string token)
        {
            try
            {
                iRepackingListDetail.status_value = Constants.Application.Pending_Approved_status;
                Setdescription();
                GenerateRefNo(OrderId, token);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public void GenerateRefNo(long OrderId,string token)
        {
            string prefix = string.Empty;
            if (string.IsNullOrEmpty(iRepackingListDetail.package_no))
            {
                if(OrderId != 0)
                {
                    long warehouseid = 0;
                    OrderDetails lOrderDetails = new OrderDetails();
                    lOrderDetails.order_detail_id = OrderId;
                    DALOrderDetails lDALOrderDetails = new DALOrderDetails(lOrderDetails);
                    lDALOrderDetails.Open(token);
                    WareHouse lWareHouse = new WareHouse();
                    lWareHouse.warehouse_id = lOrderDetails.warehouse_id;
                    DALWareHouse lDALWareHouse = new DALWareHouse(lWareHouse);
                    lDALWareHouse.Open(token);

                    prefix = lWareHouse.warehouse_ref_no.Substring(0, 3);
                }
                

                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber1", new string[] { "@Config_const", "@SampPrefix" }, new object[] { "PKAG",
                    prefix
                });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iRepackingListDetail.package_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        public void SaveRepackingListDetail(string token)
        {
            try
            {
                //ValidateRepackingListDetailSave();
                
                if (iRepackingListDetail != null && (iRepackingListDetail.errorMsg_lsit == null || iRepackingListDetail.errorMsg_lsit.Count == 0))
                {
                    if (iRepackingListDetail.repacking_list_detail_id == 0)
                    {
                        iRepackingListDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                        iRepackingListDetail.changed_date = DateTime.Now;
                        iRepackingListDetail.entered_date = DateTime.Now;
                        iRepackingListDetail.entered_by = iRepackingListDetail.iLoggedInUserDetails.user_login_id;
                        iRepackingListDetail.changed_by = iRepackingListDetail.iLoggedInUserDetails.user_login_id;
                       // GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iRepackingListDetail.changed_date = DateTime.Now;
                        iRepackingListDetail.changed_by = iRepackingListDetail.iLoggedInUserDetails.user_login_id;
                        Update(token);
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

        private void ValidateRepackingListDetailSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateRepackingListDetail(string token)
        {
            try
            {
                //ValidateRepackingListDetailSave();
                if (iRepackingListDetail != null && (iRepackingListDetail.errorMsg_lsit == null || iRepackingListDetail.errorMsg_lsit.Count == 0))
                {
                    iRepackingListDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                    iRepackingListDetail.changed_date = DateTime.Now;
                    iRepackingListDetail.changed_by = iRepackingListDetail.iLoggedInUserDetails.user_login_id;
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

        public List<RepackingListDetail> DeleteRepackingListDetail(string token)
        {
            List<RepackingListDetail> lst = new List<RepackingListDetail>();
            try
            {
                //ValidateRepackingListDetailDelete();
                if (iRepackingListDetail != null  && iRepackingListDetail.repacking_list_detail_id > 0)
                {
                    Delete(token);
                }
                lst= GetListofRepackingList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return lst;
        }

        private List<RepackingListDetail> GetListofRepackingList()
        {
            List<RepackingListDetail> lst = new List<RepackingListDetail>();
            if (iRepackingListDetail.repacking_detail_id > 0)
            {
                lst = CommonDAL.SelectDataFromDataBase<RepackingListDetail>(new string[] { "REPACKING_DETAIL_ID" }, new string[] { "=" },
                    new object[] { iRepackingListDetail.repacking_detail_id }).ToList();
                foreach(RepackingListDetail lRepackingListDetail in lst)
                {
                    SetdescriptionAndReturn(lRepackingListDetail);
                }
            }
            return lst;

        }

        public void OpenRepackingListDetail(string token)
        {
            try
            {
                if (iRepackingListDetail != null && (iRepackingListDetail.errorMsg_lsit == null || iRepackingListDetail.errorMsg_lsit.Count == 0) && iRepackingListDetail.repacking_list_detail_id > 0)
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
    }
}




