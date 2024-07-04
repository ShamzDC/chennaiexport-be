using CHEExportsDataAccessLayer;
using CHEExportsDataObjects;

using CHEExportsProto;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Math;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace CHEExportsService
{
    public class ApplicationSerivce : CHEExportsProto.ApplicationService.ApplicationServiceBase
    {
        public override async Task<protoCommonDDlList> AutoSearchForCommon(protoStringData2 aprotoStringData, ServerCallContext context)
        {
            List<CommonDDl> lstCommonDDl = new List<CommonDDl>();
            try
            {
                lstCommonDDl = CommonDAL.CommonAutoSearch(aprotoStringData.Data1, aprotoStringData.Data2, aprotoStringData.Long1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(new CommonDDlList().GetProto(lstCommonDDl));
        }
        #region customer

        #region CreateNewCustomerSearch
        public override Task<protoSearchParams> CreateNewCustomerSearch(Empty request, ServerCallContext context)
        {

            protoSearchParams lCustomerSearch = new protoSearchParams();
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lCustomerSearch);
        }
        #endregion

        #region SearchCustomer
        public override Task<protoCustomerSearchResult> SearchCustomer(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<CustomerSearchResultset> searchResults = new SearchResultBase<CustomerSearchResultset>();
            try
            {
                Customer lCustomer = new Customer();
                DALCustomer lDALCustomer = new DALCustomer(lCustomer);
                searchResults = lDALCustomer.SearchCustomer(request);
                //lprotoCustomerSearchResult.PlstprotoCustomerSearchResultSet.Add(new protoCustomerSearchResultSet());

            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new CustomerSearchResult().GetProto(searchResults));
        }
        #endregion

        public override Task<entDDLData> GetInitialDataCustomerAndVendor(Empty request, ServerCallContext context)
        {
            entDDLData lentDDLData = new entDDLData();
            try
            {
                entDDL lentDDL = new entDDL();
                string config_ids = Constants.Application.Active_Iactive_Status_id + "," + Constants.Application.customer_type_id + "," + Constants.Application.Region_id;
                List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLStatus", Constants.Application.Active_Iactive_Status_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLRegion", Constants.Application.Region_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLCustomerType", Constants.Application.customer_type_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lentDDLData);
        }
        public override async Task<protoCustomer> CreateNewCustomer(Empty empty, ServerCallContext context)
        {
            Customer lCustomer = new Customer();
            try
            {
                DALCustomer lDALCustomer = new DALCustomer(lCustomer);
                string token = context.RequestHeaders.GetValue("authorization");
                lCustomer.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lDALCustomer.CreateNewCustomer();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCustomer.GetProto());
        }

        public override async Task<protoCustomer> SaveCustomer(protoCustomer aprotoCustomer, ServerCallContext context)
        {
            Customer lCustomer = new Customer();
            try
            {
                lCustomer.status_value = Constants.Application.Active;
                string token = context.RequestHeaders.GetValue("authorization");
                lCustomer.GetData(aprotoCustomer);
                lCustomer.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lCustomer.changed_date = DateTime.Now;
                lCustomer.entered_date = DateTime.Now;
                DALCustomer lDALCustomer = new DALCustomer(lCustomer);
                lDALCustomer.SaveCustomer(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCustomer.GetProto());
        }
        public override async Task<entDDLClass> SaveCustomerForOrderSrn(protoCustomer aprotoCustomer, ServerCallContext context)
        {
            entDDLClass LentDDLClass = new entDDLClass();
            Customer lCustomer = new Customer();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lCustomer.GetData(aprotoCustomer);
                lCustomer.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lCustomer.changed_date = DateTime.Now;
                lCustomer.entered_date = DateTime.Now;
                DALCustomer lDALCustomer = new DALCustomer(lCustomer);
                lDALCustomer.SaveCustomer(token);
                LentDDLClass.LstDDLClass.AddRange(CommonDAL.GetAllCustomer(0));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(LentDDLClass);
        }
        public override async Task<protoCustomer> UpdateCustomer(protoCustomer aprotoCustomer, ServerCallContext context)
        {
            Customer lCustomer = new Customer();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lCustomer.GetData(aprotoCustomer);
                lCustomer.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALCustomer lDALCustomer = new DALCustomer(lCustomer);
                lCustomer.changed_date = DateTime.Now;
                lDALCustomer.UpdateCustomer(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCustomer.GetProto());
        }

        public override async Task<protoCustomer> OpenCustomer(protoLongData aprotoLongData, ServerCallContext context)
        {
            Customer Customer = new Customer();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Customer.customer_id = aprotoLongData.Data;
                    DALCustomer lDALCustomer = new DALCustomer(Customer);
                    lDALCustomer.OpenCustomer("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Customer.GetProto());
        }

        public override async Task<protoCustomer> DeleteCustomer(protoLongData aprotoLongData, ServerCallContext context)
        {
            Customer Customer = new Customer();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Customer.customer_id = aprotoLongData.Data;
                    DALCustomer lDALCustomer = new DALCustomer(Customer);
                    lDALCustomer.DeleteCustomer("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Customer.GetProto());
        }
        #endregion

        #region Vendor

        public override Task<protoSearchParams> CreateNewVendorSearch(Empty request, ServerCallContext context)
        {

            protoSearchParams lprotoSearchParams = new protoSearchParams();
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lprotoSearchParams);
        }


        #region SearchVendor
        public override Task<protoVendorSearchResult> SearchVendor(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<VendorSearchResultset> searchResults = new SearchResultBase<VendorSearchResultset>();
            try
            {
                Vendor lVendor = new Vendor();
                DALVendor lDALVendor = new DALVendor(lVendor);
                searchResults = lDALVendor.SearchVendor(request);
                //lprotoVendorSearchResult.PlstprotoVendorSearchResultSet.Add(new protoVendorSearchResultSet());
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new VendorSearchResult().GetProto(searchResults));
        }
        #endregion
        public override async Task<protoVendor> CreateNewVendor(Empty empty, ServerCallContext context)
        {
            Vendor lVendor = new Vendor();
            try
            {
                DALVendor lDALVendor = new DALVendor(lVendor);
                lDALVendor.CreateNewVendor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lVendor.GetProto());
        }

        public override async Task<protoVendor> SaveVendor(protoVendor aprotoVendor, ServerCallContext context)
        {
            Vendor lVendor = new Vendor();
            try
            {
                lVendor.GetData(aprotoVendor);
                string token = context.RequestHeaders.GetValue("authorization");
                lVendor.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lVendor.changed_date = DateTime.Now;
                lVendor.entered_date = DateTime.Now;
                lVendor.entered_by = lVendor.iLoggedInUserDetails.user_login_id;
                lVendor.changed_by = lVendor.iLoggedInUserDetails.user_login_id;
                DALVendor lDALVendor = new DALVendor(lVendor);
                lDALVendor.SaveVendor(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lVendor.GetProto());
        }

        public override async Task<protoVendor> UpdateVendor(protoVendor aprotoVendor, ServerCallContext context)
        {
            Vendor lVendor = new Vendor();
            try
            {
                lVendor.GetData(aprotoVendor);
                string token = context.RequestHeaders.GetValue("authorization");
                lVendor.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lVendor.changed_date = DateTime.Now;
                lVendor.changed_by = lVendor.iLoggedInUserDetails.user_login_id;
                DALVendor lDALVendor = new DALVendor(lVendor);
                lDALVendor.UpdateVendor(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lVendor.GetProto());
        }

        public override async Task<protoVendor> OpenVendor(protoLongData aprotoLongData, ServerCallContext context)
        {
            Vendor Vendor = new Vendor();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Vendor.vendor_id = aprotoLongData.Data;
                    DALVendor lDALVendor = new DALVendor(Vendor);
                    lDALVendor.OpenVendor("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Vendor.GetProto());
        }

        public override async Task<protoVendor> DeleteVendor(protoLongData aprotoLongData, ServerCallContext context)
        {
            Vendor Vendor = new Vendor();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Vendor.vendor_id = aprotoLongData.Data;
                    DALVendor lDALVendor = new DALVendor(Vendor);
                    lDALVendor.DeleteVendor("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Vendor.GetProto());
        }

        #endregion

        #region Company
        public override async Task<protoCompany> CreateNewCompany(Empty empty, ServerCallContext context)
        {
            Company lCompany = new Company();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCompany.GetProto());
        }

        public override async Task<protoCompany> SaveCompany(protoCompany aprotoCompany, ServerCallContext context)
        {
            Company lCompany = new Company();
            try
            {
                lCompany.GetData(aprotoCompany);
                string token = context.RequestHeaders.GetValue("authorization");
                lCompany.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lCompany.changed_date = DateTime.Now;
                lCompany.entered_date = DateTime.Now;
                lCompany.entered_by = lCompany.iLoggedInUserDetails.user_login_id;
                lCompany.changed_by = lCompany.iLoggedInUserDetails.user_login_id;
                DALCompany lDALCompany = new DALCompany(lCompany);
                lDALCompany.SaveCompany(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCompany.GetProto());
        }

        public override async Task<protoCompany> UpdateCompany(protoCompany aprotoCompany, ServerCallContext context)
        {
            Company lCompany = new Company();
            try
            {
                lCompany.GetData(aprotoCompany);
                string token = context.RequestHeaders.GetValue("authorization");
                lCompany.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lCompany.changed_date = DateTime.Now;
                lCompany.changed_by = lCompany.iLoggedInUserDetails.user_login_id;
                DALCompany lDALCompany = new DALCompany(lCompany);
                lDALCompany.UpdateCompany(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCompany.GetProto());
        }

        public override async Task<protoCompany> OpenCompany(protoLongData aprotoLongData, ServerCallContext context)
        {
            Company Company = new Company();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Company.company_id = aprotoLongData.Data;
                    DALCompany lDALCompany = new DALCompany(Company);
                    lDALCompany.OpenCompany("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Company.GetProto());
        }

        public override async Task<protoCompany> DeleteCompany(protoLongData aprotoLongData, ServerCallContext context)
        {
            Company Company = new Company();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Company.company_id = aprotoLongData.Data;
                    DALCompany lDALCompany = new DALCompany(Company);
                    lDALCompany.DeleteCompany("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Company.GetProto());
        }
        #endregion

        #region CompanyBankDetail
        public override async Task<protoCompanyBankDetail> CreateNewCompanyBankDetail(Empty empty, ServerCallContext context)
        {
            CompanyBankDetail lCompanyBankDetail = new CompanyBankDetail();
            try
            {
                DALCompanyBankDetail lDALCompanyBankDetail = new DALCompanyBankDetail(lCompanyBankDetail);
                lDALCompanyBankDetail.CreateNewCompanyBankDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCompanyBankDetail.GetProto());
        }

        public override async Task<protoCompanyBankDetail> SaveCompanyBankDetail(protoCompanyBankDetail aprotoCompanyBankDetail, ServerCallContext context)
        {
            CompanyBankDetail lCompanyBankDetail = new CompanyBankDetail();
            try
            {
                lCompanyBankDetail.GetData(aprotoCompanyBankDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lCompanyBankDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lCompanyBankDetail.changed_date = DateTime.Now;
                lCompanyBankDetail.entered_date = DateTime.Now;
                lCompanyBankDetail.entered_by = lCompanyBankDetail.iLoggedInUserDetails.user_login_id;
                lCompanyBankDetail.changed_by = lCompanyBankDetail.iLoggedInUserDetails.user_login_id;
                DALCompanyBankDetail lDALCompanyBankDetail = new DALCompanyBankDetail(lCompanyBankDetail);
                lDALCompanyBankDetail.SaveCompanyBankDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCompanyBankDetail.GetProto());
        }

        public override async Task<protoCompanyBankDetail> UpdateCompanyBankDetail(protoCompanyBankDetail aprotoCompanyBankDetail, ServerCallContext context)
        {
            CompanyBankDetail lCompanyBankDetail = new CompanyBankDetail();
            try
            {
                lCompanyBankDetail.GetData(aprotoCompanyBankDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lCompanyBankDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lCompanyBankDetail.changed_date = DateTime.Now;
                lCompanyBankDetail.changed_by = lCompanyBankDetail.iLoggedInUserDetails.user_login_id;
                DALCompanyBankDetail lDALCompanyBankDetail = new DALCompanyBankDetail(lCompanyBankDetail);
                lDALCompanyBankDetail.UpdateCompanyBankDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lCompanyBankDetail.GetProto());
        }

        public override async Task<protoCompanyBankDetail> OpenCompanyBankDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            CompanyBankDetail CompanyBankDetail = new CompanyBankDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    CompanyBankDetail.company_bank_id = aprotoLongData.Data;
                    DALCompanyBankDetail lDALCompanyBankDetail = new DALCompanyBankDetail(CompanyBankDetail);
                    lDALCompanyBankDetail.OpenCompanyBankDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(CompanyBankDetail.GetProto());
        }

        public override async Task<protoCompanyBankDetail> DeleteCompanyBankDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            CompanyBankDetail CompanyBankDetail = new CompanyBankDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    CompanyBankDetail.company_bank_id = aprotoLongData.Data;
                    DALCompanyBankDetail lDALCompanyBankDetail = new DALCompanyBankDetail(CompanyBankDetail);
                    lDALCompanyBankDetail.DeleteCompanyBankDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(CompanyBankDetail.GetProto());
        }
        #endregion

        #region WareHouse

        public override Task<protoWareHouseSearchResult> SearchWareHouse(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<WareHouseSearchResultset> searchResults = new SearchResultBase<WareHouseSearchResultset>();
            try
            {
                WareHouse lWareHouse = new WareHouse();
                DALWareHouse lDALWareHouse = new DALWareHouse(lWareHouse);
                searchResults = lDALWareHouse.SearchWareHouse(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new WareHouseSearchResult().GetProto(searchResults));
        }
        public override async Task<protoWareHouse> CreateNewWareHouse(Empty empty, ServerCallContext context)
        {
            WareHouse lWareHouse = new WareHouse();
            try
            {
                DALWareHouse lDALWareHouse = new DALWareHouse(lWareHouse);
                lDALWareHouse.CreateNewWareHouse();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lWareHouse.GetProto());
        }

        public override async Task<protoWareHouse> SaveWareHouse(protoWareHouse aprotoWareHouse, ServerCallContext context)
        {
            WareHouse lWareHouse = new WareHouse();
            try
            {
                lWareHouse.GetData(aprotoWareHouse);
                string token = context.RequestHeaders.GetValue("authorization");
                lWareHouse.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lWareHouse.changed_date = DateTime.Now;
                lWareHouse.entered_date = DateTime.Now;
                lWareHouse.entered_by = lWareHouse.iLoggedInUserDetails.user_login_id;
                lWareHouse.changed_by = lWareHouse.iLoggedInUserDetails.user_login_id;
                DALWareHouse lDALWareHouse = new DALWareHouse(lWareHouse);
                lDALWareHouse.SaveWareHouse(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lWareHouse.GetProto());
        }

        public override async Task<protoWareHouse> UpdateWareHouse(protoWareHouse aprotoWareHouse, ServerCallContext context)
        {
            WareHouse lWareHouse = new WareHouse();
            try
            {
                lWareHouse.GetData(aprotoWareHouse);
                string token = context.RequestHeaders.GetValue("authorization");
                lWareHouse.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lWareHouse.changed_date = DateTime.Now;
                lWareHouse.changed_by = lWareHouse.iLoggedInUserDetails.user_login_id;
                DALWareHouse lDALWareHouse = new DALWareHouse(lWareHouse);
                lDALWareHouse.UpdateWareHouse("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lWareHouse.GetProto());
        }

        public override async Task<protoWareHouse> OpenWareHouse(protoLongData aprotoLongData, ServerCallContext context)
        {
            WareHouse WareHouse = new WareHouse();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    WareHouse.warehouse_id = aprotoLongData.Data;
                    DALWareHouse lDALWareHouse = new DALWareHouse(WareHouse);
                    lDALWareHouse.OpenWareHouse("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(WareHouse.GetProto());
        }

        public override async Task<protoWareHouse> DeleteWareHouse(protoLongData aprotoLongData, ServerCallContext context)
        {
            WareHouse WareHouse = new WareHouse();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    WareHouse.warehouse_id = aprotoLongData.Data;
                    DALWareHouse lDALWareHouse = new DALWareHouse(WareHouse);
                    lDALWareHouse.DeleteWareHouse("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(WareHouse.GetProto());
        }
        #endregion

        #region OrderDetails

        #region CreateNewCustomerSearch
        public override Task<protoSearchParams> CreateNewOrderDetailsSearch(Empty request, ServerCallContext context)
        {

            protoSearchParams lprotoSearchParams = new protoSearchParams();
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lprotoSearchParams);
        }
        #endregion

        #region SearchCustomer
        public override Task<protoOrderDetailsResult> SearchOrderDetails(protoSearchParams request, ServerCallContext context)
        {
            {

                OrderSearch lOrderSearch = new OrderSearch();
                SearchResultBase<OrderSearchResultset> searchResults = new SearchResultBase<OrderSearchResultset>();
                try
                {
                    OrderDetails lOrderDetails = new OrderDetails();
                    DALOrderDetails lDALOrderDetails = new DALOrderDetails(lOrderDetails);
                    searchResults = lDALOrderDetails.SearchOrderDetails(request);
                }
                catch (Exception ex)
                {
                    throw;
                }
                return Task.FromResult(new OrderSearchResult().GetProto(searchResults));
                //CustomerSearch lCustomerSearch = new CustomerSearch();
            }
        }
        #endregion

        #region GetInitialDataForApplication
        public override Task<entDDLData> GetInitialDataForOrder(Empty request, ServerCallContext context)
        {
            entDDLData lentDDLData = new entDDLData();
            try
            {
                entDDL lentDDL = new entDDL();
                string config_ids = Constants.Application.Status_id + "," + Constants.Application.Unit_type_id + "," + Constants.Application.transport_name_id
                    + "," + Constants.Application.package_type_id + "," + Constants.Application.mode_of_packing_id + "," + Constants.Application.Region_id
                    + "," + Constants.Application.Mode_Payment_id + "," + Constants.Application.Dispatched_name_id + "," + Constants.Application.Destination_id;

                List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                //lentDDL = new entDDL().GetSubConfigValue("DDLStatus", Constants.Application.Status_id);
                //lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLStatus", Constants.Application.Status_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLRegion", Constants.Application.Region_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLUnitType", Constants.Application.Unit_type_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLTransPortName", Constants.Application.transport_name_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLModeOfPayment", Constants.Application.Mode_Payment_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLDispatched", Constants.Application.Dispatched_name_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLDestination", Constants.Application.Destination_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);

                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLPakageType", Constants.Application.package_type_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLModeOFPakageType", Constants.Application.mode_of_packing_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetAllWareHouse("DDlAllWareHouse", 0);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetAllCustomer("DDlAllCustomer", 0);
                lentDDLData.Data.Add(lentDDL);

                lentDDL = new entDDL().GetAllProduct("DDlAllProduct", 0);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetAllVendor("DDlAllVendor", 0);
                lentDDLData.Data.Add(lentDDL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lentDDLData);
        }
        #endregion

        public override async Task<protoOrderDetails> CreateNewOrderDetails(protoStringData aprotoStringData, ServerCallContext context)
        {
            OrderDetails lOrderDetails = new OrderDetails();
            try
            {
                lOrderDetails.is_invoice = Constants.Application.Is_Yes;
                lOrderDetails.region_value = aprotoStringData.Data;
                DALOrderDetails lDALOrderDetails = new DALOrderDetails(lOrderDetails);
                lDALOrderDetails.GenerateRefNo();
                lOrderDetails.status_value = Constants.Application.pending_completion;
                string config_ids = Constants.Application.Status_id + "," + Constants.Application.Region_id;
                List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                lOrderDetails.status_description = lstSubConfig.Where(x => x.s_config_value == lOrderDetails.status_value).Select(x => x.s_config_description).FirstOrDefault();
                lOrderDetails.region_description = lstSubConfig.Where(x => x.s_config_value == lOrderDetails.region_value).Select(x => x.s_config_description).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDetails.GetProto());
        }

        public override async Task<protoOrderDetails> SaveOrderDetails(protoOrderDetails aprotoOrderDetails, ServerCallContext context)
        {
            OrderDetails lOrderDetails = new OrderDetails();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lOrderDetails.GetData(aprotoOrderDetails);
                lOrderDetails.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALOrderDetails lDALOrderDetails = new DALOrderDetails(lOrderDetails);
                lDALOrderDetails.SaveOrderDetails(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDetails.GetProto());
        }

        public override async Task<protoOrderDetails> UpdateOrderDetails(protoOrderDetails aprotoOrderDetails, ServerCallContext context)
        {
            OrderDetails lOrderDetails = new OrderDetails();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lOrderDetails.GetData(aprotoOrderDetails);
                lOrderDetails.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lOrderDetails.changed_date = DateTime.Now;
                lOrderDetails.changed_by = lOrderDetails.iLoggedInUserDetails.user_login_id;
                DALOrderDetails lDALOrderDetails = new DALOrderDetails(lOrderDetails);
                lDALOrderDetails.UpdateOrderDetails(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDetails.GetProto());
        }

        public override async Task<protoOrderDetails> OpenOrderDetails(protoLongData aprotoLongData, ServerCallContext context)
        {
            OrderDetails OrderDetails = new OrderDetails();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    OrderDetails.order_detail_id = aprotoLongData.Data;
                    DALOrderDetails lDALOrderDetails = new DALOrderDetails(OrderDetails);
                    lDALOrderDetails.OpenOrderDetails("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(OrderDetails.GetProto());
        }

        public override async Task<protoOrderDetails> DeleteOrderDetails(protoLongData aprotoLongData, ServerCallContext context)
        {
            OrderDetails OrderDetails = new OrderDetails();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    OrderDetails.order_detail_id = aprotoLongData.Data;
                    DALOrderDetails lDALOrderDetails = new DALOrderDetails(OrderDetails);
                    lDALOrderDetails.DeleteOrderDetails("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(OrderDetails.GetProto());
        }
        #endregion

        #region LRDetails
        public override async Task<protoLRDetails> CreateNewLRDetails(Empty empty, ServerCallContext context)
        {
            LRDetails lLRDetails = new LRDetails();
            try
            {
                //lLRDetails.consignee_name = Constants.Application.consignee_name;
                lLRDetails.received_date = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lLRDetails.GetProto());
        }

        public override async Task<protoLRDetails> SaveLRDetails(protoLRDetails aprotoLRDetails, ServerCallContext context)
        {
            LRDetails lLRDetails = new LRDetails();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lLRDetails.GetData(aprotoLRDetails);
                lLRDetails.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lLRDetails.changed_date = DateTime.Now;
                lLRDetails.entered_date = DateTime.Now;
                lLRDetails.entered_by = lLRDetails.iLoggedInUserDetails.user_login_id;
                lLRDetails.changed_by = lLRDetails.iLoggedInUserDetails.user_login_id;
                DALLRDetails lDALLRDetails = new DALLRDetails(lLRDetails);
                lDALLRDetails.SaveLRDetails(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lLRDetails.GetProto());
        }

        public override async Task<protoLRDetails> UpdateLRDetails(protoLRDetails aprotoLRDetails, ServerCallContext context)
        {
            LRDetails lLRDetails = new LRDetails();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lLRDetails.GetData(aprotoLRDetails);
                lLRDetails.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lLRDetails.changed_date = DateTime.Now;
                lLRDetails.changed_by = lLRDetails.iLoggedInUserDetails.user_login_id;
                DALLRDetails lDALLRDetails = new DALLRDetails(lLRDetails);
                lDALLRDetails.UpdateLRDetails(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lLRDetails.GetProto());
        }

        public override async Task<protoLRDetails> OpenLRDetails(protoLongData aprotoLongData, ServerCallContext context)
        {
            LRDetails LRDetails = new LRDetails();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    LRDetails.lr_detail_id = aprotoLongData.Data;
                    DALLRDetails lDALLRDetails = new DALLRDetails(LRDetails);
                    lDALLRDetails.OpenLRDetails("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(LRDetails.GetProto());
        }

        public override async Task<protoLRDetails> DeleteLRDetails(protoLongData aprotoLongData, ServerCallContext context)
        {
            LRDetails LRDetails = new LRDetails();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    LRDetails.lr_detail_id = aprotoLongData.Data;
                    DALLRDetails lDALLRDetails = new DALLRDetails(LRDetails);
                    lDALLRDetails.DeleteLRDetails("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(LRDetails.GetProto());
        }
        #endregion

        #region InvoiceDetails
        public override async Task<protoInvoiceDetails> CreateNewInvoiceDetails(Empty empty, ServerCallContext context)
        {
            InvoiceDetails lInvoiceDetails = new InvoiceDetails();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lInvoiceDetails.GetProto());
        }

        public override async Task<protoOrderDetails> SaveInvoiceDetailsAndSlipDetails(protoOrderDetails aprotoOrderDetails, ServerCallContext context)
        {
            OrderDetails lOrderDetails = new OrderDetails();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lOrderDetails.GetData(aprotoOrderDetails);
                lOrderDetails.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lOrderDetails.changed_date = DateTime.Now;
                lOrderDetails.changed_by = lOrderDetails.iLoggedInUserDetails.user_login_id;
                DALOrderDetails lDALOrderDetails = new DALOrderDetails(lOrderDetails);
                lDALOrderDetails.UpdateOrderDetails(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDetails.GetProto());
        }
        public override async Task<protoInvoiceDetails> SaveInvoiceDetails(protoInvoiceDetails aprotoInvoiceDetails, ServerCallContext context)
        {
            InvoiceDetails lInvoiceDetails = new InvoiceDetails();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lInvoiceDetails.GetData(aprotoInvoiceDetails);
                lInvoiceDetails.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lInvoiceDetails.changed_date = DateTime.Now;
                lInvoiceDetails.changed_by = lInvoiceDetails.iLoggedInUserDetails.user_login_id;
                lInvoiceDetails.entered_date = DateTime.Now;
                lInvoiceDetails.entered_by = lInvoiceDetails.iLoggedInUserDetails.user_login_id;
                DALInvoiceDetails lDALInvoiceDetails = new DALInvoiceDetails(lInvoiceDetails);
                lDALInvoiceDetails.SaveInvoiceDetails(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lInvoiceDetails.GetProto());
        }

        public override async Task<protoInvoiceDetails> UpdateInvoiceDetails(protoInvoiceDetails aprotoInvoiceDetails, ServerCallContext context)
        {
            InvoiceDetails InvoiceDetails = new InvoiceDetails();
            try
            {
                InvoiceDetails.GetData(aprotoInvoiceDetails);
                DALInvoiceDetails lDALInvoiceDetails = new DALInvoiceDetails(InvoiceDetails);
                lDALInvoiceDetails.UpdateInvoiceDetails("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(InvoiceDetails.GetProto());
        }

        public override async Task<protoInvoiceDetails> OpenInvoiceDetails(protoLongData aprotoLongData, ServerCallContext context)
        {
            InvoiceDetails InvoiceDetails = new InvoiceDetails();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    InvoiceDetails.invoice_detail_id = aprotoLongData.Data;
                    DALInvoiceDetails lDALInvoiceDetails = new DALInvoiceDetails(InvoiceDetails);
                    lDALInvoiceDetails.OpenInvoiceDetails("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(InvoiceDetails.GetProto());
        }

        public override async Task<protoInvoiceDetails> DeleteInvoiceDetails(protoLongData aprotoLongData, ServerCallContext context)
        {
            InvoiceDetails InvoiceDetails = new InvoiceDetails();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    InvoiceDetails.order_detail_id = aprotoLongData.Data;
                    DALInvoiceDetails lDALInvoiceDetails = new DALInvoiceDetails(InvoiceDetails);
                    lDALInvoiceDetails.DeleteInvoiceDetails("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(InvoiceDetails.GetProto());
        }
        #endregion

        #region InvoiceDetailsItems
        public override async Task<protoInvoiceDetailsItems> CreateNewInvoiceDetailsItems(Empty empty, ServerCallContext context)
        {
            protoInvoiceDetailsItems lprotoInvoiceDetailsItems = new protoInvoiceDetailsItems();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lprotoInvoiceDetailsItems);
        }

        public override async Task<protoInvoiceDetailsItems> SaveInvoiceDetailsItems(protoInvoiceDetailsItems aprotoInvoiceDetailsItems, ServerCallContext context)
        {
            InvoiceDetailsItems InvoiceDetailsItems = new InvoiceDetailsItems();
            try
            {
                InvoiceDetailsItems.GetData(aprotoInvoiceDetailsItems);
                DALInvoiceDetailsItems lDALInvoiceDetailsItems = new DALInvoiceDetailsItems(InvoiceDetailsItems);
                lDALInvoiceDetailsItems.SaveInvoiceDetailsItems("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(InvoiceDetailsItems.GetProto());
        }

        public override async Task<protoInvoiceDetailsItems> UpdateInvoiceDetailsItems(protoInvoiceDetailsItems aprotoInvoiceDetailsItems, ServerCallContext context)
        {
            InvoiceDetailsItems InvoiceDetailsItems = new InvoiceDetailsItems();
            try
            {
                InvoiceDetailsItems.GetData(aprotoInvoiceDetailsItems);
                DALInvoiceDetailsItems lDALInvoiceDetailsItems = new DALInvoiceDetailsItems(InvoiceDetailsItems);
                lDALInvoiceDetailsItems.UpdateInvoiceDetailsItems("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(InvoiceDetailsItems.GetProto());
        }

        public override async Task<protoInvoiceDetailsItems> OpenInvoiceDetailsItems(protoLongData aprotoLongData, ServerCallContext context)
        {
            InvoiceDetailsItems InvoiceDetailsItems = new InvoiceDetailsItems();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    InvoiceDetailsItems.invoice_detail_item_id = aprotoLongData.Data;
                    DALInvoiceDetailsItems lDALInvoiceDetailsItems = new DALInvoiceDetailsItems(InvoiceDetailsItems);
                    lDALInvoiceDetailsItems.OpenInvoiceDetailsItems("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(InvoiceDetailsItems.GetProto());
        }

        public override async Task<protoInvoiceDetailsItemsList> DeleteInvoiceDetailsItems(protoInvoiceDetailsItems request, ServerCallContext context)
        {
            InvoiceDetailsItems InvoiceDetailsItems = new InvoiceDetailsItems();
            List<InvoiceDetailsItems> lstInvoiceDetailsItems = new List<InvoiceDetailsItems>();
            try
            {
                if (request != null && request.InvoiceDetailItemId > 0)
                {
                    DALInvoiceDetailsItems lDALInvoiceDetailsItems = new DALInvoiceDetailsItems(InvoiceDetailsItems);
                    lstInvoiceDetailsItems=lDALInvoiceDetailsItems.DeleteInvoiceDetailsItems("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(new InvoiceDetailsItemsList().GetProto(lstInvoiceDetailsItems));
        }
        #endregion

        #region OrderDeliverySlipDetail
        public override async Task<protoOrderDeliverySlipDetail> CreateNewOrderDeliverySlipDetail(Empty empty, ServerCallContext context)
        {
            OrderDeliverySlipDetails lOrderDeliverySlipDetails = new OrderDeliverySlipDetails();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetails.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetail> SaveOrderDeliverySlipDetail(protoOrderDeliverySlipDetail aprotoOrderDeliverySlipDetail, ServerCallContext context)
        {
            OrderDeliverySlipDetails lOrderDeliverySlipDetails = new OrderDeliverySlipDetails();
            try
            {
                lOrderDeliverySlipDetails.GetData(aprotoOrderDeliverySlipDetail);
                DALOrderDeliverySlipDetails lDALOrderDeliverySlipDetail = new DALOrderDeliverySlipDetails(lOrderDeliverySlipDetails);
                lDALOrderDeliverySlipDetail.SaveOrderDeliverySlipDetails("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetails.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetail> UpdateOrderDeliverySlipDetail(protoOrderDeliverySlipDetail aprotoOrderDeliverySlipDetail, ServerCallContext context)
        {
            OrderDeliverySlipDetails lOrderDeliverySlipDetails = new OrderDeliverySlipDetails();
            try
            {
                lOrderDeliverySlipDetails.GetData(aprotoOrderDeliverySlipDetail);
                DALOrderDeliverySlipDetails lDALOrderDeliverySlipDetail = new DALOrderDeliverySlipDetails(lOrderDeliverySlipDetails);
                lDALOrderDeliverySlipDetail.UpdateOrderDeliverySlipDetails("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetails.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetail> OpenOrderDeliverySlipDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            OrderDeliverySlipDetails lOrderDeliverySlipDetails = new OrderDeliverySlipDetails();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    lOrderDeliverySlipDetails.order_delivery_slip_detail_id = aprotoLongData.Data;
                    DALOrderDeliverySlipDetails lDALOrderDeliverySlipDetail = new DALOrderDeliverySlipDetails(lOrderDeliverySlipDetails);
                    lDALOrderDeliverySlipDetail.OpenOrderDeliverySlipDetails("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetails.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetailList> DeleteOrderDeliverySlipDetail(protoOrderDeliverySlipDetail request, ServerCallContext context)
        {
            List<OrderDeliverySlipDetails> lstOrderDeliverySlipDetails = new List<OrderDeliverySlipDetails>();
            OrderDeliverySlipDetails lOrderDeliverySlipDetails = new OrderDeliverySlipDetails();
            lOrderDeliverySlipDetails.GetData(request);
            try
            {
                if (request != null && request.OrderDeliverySlipDetailId > 0)
                {
                    DALOrderDeliverySlipDetails lDALOrderDeliverySlipDetail = new DALOrderDeliverySlipDetails(lOrderDeliverySlipDetails);
                    lstOrderDeliverySlipDetails = lDALOrderDeliverySlipDetail.DeleteOrderDeliverySlipDetails("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(new OrderDeliverySlipDetailsList().GetProto(lstOrderDeliverySlipDetails));
        }
        #endregion

        #region OrderDeliverySlipDetailsItems
        public override async Task<protoOrderDeliverySlipDetailsItems> CreateNewOrderDeliverySlipDetailsItems(Empty empty, ServerCallContext context)
        {
            protoOrderDeliverySlipDetailsItems lprotoOrderDeliverySlipDetailsItems = new protoOrderDeliverySlipDetailsItems();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lprotoOrderDeliverySlipDetailsItems);
        }

        public override async Task<protoOrderDeliverySlipDetailsItems> SaveOrderDeliverySlipDetailsItems(protoOrderDeliverySlipDetailsItems aprotoOrderDeliverySlipDetailsItems, ServerCallContext context)
        {
            OrderDeliverySlipDetailsItems lOrderDeliverySlipDetailsItems = new OrderDeliverySlipDetailsItems();
            try
            {
                lOrderDeliverySlipDetailsItems.GetData(aprotoOrderDeliverySlipDetailsItems);
                DALOrderDeliverySlipDetailsItems lDALOrderDeliverySlipDetailsItems = new DALOrderDeliverySlipDetailsItems(lOrderDeliverySlipDetailsItems);
                lDALOrderDeliverySlipDetailsItems.SaveOrderDeliverySlipDetailsItems("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetailsItems.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetailsItems> UpdateOrderDeliverySlipDetailsItems(protoOrderDeliverySlipDetailsItems aprotoOrderDeliverySlipDetailsItems, ServerCallContext context)
        {
            OrderDeliverySlipDetailsItems lOrderDeliverySlipDetailsItems = new OrderDeliverySlipDetailsItems();
            try
            {
                lOrderDeliverySlipDetailsItems.GetData(aprotoOrderDeliverySlipDetailsItems);
                DALOrderDeliverySlipDetailsItems lDALOrderDeliverySlipDetailsItems = new DALOrderDeliverySlipDetailsItems(lOrderDeliverySlipDetailsItems);
                lDALOrderDeliverySlipDetailsItems.UpdateOrderDeliverySlipDetailsItems("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetailsItems.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetailsItems> OpenOrderDeliverySlipDetailsItems(protoLongData aprotoLongData, ServerCallContext context)
        {
            OrderDeliverySlipDetailsItems lOrderDeliverySlipDetailsItems = new OrderDeliverySlipDetailsItems();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    lOrderDeliverySlipDetailsItems.order_delivery_slip_detail_items_id = aprotoLongData.Data;
                    DALOrderDeliverySlipDetailsItems lDALOrderDeliverySlipDetailsItems = new DALOrderDeliverySlipDetailsItems(lOrderDeliverySlipDetailsItems);
                    lDALOrderDeliverySlipDetailsItems.OpenOrderDeliverySlipDetailsItems("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetailsItems.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetailsItems> DeleteOrderDeliverySlipDetailsItems(protoLongData aprotoLongData, ServerCallContext context)
        {
            OrderDeliverySlipDetailsItems lOrderDeliverySlipDetailsItems = new OrderDeliverySlipDetailsItems();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    lOrderDeliverySlipDetailsItems.order_delivery_slip_detail_items_id = aprotoLongData.Data;
                    DALOrderDeliverySlipDetailsItems lDALOrderDeliverySlipDetailsItems = new DALOrderDeliverySlipDetailsItems(lOrderDeliverySlipDetailsItems);
                    lDALOrderDeliverySlipDetailsItems.DeleteOrderDeliverySlipDetailsItems("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetailsItems.GetProto());
        }
        #endregion

        #region Product
        public override Task<protoProductSearchResult> SearchProduct(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<ProductSearchResultset> searchResults = new SearchResultBase<ProductSearchResultset>();
            try
            {
                Product lProduct = new Product();
                DALProduct lDALProduct = new DALProduct(lProduct);
                searchResults = lDALProduct.SearchProduct(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new ProductSearchResult().GetProto(searchResults));
        }

        public override async Task<protoProduct> CreateNewProduct(Empty empty, ServerCallContext context)
        {
            Product lProduct = new Product();
            try
            {
                lProduct.status_value = Constants.Application.Active;
                string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
                List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                lProduct.status_description = lstSubConfig.Where(x => x.s_config_value == lProduct.status_value).Select(x => x.s_config_description).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lProduct.GetProto());
        }

        public override async Task<protoProduct> SaveProduct(protoProduct aprotoProduct, ServerCallContext context)
        {
            Product lProduct = new Product();
            try
            {
                lProduct.GetData(aprotoProduct);
                string token = context.RequestHeaders.GetValue("authorization");
                lProduct.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lProduct.changed_date = DateTime.Now;
                lProduct.changed_by = lProduct.iLoggedInUserDetails.user_login_id;
                lProduct.entered_date = DateTime.Now;
                lProduct.entered_by = lProduct.iLoggedInUserDetails.user_login_id;
                DALProduct lDALProduct = new DALProduct(lProduct);
                lDALProduct.SaveProduct(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lProduct.GetProto());
        }

        public override async Task<protoProduct> UpdateProduct(protoProduct aprotoProduct, ServerCallContext context)
        {
            Product lProduct = new Product();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lProduct.GetData(aprotoProduct);
                lProduct.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lProduct.changed_date = DateTime.Now;
                lProduct.changed_by = lProduct.iLoggedInUserDetails.user_login_id;
                lProduct.GetData(aprotoProduct);
                DALProduct lDALProduct = new DALProduct(lProduct);
                lDALProduct.UpdateProduct(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lProduct.GetProto());
        }

        public override async Task<protoProduct> OpenProduct(protoLongData aprotoLongData, ServerCallContext context)
        {
            Product Product = new Product();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Product.product_id = aprotoLongData.Data;
                    DALProduct lDALProduct = new DALProduct(Product);
                    lDALProduct.OpenProduct("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Product.GetProto());
        }

        public override async Task<protoProduct> DeleteProduct(protoLongData aprotoLongData, ServerCallContext context)
        {
            Product Product = new Product();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Product.product_id = aprotoLongData.Data;
                    DALProduct lDALProduct = new DALProduct(Product);
                    lDALProduct.DeleteProduct("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Product.GetProto());
        }
        #endregion

        #region OrderDeliverySlipDetailWithoutInvoice
        public override async Task<protoOrderDeliverySlipDetailWithoutInvoice> CreateNewOrderDeliverySlipDetailWithoutInvoice(Empty empty, ServerCallContext context)
        {
            OrderDeliverySlipDetailWithoutInvoice lOrderDeliverySlipDetailWithoutInvoice = new OrderDeliverySlipDetailWithoutInvoice();
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetailWithoutInvoice.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetailWithoutInvoice> SaveOrderDeliverySlipDetailWithoutInvoice(protoOrderDeliverySlipDetailWithoutInvoice aprotoOrderDeliverySlipDetailWithoutInvoice, ServerCallContext context)
        {
            OrderDeliverySlipDetailWithoutInvoice lOrderDeliverySlipDetailWithoutInvoice = new OrderDeliverySlipDetailWithoutInvoice();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lOrderDeliverySlipDetailWithoutInvoice.GetData(aprotoOrderDeliverySlipDetailWithoutInvoice);
                lOrderDeliverySlipDetailWithoutInvoice.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lOrderDeliverySlipDetailWithoutInvoice.changed_date = DateTime.Now;
                lOrderDeliverySlipDetailWithoutInvoice.entered_date = DateTime.Now;
                DALOrderDeliverySlipDetailWithoutInvoice lDALOrderDeliverySlipDetailWithoutInvoice = new DALOrderDeliverySlipDetailWithoutInvoice(lOrderDeliverySlipDetailWithoutInvoice);
                lDALOrderDeliverySlipDetailWithoutInvoice.SaveOrderDeliverySlipDetailWithoutInvoice(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetailWithoutInvoice.GetProto());
        }
        public override async Task<protoOrderDeliverySlipDetailWithoutInvoice> UpdateOrderDeliverySlipDetailWithoutInvoice(protoOrderDeliverySlipDetailWithoutInvoice aprotoOrderDeliverySlipDetailWithoutInvoice, ServerCallContext context)
        {
            OrderDeliverySlipDetailWithoutInvoice lOrderDeliverySlipDetailWithoutInvoice = new OrderDeliverySlipDetailWithoutInvoice();
            try
            {
                string token = context.RequestHeaders.GetValue("authorization");
                lOrderDeliverySlipDetailWithoutInvoice.GetData(aprotoOrderDeliverySlipDetailWithoutInvoice);
                lOrderDeliverySlipDetailWithoutInvoice.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALOrderDeliverySlipDetailWithoutInvoice lDALOrderDeliverySlipDetailWithoutInvoice = new DALOrderDeliverySlipDetailWithoutInvoice(lOrderDeliverySlipDetailWithoutInvoice);
                lOrderDeliverySlipDetailWithoutInvoice.changed_date = DateTime.Now;
                lOrderDeliverySlipDetailWithoutInvoice.changed_by = lOrderDeliverySlipDetailWithoutInvoice.iLoggedInUserDetails.user_login_id;
                lDALOrderDeliverySlipDetailWithoutInvoice.UpdateOrderDeliverySlipDetailWithoutInvoice(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lOrderDeliverySlipDetailWithoutInvoice.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetailWithoutInvoice> OpenOrderDeliverySlipDetailWithoutInvoice(protoLongData aprotoLongData, ServerCallContext context)
        {
            OrderDeliverySlipDetailWithoutInvoice OrderDeliverySlipDetailWithoutInvoice = new OrderDeliverySlipDetailWithoutInvoice();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    OrderDeliverySlipDetailWithoutInvoice.order_delivery_slip_without_id = aprotoLongData.Data;
                    DALOrderDeliverySlipDetailWithoutInvoice lDALOrderDeliverySlipDetailWithoutInvoice = new DALOrderDeliverySlipDetailWithoutInvoice(OrderDeliverySlipDetailWithoutInvoice);
                    lDALOrderDeliverySlipDetailWithoutInvoice.OpenOrderDeliverySlipDetailWithoutInvoice("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(OrderDeliverySlipDetailWithoutInvoice.GetProto());
        }

        public override async Task<protoOrderDeliverySlipDetailWithoutInvoice> DeleteOrderDeliverySlipDetailWithoutInvoice(protoLongData aprotoLongData, ServerCallContext context)
        {
            OrderDeliverySlipDetailWithoutInvoice OrderDeliverySlipDetailWithoutInvoice = new OrderDeliverySlipDetailWithoutInvoice();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    OrderDeliverySlipDetailWithoutInvoice.order_delivery_slip_without_id = aprotoLongData.Data;
                    DALOrderDeliverySlipDetailWithoutInvoice lDALOrderDeliverySlipDetailWithoutInvoice = new DALOrderDeliverySlipDetailWithoutInvoice(OrderDeliverySlipDetailWithoutInvoice);
                    lDALOrderDeliverySlipDetailWithoutInvoice.DeleteOrderDeliverySlipDetailWithoutInvoice("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(OrderDeliverySlipDetailWithoutInvoice.GetProto());
        }
        #endregion

        #region RepackingDetail

        #region GetInitialDataForApplication
        public override Task<entDDLData> GetInitialDataForRepaking(Empty request, ServerCallContext context)
        {
            entDDLData lentDDLData = new entDDLData();
            try
            {
                entDDL lentDDL = new entDDL();
                string config_ids = Constants.Application.repaking_status_id + "," + Constants.Application.Unit_type_id + "," + Constants.Application.package_type_id;
                List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLRepakingStatus", Constants.Application.repaking_status_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLUnitType", Constants.Application.Unit_type_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetSubConfigValuesfromlist("DDLPakageType", Constants.Application.package_type_id, lstSubConfig);
                lentDDLData.Data.Add(lentDDL);
                lentDDL = new entDDL().GetOrderWithoutRepaking("DDlOrder", 0);
                lentDDLData.Data.Add(lentDDL);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lentDDLData);
        }
        #endregion

        #region CreateNewRepackingDetailSearch
        public override Task<protoSearchParams> CreateNewRepackingDetailSearch(Empty request, ServerCallContext context)
        {
            protoSearchParams lprotoSearchParams = new protoSearchParams();
            RepakingDetailsSearch lRepakingDetailsSearch = new RepakingDetailsSearch();
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lprotoSearchParams);
        }
        #endregion

        #region SearchRepackingDetail
        public override Task<protoRepackingDetailResult> SearchRepackingDetail(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<RepakingDetailsSearchResultset> searchResults = new SearchResultBase<RepakingDetailsSearchResultset>();
            try
            {
                // lRepakingDetailsSearch.GetData(request);
                RepackingDetail lRepackingListDetail = new RepackingDetail();
                DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(lRepackingListDetail);
                searchResults = lDALRepackingDetail.SearchRepackingDetail(request);
                //lprotoCustomerSearchResult.PlstprotoCustomerSearchResultSet.Add(new protoCustomerSearchResultSet());

            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new RepackingDetailSearchResult().GetProto(searchResults));
        }
        #endregion
        public override async Task<protoRepackingDetail> CreateNewRepackingDetail(Empty empty, ServerCallContext context)
        {
            RepackingDetail lRepackingDetail = new RepackingDetail();
            try
            {
                DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(lRepackingDetail);
                lDALRepackingDetail.CreateNewRepackingDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingDetail.GetProto());
        }

        public override async Task<protoRepackingDetail> SaveRepackingDetail(protoRepackingDetail aprotoRepackingDetail, ServerCallContext context)
        {
            RepackingDetail lRepackingDetail = new RepackingDetail();
            try
            {
                lRepackingDetail.GetData(aprotoRepackingDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lRepackingDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(lRepackingDetail);
                lDALRepackingDetail.SaveRepackingDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingDetail.GetProto());
        }


        public override async Task<protoRepackingDetail> CheckOrderStatusInRepacking(protoLongData aprotoLongData, ServerCallContext context)
        {
            RepackingDetail lRepackingDetail = new RepackingDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    lRepackingDetail.order_id = aprotoLongData.Data;
                    DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(lRepackingDetail);
                    lDALRepackingDetail.CheckOrderRepackingStatus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingDetail.GetProto());
        }

        public override async Task<protoRepackingDetail> UpdateRepackingDetail(protoRepackingDetail aprotoRepackingDetail, ServerCallContext context)
        {
            RepackingDetail lRepackingDetail = new RepackingDetail();
            try
            {
                lRepackingDetail.GetData(aprotoRepackingDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lRepackingDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lRepackingDetail.changed_date = DateTime.Now;
                lRepackingDetail.changed_by = lRepackingDetail.iLoggedInUserDetails.user_login_id;
                DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(lRepackingDetail);
                lDALRepackingDetail.UpdateRepackingDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingDetail.GetProto());
        }
        public override async Task<protoRepackingDetail> UpdateApprovedRepackingDetail(protoRepackingDetail aprotoRepackingDetail, ServerCallContext context)
        {
            RepackingDetail lRepackingDetail = new RepackingDetail();
            try
            {
                lRepackingDetail.GetData(aprotoRepackingDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lRepackingDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lRepackingDetail.changed_date = DateTime.Now;
                lRepackingDetail.changed_by = lRepackingDetail.iLoggedInUserDetails.user_login_id;
                DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(lRepackingDetail);
                lDALRepackingDetail.UpdateApprovedRepackingDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingDetail.GetProto());
        }
        public override async Task<protoRepackingDetail> GetOrderDetailsForRepaking(protoLongData aprotoLongData, ServerCallContext context)
        {
            RepackingDetail lRepackingDetail = new RepackingDetail();
            try
            {

                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    lRepackingDetail.order_id = aprotoLongData.Data;
                    DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(lRepackingDetail);
                    lDALRepackingDetail.GetOrderDetails();
                    // lRepackingDetail.lstRepackingListDetail.Add(new RepackingListDetail());

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingDetail.GetProto());
        }
        public override async Task<protoRepackingDetail> GetOrderDetailsBasedOnValues(entPassingParam aentPassingParam, ServerCallContext context)
        {
            RepackingDetail lRepackingDetail = new RepackingDetail();
            try
            {
                Common lCommon = new Common();
                ProtoDataConverter.GetData(aentPassingParam, lCommon);
                DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(lRepackingDetail);
                lDALRepackingDetail.GetOrderDetailsBasedOnValues(lCommon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingDetail.GetProto());
        }
        public override async Task<protoRepackingDetail> OpenRepackingDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            RepackingDetail RepackingDetail = new RepackingDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    RepackingDetail.repacking_detail_id = aprotoLongData.Data;
                    DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(RepackingDetail);
                    lDALRepackingDetail.OpenRepackingDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(RepackingDetail.GetProto());
        }

        public override async Task<protoRepackingDetail> DeleteRepackingDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            RepackingDetail RepackingDetail = new RepackingDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    RepackingDetail.repacking_detail_id = aprotoLongData.Data;
                    DALRepackingDetail lDALRepackingDetail = new DALRepackingDetail(RepackingDetail);
                    lDALRepackingDetail.DeleteRepackingDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(RepackingDetail.GetProto());
        }
        #endregion

        #region RepackingListDetail
        public override async Task<protoRepackingListDetail> CreateNewRepackingListDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            RepackingListDetail lRepackingListDetail = new RepackingListDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    string token = context.RequestHeaders.GetValue("authorization");
                    DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                    lDALRepackingListDetail.CreateNewRepackingListDetail(aprotoLongData.Data, token);
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingListDetail.GetProto());
        }

        public override async Task<protoRepackingListDetail> SaveRepackingListDetail(protoRepackingListDetail aprotoRepackingListDetail, ServerCallContext context)
        {
            RepackingListDetail lRepackingListDetail = new RepackingListDetail();
            try
            {
                lRepackingListDetail.GetData(aprotoRepackingListDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lRepackingListDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lRepackingListDetail.changed_date = DateTime.Now;
                lRepackingListDetail.entered_date = DateTime.Now;
                lRepackingListDetail.entered_by = lRepackingListDetail.iLoggedInUserDetails.user_login_id;
                lRepackingListDetail.changed_by = lRepackingListDetail.iLoggedInUserDetails.user_login_id;
                DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                lDALRepackingListDetail.SaveRepackingListDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingListDetail.GetProto());
        }

        public override async Task<protoRepackingListDetail> UpdateRepackingListDetail(protoRepackingListDetail aprotoRepackingListDetail, ServerCallContext context)
        {
            RepackingListDetail lRepackingListDetail = new RepackingListDetail();
            try
            {
                lRepackingListDetail.GetData(aprotoRepackingListDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lRepackingListDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lRepackingListDetail.changed_date = DateTime.Now;
                lRepackingListDetail.changed_by = lRepackingListDetail.iLoggedInUserDetails.user_login_id;
                DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                lDALRepackingListDetail.UpdateRepackingListDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRepackingListDetail.GetProto());
        }

        public override async Task<protoRepackingListDetail> OpenRepackingListDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            RepackingListDetail RepackingListDetail = new RepackingListDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    RepackingListDetail.repacking_list_detail_id = aprotoLongData.Data;
                    DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(RepackingListDetail);
                    lDALRepackingListDetail.OpenRepackingListDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(RepackingListDetail.GetProto());
        }

        public override async Task<protoRepackingListDetailList> DeleteRepackingListDetail(protoRepackingListDetail request, ServerCallContext context)
        {
            RepackingListDetail lRepackingListDetail = new RepackingListDetail();
            List<RepackingListDetail> lstRepackingListDetail = new List<RepackingListDetail>();
            try
            {
                if (request.RepackingListDetailId > 0)
                {
                    lRepackingListDetail.GetData(request);
                    DALRepackingListDetail lDALRepackingListDetail = new DALRepackingListDetail(lRepackingListDetail);
                    lstRepackingListDetail=lDALRepackingListDetail.DeleteRepackingListDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(new RepackingListDetailList().GetProto(lstRepackingListDetail));
        }
        #endregion

        #region FinalPackingDetail
        public override Task<protoFinalPackingDetailsSearchResult> SearchFinalPackingDetails(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<FinalPackingDetailsSearchResultset> searchResults = new SearchResultBase<FinalPackingDetailsSearchResultset>();
            try
            {
                FinalPackingDetail lFinalPackingDetails = new FinalPackingDetail();
                DALFinalPackingDetail lDALFinalPackingDetails = new DALFinalPackingDetail(lFinalPackingDetails);
                searchResults = lDALFinalPackingDetails.SearchFinalPackingDetails(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new FinalPackingDetailsSearchResult().GetProto(searchResults));
        }
        public override async Task<protoFinalPackingDetail> CreateNewFinalPackingDetail(Empty empty, ServerCallContext context)
        {
            FinalPackingDetail lFinalPackingDetail = new FinalPackingDetail();
            try
            {
                DALFinalPackingDetail lDALFinalPackingDetail = new DALFinalPackingDetail(lFinalPackingDetail);
                lDALFinalPackingDetail.CreateNewFinalPackingDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lFinalPackingDetail.GetProto());
        }

        public override async Task<protoFinalPackingDetail> SaveFinalPackingDetail(protoFinalPackingDetail aprotoFinalPackingDetail, ServerCallContext context)
        {
            FinalPackingDetail lFinalPackingDetail = new FinalPackingDetail();
            try
            {
                lFinalPackingDetail.GetData(aprotoFinalPackingDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lFinalPackingDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lFinalPackingDetail.changed_date = DateTime.Now;
                lFinalPackingDetail.entered_date = DateTime.Now;
                lFinalPackingDetail.entered_by = lFinalPackingDetail.iLoggedInUserDetails.user_login_id;
                lFinalPackingDetail.changed_by = lFinalPackingDetail.iLoggedInUserDetails.user_login_id;
                DALFinalPackingDetail lDALFinalPackingDetail = new DALFinalPackingDetail(lFinalPackingDetail);
                lDALFinalPackingDetail.SaveFinalPackingDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lFinalPackingDetail.GetProto());
        }

        public override async Task<protoFinalPackingDetail> UpdateFinalPackingDetail(protoFinalPackingDetail aprotoFinalPackingDetail, ServerCallContext context)
        {
            FinalPackingDetail lFinalPackingDetail = new FinalPackingDetail();
            try
            {
                lFinalPackingDetail.GetData(aprotoFinalPackingDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lFinalPackingDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lFinalPackingDetail.changed_date = DateTime.Now;
                lFinalPackingDetail.changed_by = lFinalPackingDetail.iLoggedInUserDetails.user_login_id;
                DALFinalPackingDetail lDALFinalPackingDetail = new DALFinalPackingDetail(lFinalPackingDetail);
                lDALFinalPackingDetail.UpdateFinalPackingDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lFinalPackingDetail.GetProto());
        }

        public override async Task<protoFinalPackingDetail> OpenFinalPackingDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            FinalPackingDetail FinalPackingDetail = new FinalPackingDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    FinalPackingDetail.final_packing_detail_id = aprotoLongData.Data;
                    DALFinalPackingDetail lDALFinalPackingDetail = new DALFinalPackingDetail(FinalPackingDetail);
                    lDALFinalPackingDetail.OpenFinalPackingDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(FinalPackingDetail.GetProto());
        }

        public override async Task<protoFinalPackingDetail> DeleteFinalPackingDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            FinalPackingDetail FinalPackingDetail = new FinalPackingDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    FinalPackingDetail.final_packing_detail_id = aprotoLongData.Data;
                    DALFinalPackingDetail lDALFinalPackingDetail = new DALFinalPackingDetail(FinalPackingDetail);
                    lDALFinalPackingDetail.DeleteFinalPackingDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(FinalPackingDetail.GetProto());
        }
        #endregion

        #region ExportInvoice

        #region SearchCustomer
        public override Task<protoExportInvoiceSearchResult> SearchExportInvoice(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<ExportInvoiceSearchResultset> searchResults = new SearchResultBase<ExportInvoiceSearchResultset>();
            try
            {
                ExportInvoice lExportInvoice = new ExportInvoice();
                DALExportInvoice lDALExportInvoice = new DALExportInvoice(lExportInvoice);
                searchResults = lDALExportInvoice.SearchExportInvoice(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new ExportInvoiceSearchResult().GetProto(searchResults));
        }
        #endregion

        public override Task<protoExportInvoiceDetailList> GetInStockInvoiceDetailBasedPakgNO(protoStringData request, ServerCallContext context)
        {
            List<ExportInvoiceDetail> lstExportInvoiceDetail = new List<ExportInvoiceDetail>();
            ExportInvoiceDetail lExportInvoiceDetail = new ExportInvoiceDetail();
            try
            {
                DALExportInvoiceDetail lDALExportInvoiceDetail = new DALExportInvoiceDetail(lExportInvoiceDetail);
                lstExportInvoiceDetail = lDALExportInvoiceDetail.GetInStockInvoiceDetailBasedPakgNO(request.Data);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new ExportInvoiceDetailList().GetProto(lstExportInvoiceDetail));
        }
        public override async Task<protoExportInvoice> CreateNewExportInvoice(Empty empty, ServerCallContext context)
        {
            ExportInvoice lExportInvoice = new ExportInvoice();
            try
            {
                DALExportInvoice lDALExportInvoice = new DALExportInvoice(lExportInvoice);
                lDALExportInvoice.CreateNewExportInvoice();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportInvoice.GetProto());
        }

        public override async Task<protoExportInvoice> SaveExportInvoice(protoExportInvoice aprotoExportInvoice, ServerCallContext context)
        {
            ExportInvoice lExportInvoice = new ExportInvoice();
            try
            {
                lExportInvoice.GetData(aprotoExportInvoice);
                string token = context.RequestHeaders.GetValue("authorization");
                lExportInvoice.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALExportInvoice lDALExportInvoice = new DALExportInvoice(lExportInvoice);
                lDALExportInvoice.SaveExportInvoice(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportInvoice.GetProto());
        }

        public override async Task<protoExportInvoice> UpdateExportInvoice(protoExportInvoice aprotoExportInvoice, ServerCallContext context)
        {
            ExportInvoice lExportInvoice = new ExportInvoice();
            try
            {
                lExportInvoice.GetData(aprotoExportInvoice);
                string token = context.RequestHeaders.GetValue("authorization");
                lExportInvoice.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lExportInvoice.changed_date = DateTime.Now;
                lExportInvoice.changed_by = lExportInvoice.iLoggedInUserDetails.user_login_id;
                DALExportInvoice lDALExportInvoice = new DALExportInvoice(lExportInvoice);
                lDALExportInvoice.UpdateExportInvoice(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportInvoice.GetProto());
        }

        public override async Task<protoExportInvoice> OpenExportInvoice(protoLongData aprotoLongData, ServerCallContext context)
        {
            ExportInvoice ExportInvoice = new ExportInvoice();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    ExportInvoice.export_invoice_id = aprotoLongData.Data;
                    DALExportInvoice lDALExportInvoice = new DALExportInvoice(ExportInvoice);
                    lDALExportInvoice.OpenExportInvoice("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(ExportInvoice.GetProto());
        }

        public override async Task<protoExportInvoice> DeleteExportInvoice(protoLongData aprotoLongData, ServerCallContext context)
        {
            ExportInvoice ExportInvoice = new ExportInvoice();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    ExportInvoice.export_invoice_id = aprotoLongData.Data;
                    DALExportInvoice lDALExportInvoice = new DALExportInvoice(ExportInvoice);
                    lDALExportInvoice.DeleteExportInvoice("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(ExportInvoice.GetProto());
        }
        #endregion

        #region ExportInvoiceDetail
        public override async Task<protoExportInvoiceDetail> CreateNewExportInvoiceDetail(Empty empty, ServerCallContext context)
        {
            ExportInvoiceDetail lExportInvoiceDetail = new ExportInvoiceDetail();
            try
            {
                DALExportInvoiceDetail lDALExportInvoiceDetail = new DALExportInvoiceDetail(lExportInvoiceDetail);
                lDALExportInvoiceDetail.CreateNewExportInvoiceDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportInvoiceDetail.GetProto());
        }

        public override async Task<protoExportInvoiceDetail> SaveExportInvoiceDetail(protoExportInvoiceDetail aprotoExportInvoiceDetail, ServerCallContext context)
        {
            ExportInvoiceDetail lExportInvoiceDetail = new ExportInvoiceDetail();
            try
            {
                lExportInvoiceDetail.GetData(aprotoExportInvoiceDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lExportInvoiceDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                //lExportInvoiceDetail.changed_date = DateTime.Now;
                //lExportInvoiceDetail.entered_date = DateTime.Now;
                //lExportInvoiceDetail.entered_by = lExportInvoiceDetail.iLoggedInUserDetails.user_login_id;
                //lExportInvoiceDetail.changed_by = lExportInvoiceDetail.iLoggedInUserDetails.user_login_id;
                DALExportInvoiceDetail lDALExportInvoiceDetail = new DALExportInvoiceDetail(lExportInvoiceDetail);
                lDALExportInvoiceDetail.SaveExportInvoiceDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportInvoiceDetail.GetProto());
        }

        public override async Task<protoExportInvoiceDetail> UpdateExportInvoiceDetail(protoExportInvoiceDetail aprotoExportInvoiceDetail, ServerCallContext context)
        {
            ExportInvoiceDetail lExportInvoiceDetail = new ExportInvoiceDetail();
            try
            {
                lExportInvoiceDetail.GetData(aprotoExportInvoiceDetail);
                string token = context.RequestHeaders.GetValue("authorization");
                lExportInvoiceDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lExportInvoiceDetail.changed_date = DateTime.Now;
                lExportInvoiceDetail.changed_by = lExportInvoiceDetail.iLoggedInUserDetails.user_login_id;
                DALExportInvoiceDetail lDALExportInvoiceDetail = new DALExportInvoiceDetail(lExportInvoiceDetail);
                lDALExportInvoiceDetail.UpdateExportInvoiceDetail(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportInvoiceDetail.GetProto());
        }

        public override async Task<protoExportInvoiceDetail> OpenExportInvoiceDetail(protoLongData aprotoLongData, ServerCallContext context)
        {
            ExportInvoiceDetail ExportInvoiceDetail = new ExportInvoiceDetail();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    ExportInvoiceDetail.export_invoice_detail_id = aprotoLongData.Data;
                    DALExportInvoiceDetail lDALExportInvoiceDetail = new DALExportInvoiceDetail(ExportInvoiceDetail);
                    lDALExportInvoiceDetail.OpenExportInvoiceDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(ExportInvoiceDetail.GetProto());
        }

        public override async Task<protoExportInvoiceDetailList> DeleteExportInvoiceDetail(protoExportInvoiceDetail aprotoLongData, ServerCallContext context)
        {
            List<ExportInvoiceDetail> lstExportInvoiceDetail = new List<ExportInvoiceDetail>();
            ExportInvoiceDetail ExportInvoiceDetail = new ExportInvoiceDetail();
            ExportInvoiceDetail.GetData(aprotoLongData);
            try
            {
                if (aprotoLongData != null && aprotoLongData.ExportInvoiceDetailId > 0)
                {
                    DALExportInvoiceDetail lDALExportInvoiceDetail = new DALExportInvoiceDetail(ExportInvoiceDetail);
                    lstExportInvoiceDetail=lDALExportInvoiceDetail.DeleteExportInvoiceDetail("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(new ExportInvoiceDetailList().GetProto(lstExportInvoiceDetail));
        }
        #endregion

        #region Consignee

        public override Task<protoConsigneeSearchResult> SearchConsignee(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<ConsigneeSearchResultset> searchResults = new SearchResultBase<ConsigneeSearchResultset>();
            try
            {
                Consignee lConsignee = new Consignee();
                DALConsignee lDALConsignee = new DALConsignee(lConsignee);
                searchResults = lDALConsignee.SearchConsignee(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new ConsigneeSearchResult().GetProto(searchResults));
        }
        public override async Task<protoConsignee> CreateNewConsignee(Empty empty, ServerCallContext context)
        {
            Consignee lConsignee = new Consignee();
            try
            {
                DALConsignee lDALConsignee = new DALConsignee(lConsignee);
                lDALConsignee.CreateNewConsignee();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lConsignee.GetProto());
        }

        public override async Task<protoConsignee> SaveConsignee(protoConsignee aprotoConsignee, ServerCallContext context)
        {
            Consignee lConsignee = new Consignee();
            try
            {
                lConsignee.GetData(aprotoConsignee);
                string token = context.RequestHeaders.GetValue("authorization");
                lConsignee.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lConsignee.changed_date = DateTime.Now;
                lConsignee.entered_date = DateTime.Now;
                lConsignee.entered_by = lConsignee.iLoggedInUserDetails.user_login_id;
                lConsignee.changed_by = lConsignee.iLoggedInUserDetails.user_login_id;
                DALConsignee lDALConsignee = new DALConsignee(lConsignee);
                lDALConsignee.SaveConsignee(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lConsignee.GetProto());
        }

        public override async Task<protoConsignee> UpdateConsignee(protoConsignee aprotoConsignee, ServerCallContext context)
        {
            Consignee lConsignee = new Consignee();
            try
            {
                lConsignee.GetData(aprotoConsignee);
                string token = context.RequestHeaders.GetValue("authorization");
                lConsignee.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lConsignee.changed_date = DateTime.Now;
                lConsignee.changed_by = lConsignee.iLoggedInUserDetails.user_login_id;
                DALConsignee lDALConsignee = new DALConsignee(lConsignee);
                lDALConsignee.UpdateConsignee(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lConsignee.GetProto());
        }

        public override async Task<protoConsignee> OpenConsignee(protoLongData aprotoLongData, ServerCallContext context)
        {
            Consignee Consignee = new Consignee();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Consignee.consignee_id = aprotoLongData.Data;
                    DALConsignee lDALConsignee = new DALConsignee(Consignee);
                    lDALConsignee.OpenConsignee("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Consignee.GetProto());
        }

        public override async Task<protoConsignee> DeleteConsignee(protoLongData aprotoLongData, ServerCallContext context)
        {
            Consignee Consignee = new Consignee();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Consignee.consignee_id = aprotoLongData.Data;
                    DALConsignee lDALConsignee = new DALConsignee(Consignee);
                    lDALConsignee.DeleteConsignee("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Consignee.GetProto());
        }
        #endregion

        #region Buyer

        public override Task<protoBuyerSearchResult> SearchBuyer(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<BuyerSearchResultset> searchResults = new SearchResultBase<BuyerSearchResultset>();
            try
            {
                Buyer lBuyer = new Buyer();
                DALBuyer lDALBuyer = new DALBuyer(lBuyer);
                searchResults = lDALBuyer.SearchBuyer(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new BuyerSearchResult().GetProto(searchResults));
        }
        public override async Task<protoBuyer> CreateNewBuyer(Empty empty, ServerCallContext context)
        {
            Buyer lBuyer = new Buyer();
            try
            {
                DALBuyer lDALBuyer = new DALBuyer(lBuyer);
                lDALBuyer.CreateNewBuyer();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lBuyer.GetProto());
        }

        public override async Task<protoBuyer> SaveBuyer(protoBuyer aprotoBuyer, ServerCallContext context)
        {
            Buyer lBuyer = new Buyer();
            try
            {
                lBuyer.GetData(aprotoBuyer);
                string token = context.RequestHeaders.GetValue("authorization");
                lBuyer.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lBuyer.changed_date = DateTime.Now;
                lBuyer.entered_date = DateTime.Now;
                lBuyer.entered_by = lBuyer.iLoggedInUserDetails.user_login_id;
                lBuyer.changed_by = lBuyer.iLoggedInUserDetails.user_login_id;
                DALBuyer lDALBuyer = new DALBuyer(lBuyer);
                lDALBuyer.SaveBuyer(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lBuyer.GetProto());
        }

        public override async Task<protoBuyer> UpdateBuyer(protoBuyer aprotoBuyer, ServerCallContext context)
        {
            Buyer lBuyer = new Buyer();
            try
            {
                lBuyer.GetData(aprotoBuyer);
                string token = context.RequestHeaders.GetValue("authorization");
                lBuyer.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lBuyer.changed_date = DateTime.Now;
                lBuyer.changed_by = lBuyer.iLoggedInUserDetails.user_login_id;
                DALBuyer lDALBuyer = new DALBuyer(lBuyer);
                lDALBuyer.UpdateBuyer(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lBuyer.GetProto());
        }

        public override async Task<protoBuyer> OpenBuyer(protoLongData aprotoLongData, ServerCallContext context)
        {
            Buyer Buyer = new Buyer();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Buyer.buyer_id = aprotoLongData.Data;
                    DALBuyer lDALBuyer = new DALBuyer(Buyer);
                    lDALBuyer.OpenBuyer("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Buyer.GetProto());
        }

        public override async Task<protoBuyer> DeleteBuyer(protoLongData aprotoLongData, ServerCallContext context)
        {
            Buyer Buyer = new Buyer();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Buyer.buyer_id = aprotoLongData.Data;
                    DALBuyer lDALBuyer = new DALBuyer(Buyer);
                    lDALBuyer.DeleteBuyer("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Buyer.GetProto());
        }

        #endregion

        #region Preorder

        public override Task<protoPreorderSearchResult> SearchPreorder(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<PreorderSearchResultset> searchResults = new SearchResultBase<PreorderSearchResultset>();
            try
            {
                Preorder lPreorder = new Preorder();
                DALPreorder lDALPreorder = new DALPreorder(lPreorder);
                searchResults = lDALPreorder.SearchPreorder(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new PreorderSearchResult().GetProto(searchResults));
        }
        public override async Task<protoPreorder> CreateNewPreorder(Empty empty, ServerCallContext context)
        {
            Preorder lPreorder = new Preorder();
            try
            {
                DALPreorder lDALPreorder = new DALPreorder(lPreorder);
                lDALPreorder.CreateNewPreorder();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lPreorder.GetProto());
        }

        public override async Task<protoPreorder> SavePreorder(protoPreorder aprotoPreorder, ServerCallContext context)
        {
            Preorder lPreorder = new Preorder();
            try
            {
                lPreorder.GetData(aprotoPreorder);
                string token = context.RequestHeaders.GetValue("authorization");
                lPreorder.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALPreorder lDALPreorder = new DALPreorder(lPreorder);
                lDALPreorder.SavePreorder(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lPreorder.GetProto());
        }

        public override async Task<protoPreorder> UpdatePreorder(protoPreorder aprotoPreorder, ServerCallContext context)
        {
            Preorder lPreorder = new Preorder();
            try
            {
                lPreorder.GetData(aprotoPreorder);
                string token = context.RequestHeaders.GetValue("authorization");
                lPreorder.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lPreorder.changed_date = DateTime.Now;
                lPreorder.changed_by = lPreorder.iLoggedInUserDetails.user_login_id;
                DALPreorder lDALPreorder = new DALPreorder(lPreorder);
                lDALPreorder.UpdatePreorder(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lPreorder.GetProto());
        }

        public override async Task<protoPreorder> OpenPreorder(protoLongData aprotoLongData, ServerCallContext context)
        {
            Preorder Preorder = new Preorder();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Preorder.preorder_id = aprotoLongData.Data;
                    DALPreorder lDALPreorder = new DALPreorder(Preorder);
                    lDALPreorder.OpenPreorder("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Preorder.GetProto());
        }

        public override async Task<protoPreorder> DeletePreorder(protoLongData aprotoLongData, ServerCallContext context)
        {
            Preorder Preorder = new Preorder();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Preorder.preorder_id = aprotoLongData.Data;
                    DALPreorder lDALPreorder = new DALPreorder(Preorder);
                    lDALPreorder.DeletePreorder("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Preorder.GetProto());
        }
        public override async Task<protoPreorderList> GetTodayPreorders(protoStringData aprotoLongData, ServerCallContext context)
        {
            List<Preorder> lstPreorder = new List<Preorder>();
            Preorder Preorder = new Preorder();
            try
            {
                DALPreorder lDALPreorder = new DALPreorder(Preorder);
                lstPreorder = lDALPreorder.GetTodayPreorders();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(new PreorderList().GetProto(lstPreorder));
        }

        #endregion

        #region Party

        public override Task<protoPartySearchResult> SearchParty(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<PartySearchResultset> searchResults = new SearchResultBase<PartySearchResultset>();
            try
            {
                Party lParty = new Party();
                DALParty lDALParty = new DALParty(lParty);
                searchResults = lDALParty.SearchParty(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new PartySearchResult().GetProto(searchResults));
        }
        public override async Task<protoParty> CreateNewParty(Empty empty, ServerCallContext context)
        {
            Party lParty = new Party();
            try
            {
                DALParty lDALParty = new DALParty(lParty);
                lDALParty.CreateNewParty();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lParty.GetProto());
        }

        public override async Task<protoParty> SaveParty(protoParty aprotoParty, ServerCallContext context)
        {
            Party lParty = new Party();
            try
            {
                lParty.GetData(aprotoParty);
                string token = context.RequestHeaders.GetValue("authorization");
                lParty.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALParty lDALParty = new DALParty(lParty);
                lDALParty.SaveParty(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lParty.GetProto());
        }

        public override async Task<protoParty> UpdateParty(protoParty aprotoParty, ServerCallContext context)
        {
            Party lParty = new Party();
            try
            {
                lParty.GetData(aprotoParty);
                string token = context.RequestHeaders.GetValue("authorization");
                lParty.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lParty.changed_date = DateTime.Now;
                lParty.changed_by = lParty.iLoggedInUserDetails.user_login_id;
                DALParty lDALParty = new DALParty(lParty);
                lDALParty.UpdateParty(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lParty.GetProto());
        }

        public override async Task<protoParty> OpenParty(protoLongData aprotoLongData, ServerCallContext context)
        {
            Party Party = new Party();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Party.party_id = aprotoLongData.Data;
                    DALParty lDALParty = new DALParty(Party);
                    lDALParty.OpenParty("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Party.GetProto());
        }

        public override async Task<protoParty> DeleteParty(protoLongData aprotoLongData, ServerCallContext context)
        {
            Party Party = new Party();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Party.party_id = aprotoLongData.Data;
                    DALParty lDALParty = new DALParty(Party);
                    lDALParty.DeleteParty("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Party.GetProto());
        }

        #endregion

        #region Exporter

        public override Task<protoExporterSearchResult> SearchExporter(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<ExporterSearchResultset> searchResults = new SearchResultBase<ExporterSearchResultset>();
            try
            {
                Exporter lExporter = new Exporter();
                DALExporter lDALExporter = new DALExporter(lExporter);
                searchResults = lDALExporter.SearchExporter(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new ExporterSearchResult().GetProto(searchResults));
        }
        public override async Task<protoExporter> CreateNewExporter(Empty empty, ServerCallContext context)
        {
            Exporter lExporter = new Exporter();
            try
            {
                DALExporter lDALExporter = new DALExporter(lExporter);
                lDALExporter.CreateNewExporter();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExporter.GetProto());
        }

        public override async Task<protoExporter> SaveExporter(protoExporter aprotoExporter, ServerCallContext context)
        {
            Exporter lExporter = new Exporter();
            try
            {
                lExporter.GetData(aprotoExporter);
                string token = context.RequestHeaders.GetValue("authorization");
                lExporter.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALExporter lDALExporter = new DALExporter(lExporter);
                lDALExporter.SaveExporter(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExporter.GetProto());
        }

        public override async Task<protoExporter> UpdateExporter(protoExporter aprotoExporter, ServerCallContext context)
        {
            Exporter lExporter = new Exporter();
            try
            {
                lExporter.GetData(aprotoExporter);
                string token = context.RequestHeaders.GetValue("authorization");
                lExporter.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lExporter.changed_date = DateTime.Now;
                lExporter.changed_by = lExporter.iLoggedInUserDetails.user_login_id;
                DALExporter lDALExporter = new DALExporter(lExporter);
                lDALExporter.UpdateExporter(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExporter.GetProto());
        }

        public override async Task<protoExporter> OpenExporter(protoLongData aprotoLongData, ServerCallContext context)
        {
            Exporter Exporter = new Exporter();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Exporter.exporter_id = aprotoLongData.Data;
                    DALExporter lDALExporter = new DALExporter(Exporter);
                    lDALExporter.OpenExporter("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Exporter.GetProto());
        }

        public override async Task<protoExporter> DeleteExporter(protoLongData aprotoLongData, ServerCallContext context)
        {
            Exporter Exporter = new Exporter();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Exporter.exporter_id = aprotoLongData.Data;
                    DALExporter lDALExporter = new DALExporter(Exporter);
                    lDALExporter.DeleteExporter("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Exporter.GetProto());
        }

        #endregion

        #region ExportConsignee

        public override Task<protoExportConsigneeSearchResult> SearchExportConsignee(protoSearchParams request, ServerCallContext context)
        {
            SearchResultBase<ExportConsigneeSearchResultset> searchResults = new SearchResultBase<ExportConsigneeSearchResultset>();
            try
            {
                ExportConsignee lExportConsignee = new ExportConsignee();
                DALExportConsignee lDALExportConsignee = new DALExportConsignee(lExportConsignee);
                searchResults = lDALExportConsignee.SearchExportConsignee(request);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(new ExportConsigneeSearchResult().GetProto(searchResults));
        }
        public override async Task<protoExportConsignee> CreateNewExportConsignee(Empty empty, ServerCallContext context)
        {
            ExportConsignee lExportConsignee = new ExportConsignee();
            try
            {
                DALExportConsignee lDALExportConsignee = new DALExportConsignee(lExportConsignee);
                lDALExportConsignee.CreateNewExportConsignee();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportConsignee.GetProto());
        }

        public override async Task<protoExportConsignee> SaveExportConsignee(protoExportConsignee aprotoExportConsignee, ServerCallContext context)
        {
            ExportConsignee lExportConsignee = new ExportConsignee();
            try
            {
                lExportConsignee.GetData(aprotoExportConsignee);
                string token = context.RequestHeaders.GetValue("authorization");
                lExportConsignee.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lExportConsignee.changed_date = DateTime.Now;
                lExportConsignee.entered_date = DateTime.Now;
                lExportConsignee.entered_by = lExportConsignee.iLoggedInUserDetails.user_login_id;
                lExportConsignee.changed_by = lExportConsignee.iLoggedInUserDetails.user_login_id;
                DALExportConsignee lDALExportConsignee = new DALExportConsignee(lExportConsignee);
                lDALExportConsignee.SaveExportConsignee(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportConsignee.GetProto());
        }

        public override async Task<protoExportConsignee> UpdateExportConsignee(protoExportConsignee aprotoExportConsignee, ServerCallContext context)
        {
            ExportConsignee lExportConsignee = new ExportConsignee();
            try
            {
                lExportConsignee.GetData(aprotoExportConsignee);
                string token = context.RequestHeaders.GetValue("authorization");
                lExportConsignee.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lExportConsignee.changed_date = DateTime.Now;
                lExportConsignee.changed_by = lExportConsignee.iLoggedInUserDetails.user_login_id;
                DALExportConsignee lDALExportConsignee = new DALExportConsignee(lExportConsignee);
                lDALExportConsignee.UpdateExportConsignee(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lExportConsignee.GetProto());
        }

        public override async Task<protoExportConsignee> OpenExportConsignee(protoLongData aprotoLongData, ServerCallContext context)
        {
            ExportConsignee ExportConsignee = new ExportConsignee();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    ExportConsignee.export_consignee_id = aprotoLongData.Data;
                    DALExportConsignee lDALExportConsignee = new DALExportConsignee(ExportConsignee);
                    lDALExportConsignee.OpenExportConsignee("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(ExportConsignee.GetProto());
        }

        public override async Task<protoExportConsignee> DeleteExportConsignee(protoLongData aprotoLongData, ServerCallContext context)
        {
            ExportConsignee ExportConsignee = new ExportConsignee();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    ExportConsignee.export_consignee_id = aprotoLongData.Data;
                    DALExportConsignee lDALExportConsignee = new DALExportConsignee(ExportConsignee);
                    lDALExportConsignee.DeleteExportConsignee("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(ExportConsignee.GetProto());
        }
        #endregion

        public override Task<entByteData> GenerateExportInvoiceExcel(protoStringData2 request, ServerCallContext context)
        {
            entByteData lentByteData = new entByteData();
            try
            {
                byte[] ExcelFile = { };
                ExportInvoice lExportInvoice = new ExportInvoice();
                lExportInvoice.export_invoice_id = request.Long1;
                DALExportInvoice lDALExportInvoice = new DALExportInvoice(lExportInvoice);
                ExcelFile = lDALExportInvoice.GenerateExportInvoiceExcel(request);
                if (ExcelFile != null)
                {
                    lentByteData.ByteData = ByteString.CopyFrom(ExcelFile.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lentByteData);
        }

        public override Task<entByteData> GenerateExportInvoiceWithoutPakageNoExcel(protoStringData2 request, ServerCallContext context)
        {
            entByteData lentByteData = new entByteData();
            try
            {
                byte[] ExcelFile = { };
                ExportInvoice lExportInvoice = new ExportInvoice();
                lExportInvoice.export_invoice_id = request.Long1;
                DALExportInvoice lDALExportInvoice = new DALExportInvoice(lExportInvoice);
                ExcelFile = lDALExportInvoice.GenerateExportInvoiceWithoutPakageNoExcel(request);
                if (ExcelFile != null)
                {
                    lentByteData.ByteData = ByteString.CopyFrom(ExcelFile.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lentByteData);
        }
        public override Task<protoStringData> GetOrdersDetailsForDashBoard(protoLongData request, ServerCallContext context)
        {
            protoStringData lprotoStringData = new protoStringData();
            try
            {
                DataSet dtSet = CommonDAL.GetDataSetbyExecuteSP("APP_DashBoardReportOrderDeetails", new string[] { "@customer_id" }, new object[] { request.Data});
                lprotoStringData.Data= JsonConvert.SerializeObject(dtSet, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lprotoStringData);
        }

        public override Task<protoOrdersMonthWiseList> GetOrdersDetailsMonthWise(entPassingParam request, ServerCallContext context)
        {
            protoOrdersMonthWiseList lprotoOrdersMonthWiseList = new protoOrdersMonthWiseList();
            try
            {
                string year = request.String1;
                DataSet dtSet = CommonDAL.GetDataSetbyExecuteSP("App_DashBoardReportForMonthAndYear", new string[] { "@YEAR" }, new object[] { year });
                lprotoOrdersMonthWiseList.Labels.AddRange(CommonDAL.GetMonths());
                if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0 && dtSet.Tables[0].Rows.Count > 0)
                {
                    List<int> lstInts = new List<int>();
                    protoOrdersMonthWise lprotoOrdersMonthWise = new protoOrdersMonthWise();
                    lprotoOrdersMonthWise.Label = "Total Received";

                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[0][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[0][0])); // JAN
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[1][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[1][0])); // FEB
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[2][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[2][0])); // MAR
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[3][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[3][0])); // APR
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[4][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[4][0])); // MAY
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[5][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[5][0])); // JUN
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[6][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[6][0])); // JUL
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[7][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[7][0])); // AUG
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[8][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[8][0])); // SEP
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[9][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[9][0])); // OCT
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[10][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[10][0])); // NOV
                    }
                    if (!DBNull.Value.Equals(Convert.ToInt64(dtSet.Tables[0].Rows[11][0])))
                    {
                        lstInts.Add(Convert.ToInt32(dtSet.Tables[0].Rows[11][0])); // DEC
                    }
                    if (lstInts != null && lstInts.Count > 0)
                        lprotoOrdersMonthWise.Data.AddRange(lstInts);
                    lprotoOrdersMonthWiseList.Datasets.Add(lprotoOrdersMonthWise);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(lprotoOrdersMonthWiseList);
        }

    }
}
