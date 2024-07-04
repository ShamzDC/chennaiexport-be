using CHEExportsProto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CHEExportsAPI
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private Metadata Metadata = new Metadata();
        private readonly string Token;
          public const string SERVICE_ENDPOINT = "http://localhost:5243";
        public ApplicationController(IHttpContextAccessor context)
        {
            Token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrWhiteSpace(Token))
            {
                Metadata.Add("Authorization", Token);
            }
            else
            {
                Metadata.Add("Authorization", string.Empty);
            }
        }

        [HttpPost]
        [Route("AutoSearchForCommon")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "AutoSearchForCommon")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCommonDDlList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> AutoSearchForCommon(protoStringData2 aprotoStringData2)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCommonDDlList lprotoCommonDDlList = await client1.AutoSearchForCommonAsync(aprotoStringData2);
                    return await Task.FromResult(this.GetResponse(lprotoCommonDDlList));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("CheckOrderStatusInRepacking")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CheckOrderStatusInRepacking")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CheckOrderStatusInRepacking(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoRepackingDetail = await client1.CheckOrderStatusInRepackingAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        #region Company
        [HttpGet]
        [Route("CreateNewCompany")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompany))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewCompany()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompany lprotoCompany = await client1.CreateNewCompanyAsync(empty,Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompany));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveCompany")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompany))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveCompany(protoCompany aprotoCompany)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompany lprotoCompany = await client1.SaveCompanyAsync(aprotoCompany, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompany));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateCompany")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompany))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateCompany(protoCompany aprotoCompany)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompany lprotoCompany = await client1.UpdateCompanyAsync(aprotoCompany, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompany));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenCompany")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompany))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenCompany(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompany lprotoCompany = await client1.OpenCompanyAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompany));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteCompany")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompany))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteCompany(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompany lprotoCompany = await client1.DeleteCompanyAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompany));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Customer
        [HttpGet]
        [Route("GetInitialDataCustomerAndVendor")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetInitialDataCustomerAndVendor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(entDDLData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetInitialDataCustomerAndVendor()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    entDDLData lentDDLData = await client1.GetInitialDataCustomerAndVendorAsync(empty);
                    return await Task.FromResult(this.GetResponse(lentDDLData));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpGet]
        [Route("CreateNewCustomerSearch")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewCustomerSearch")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSearchParams))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewCustomerSearch()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSearchParams lprotoCustomerSearch = await client1.CreateNewCustomerSearchAsync(empty);
                    return await Task.FromResult(this.GetResponse(lprotoCustomerSearch));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SearchCustomer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCustomerSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchCustomer(protoSearchParams aprotoCustomerSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCustomerSearchResult lprotoCustomerSearchResult = await client1.SearchCustomerAsync(aprotoCustomerSearch);
                    return await Task.FromResult(this.GetResponse(lprotoCustomerSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveCustomerForOrderSrn")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveCustomerForOrderSrn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(entDDLClass))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveCustomerForOrderSrn(protoCustomer aprotoCustomer)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    entDDLClass lentDDLClass = await client1.SaveCustomerForOrderSrnAsync(aprotoCustomer);
                    return await Task.FromResult(this.GetResponse(lentDDLClass));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpGet]
        [Route("CreateNewCustomer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCustomer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewCustomer()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCustomer lprotoCustomer = await client1.CreateNewCustomerAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCustomer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveCustomer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCustomer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveCustomer(protoCustomer aprotoCustomer)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCustomer lprotoCustomer = await client1.SaveCustomerAsync(aprotoCustomer, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCustomer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateCustomer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCustomer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateCustomer(protoCustomer aprotoCustomer)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCustomer lprotoCustomer = await client1.UpdateCustomerAsync(aprotoCustomer, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCustomer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenCustomer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCustomer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenCustomer(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCustomer lprotoCustomer = await client1.OpenCustomerAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCustomer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteCustomer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCustomer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteCustomer(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCustomer lprotoCustomer = await client1.DeleteCustomerAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCustomer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region InvoiceDetails
        [HttpGet]
        [Route("CreateNewInvoiceDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewInvoiceDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewInvoiceDetails()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetails lprotoInvoiceDetails = await client1.CreateNewInvoiceDetailsAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("SaveInvoiceDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveInvoiceDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveInvoiceDetails(protoInvoiceDetails aprotoInvoiceDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetails lprotoInvoiceDetails = await client1.SaveInvoiceDetailsAsync(aprotoInvoiceDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveInvoiceDetailsAndSlipDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveInvoiceDetailsAndSlipDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveInvoiceDetailsAndSlipDetails(protoOrderDetails aprotoOrderDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDetails lprotoOrderDetails = await client1.SaveInvoiceDetailsAndSlipDetailsAsync(aprotoOrderDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateInvoiceDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateInvoiceDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateInvoiceDetails(protoInvoiceDetails aprotoInvoiceDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetails lprotoInvoiceDetails = await client1.UpdateInvoiceDetailsAsync(aprotoInvoiceDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenInvoiceDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenInvoiceDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenInvoiceDetails(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetails lprotoInvoiceDetails = await client1.OpenInvoiceDetailsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteInvoiceDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteInvoiceDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteInvoiceDetails(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetails lprotoInvoiceDetails = await client1.DeleteInvoiceDetailsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region InvoiceDetailsItems
        [HttpGet]
        [Route("CreateNewInvoiceDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewInvoiceDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewInvoiceDetailsItems()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetailsItems lprotoInvoiceDetailsItems = await client1.CreateNewInvoiceDetailsItemsAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetailsItems));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("SaveInvoiceDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveInvoiceDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveInvoiceDetailsItems(protoInvoiceDetailsItems aprotoInvoiceDetailsItems)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetailsItems lprotoInvoiceDetailsItems = await client1.SaveInvoiceDetailsItemsAsync(aprotoInvoiceDetailsItems, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetailsItems));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateInvoiceDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateInvoiceDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateInvoiceDetailsItems(protoInvoiceDetailsItems aprotoInvoiceDetailsItems)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetailsItems lprotoInvoiceDetailsItems = await client1.UpdateInvoiceDetailsItemsAsync(aprotoInvoiceDetailsItems, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetailsItems));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenInvoiceDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenInvoiceDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenInvoiceDetailsItems(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetailsItems lprotoInvoiceDetailsItems = await client1.OpenInvoiceDetailsItemsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetailsItems));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteInvoiceDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteInvoiceDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoInvoiceDetailsItemsList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteInvoiceDetailsItems(protoInvoiceDetailsItems aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoInvoiceDetailsItemsList lprotoInvoiceDetailsItems = await client1.DeleteInvoiceDetailsItemsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetailsItems));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region LRDetails
        [HttpGet]
        [Route("CreateNewLRDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewLRDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoLRDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewLRDetails()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoLRDetails lprotoInvoiceDetails = await client1.CreateNewLRDetailsAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveLRDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveLRDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoLRDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveLRDetails(protoLRDetails aprotoLRDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoLRDetails lprotoLRDetails = await client1.SaveLRDetailsAsync(aprotoLRDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoLRDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateLRDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateLRDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoLRDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateLRDetails(protoLRDetails aprotoLRDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoLRDetails lprotoLRDetails = await client1.UpdateLRDetailsAsync(aprotoLRDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoLRDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenLRDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenLRDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoLRDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenLRDetails(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoLRDetails lprotoLRDetails = await client1.OpenLRDetailsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoLRDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteLRDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteLRDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoLRDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteLRDetails(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoLRDetails lprotoLRDetails = await client1.DeleteLRDetailsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoLRDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region OrderDeliverySlipDetails

        [HttpGet]
        [Route("CreateNewOrderDeliverySlipDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewOrderDeliverySlipDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewOrderDeliverySlipDetail()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetail lprotoInvoiceDetails = await client1.CreateNewOrderDeliverySlipDetailAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveOrderDeliverySlipDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveOrderDeliverySlipDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveOrderDeliverySlipDetail(protoOrderDeliverySlipDetail aprotoOrderDeliverySlipDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetail lprotoOrderDeliverySlipDetails = await client1.SaveOrderDeliverySlipDetailAsync(aprotoOrderDeliverySlipDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateOrderDeliverySlipDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateOrderDeliverySlipDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateOrderDeliverySlipDetail(protoOrderDeliverySlipDetail aprotoOrderDeliverySlipDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetail lprotoOrderDeliverySlipDetails = await client1.UpdateOrderDeliverySlipDetailAsync(aprotoOrderDeliverySlipDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenOrderDeliverySlipDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenOrderDeliverySlipDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenOrderDeliverySlipDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetail lprotoOrderDeliverySlipDetails = await client1.OpenOrderDeliverySlipDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteOrderDeliverySlipDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteOrderDeliverySlipDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteOrderDeliverySlipDetail(protoOrderDeliverySlipDetail aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailList lprotoOrderDeliverySlipDetails = await client1.DeleteOrderDeliverySlipDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region OrderDeliverySlipDetailsItems

        [HttpGet]
        [Route("CreateNewOrderDeliverySlipDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewOrderDeliverySlipDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewOrderDeliverySlipDetailsItems()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailsItems lprotoInvoiceDetails = await client1.CreateNewOrderDeliverySlipDetailsItemsAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoInvoiceDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveOrderDeliverySlipDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveOrderDeliverySlipDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveOrderDeliverySlipDetailsItems(protoOrderDeliverySlipDetailsItems aprotoOrderDeliverySlipDetailsItemss)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailsItems lprotoOrderDeliverySlipDetailsItemss = await client1.SaveOrderDeliverySlipDetailsItemsAsync(aprotoOrderDeliverySlipDetailsItemss, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailsItemss));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateOrderDeliverySlipDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateOrderDeliverySlipDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateOrderDeliverySlipDetailsItems(protoOrderDeliverySlipDetailsItems aprotoOrderDeliverySlipDetailsItemss)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailsItems lprotoOrderDeliverySlipDetailsItemss = await client1.UpdateOrderDeliverySlipDetailsItemsAsync(aprotoOrderDeliverySlipDetailsItemss, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailsItemss));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenOrderDeliverySlipDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenOrderDeliverySlipDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenOrderDeliverySlipDetailsItems(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailsItems lprotoOrderDeliverySlipDetailsItemss = await client1.OpenOrderDeliverySlipDetailsItemsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailsItemss));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteOrderDeliverySlipDetailsItems")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteOrderDeliverySlipDetailsItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailsItems))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteOrderDeliverySlipDetailsItems(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailsItems lprotoOrderDeliverySlipDetailsItemss = await client1.DeleteOrderDeliverySlipDetailsItemsAsync(aprotoLongData   , Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailsItemss));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region OrderDetails
        [HttpGet]
        [Route("CreateNewOrderDetailsSearch")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewOrderDetailsSearch")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSearchParams))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewOrderDetailsSearch()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSearchParams lprotoOrderDetailsSearch = await client1.CreateNewOrderDetailsSearchAsync(empty);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetailsSearch));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SearchOrderDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchOrderDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDetailsResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchOrderDetails(protoSearchParams aprotoOrderDetailsSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDetailsResult lprotoOrderDetailsResult = await client1.SearchOrderDetailsAsync(aprotoOrderDetailsSearch);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetailsResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpGet]
        [Route("GetInitialDataForOrder")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetInitialDataForOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(entDDLData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetInitialDataForOrder()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    entDDLData lentDDLData = await client1.GetInitialDataForOrderAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lentDDLData));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("CreateNewOrderDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewOrderDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewOrderDetails(protoStringData aprotoStringData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDetails lprotoOrderDetails = await client1.CreateNewOrderDetailsAsync(aprotoStringData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveOrderDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveOrderDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveOrderDetails(protoOrderDetails aprotoOrderDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDetails lprotoOrderDetails = await client1.SaveOrderDetailsAsync(aprotoOrderDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateOrderDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateOrderDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateOrderDetails(protoOrderDetails aprotoOrderDetails)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDetails lprotoOrderDetails = await client1.UpdateOrderDetailsAsync(aprotoOrderDetails, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenOrderDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenOrderDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenOrderDetails(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDetails lprotoOrderDetails = await client1.OpenOrderDetailsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteOrderDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteOrderDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteOrderDetails(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDetails lprotoOrderDetails = await client1.DeleteOrderDetailsAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Product

        [HttpPost]
        [Route("SearchProduct")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoProductSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchProduct(protoSearchParams aprotoProductSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoProductSearchResult lprotoProductSearchResult = await client1.SearchProductAsync(aprotoProductSearch);
                    return await Task.FromResult(this.GetResponse(lprotoProductSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewProduct")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoProduct))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewProduct()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoProduct lprotoOrderDetails = await client1.CreateNewProductAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveProduct")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoProduct))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveProduct(protoProduct aprotoProduct)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoProduct lprotoProduct = await client1.SaveProductAsync(aprotoProduct, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoProduct));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateProduct")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoProduct))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateProduct(protoProduct aprotoProduct)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoProduct lprotoProduct = await client1.UpdateProductAsync(aprotoProduct, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoProduct));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenProduct")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoProduct))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenProduct(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoProduct lprotoProduct = await client1.OpenProductAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoProduct));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteProduct")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoProduct))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteProduct(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoProduct lprotoProduct = await client1.DeleteProductAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoProduct));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Vendor

        [HttpGet]
        [Route("CreateNewVendorSearch")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewVendorSearch")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSearchParams))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewVendorSearch()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSearchParams lprotoVendorSearch = await client1.CreateNewVendorSearchAsync(empty);
                    return await Task.FromResult(this.GetResponse(lprotoVendorSearch));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SearchVendor")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchVendor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoVendorSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchVendor(protoSearchParams aprotoVendorSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoVendorSearchResult lprotoVendorSearchResult = await client1.SearchVendorAsync(aprotoVendorSearch);
                    return await Task.FromResult(this.GetResponse(lprotoVendorSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpGet]
        [Route("CreateNewVendor")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewVendor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoVendor))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewVendor()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoVendor lprotoOrderDetails = await client1.CreateNewVendorAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveVendor")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveVendor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoVendor))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveVendor(protoVendor aprotoVendor)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoVendor lprotoVendor = await client1.SaveVendorAsync(aprotoVendor, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoVendor));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateVendor")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateVendor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoVendor))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateVendor(protoVendor aprotoVendor)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoVendor lprotoVendor = await client1.UpdateVendorAsync(aprotoVendor, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoVendor));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenVendor")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenVendor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoVendor))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenVendor(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoVendor lprotoVendor = await client1.OpenVendorAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoVendor));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteVendor")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteVendor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoVendor))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteVendor(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoVendor lprotoVendor = await client1.DeleteVendorAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoVendor));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region CompanyBankDetail

        [HttpGet]
        [Route("CreateNewCompanyBankDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewCompanyBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompanyBankDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewCompanyBankDetail()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompanyBankDetail lprotoOrderDetails = await client1.CreateNewCompanyBankDetailAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveCompanyBankDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveCompanyBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompanyBankDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveCompanyBankDetail(protoCompanyBankDetail aprotoCompanyBankDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompanyBankDetail lprotoCompanyBankDetail = await client1.SaveCompanyBankDetailAsync(aprotoCompanyBankDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompanyBankDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateCompanyBankDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateCompanyBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompanyBankDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateCompanyBankDetail(protoCompanyBankDetail aprotoCompanyBankDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompanyBankDetail lprotoCompanyBankDetail = await client1.UpdateCompanyBankDetailAsync(aprotoCompanyBankDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompanyBankDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenCompanyBankDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenCompanyBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompanyBankDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenCompanyBankDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompanyBankDetail lprotoCompanyBankDetail = await client1.OpenCompanyBankDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompanyBankDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteCompanyBankDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteCompanyBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoCompanyBankDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteCompanyBankDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoCompanyBankDetail lprotoCompanyBankDetail = await client1.DeleteCompanyBankDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoCompanyBankDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region WareHouse
        [HttpPost]
        [Route("SearchWareHouse")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchWareHouse")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoWareHouseSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchWareHouse(protoSearchParams aprotoWareHouseSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoWareHouseSearchResult lprotoWareHouseSearchResult = await client1.SearchWareHouseAsync(aprotoWareHouseSearch);
                    return await Task.FromResult(this.GetResponse(lprotoWareHouseSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewWareHouse")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewWareHouse")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoWareHouse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewWareHouse()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoWareHouse lprotoWareHouse = await client1.CreateNewWareHouseAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoWareHouse));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveWareHouse")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveWareHouse")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoWareHouse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveWareHouse(protoWareHouse aprotoWareHouse)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoWareHouse lprotoWareHouse = await client1.SaveWareHouseAsync(aprotoWareHouse, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoWareHouse));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateWareHouse")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateWareHouse")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoWareHouse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateWareHouse(protoWareHouse aprotoWareHouse)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoWareHouse lprotoWareHouse = await client1.UpdateWareHouseAsync(aprotoWareHouse, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoWareHouse));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenWareHouse")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenWareHouse")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoWareHouse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenWareHouse(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoWareHouse lprotoWareHouse = await client1.OpenWareHouseAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoWareHouse));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteWareHouse")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteWareHouse")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoWareHouse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteWareHouse(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoWareHouse lprotoWareHouse = await client1.DeleteWareHouseAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoWareHouse));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region OrderDeliverySlipDetailWithoutInvoice
        [HttpGet ]
        [Route("CreateNewOrderDeliverySlipDetailWithoutInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewOrderDeliverySlipDetailWithoutInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailWithoutInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewOrderDeliverySlipDetailWithoutInvoice()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailWithoutInvoice lprotoOrderDeliverySlipDetailWithoutInvoice = await client1.CreateNewOrderDeliverySlipDetailWithoutInvoiceAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailWithoutInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveOrderDeliverySlipDetailWithoutInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveOrderDeliverySlipDetailWithoutInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailWithoutInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveOrderDeliverySlipDetailWithoutInvoice(protoOrderDeliverySlipDetailWithoutInvoice aprotoOrderDeliverySlipDetailWithoutInvoice)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailWithoutInvoice lprotoOrderDeliverySlipDetailWithoutInvoice = await client1.SaveOrderDeliverySlipDetailWithoutInvoiceAsync(aprotoOrderDeliverySlipDetailWithoutInvoice, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailWithoutInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateOrderDeliverySlipDetailWithoutInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateOrderDeliverySlipDetailWithoutInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailWithoutInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateOrderDeliverySlipDetailWithoutInvoice(protoOrderDeliverySlipDetailWithoutInvoice aprotoOrderDeliverySlipDetailWithoutInvoice)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailWithoutInvoice lprotoOrderDeliverySlipDetailWithoutInvoice = await client1.UpdateOrderDeliverySlipDetailWithoutInvoiceAsync(aprotoOrderDeliverySlipDetailWithoutInvoice, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailWithoutInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenOrderDeliverySlipDetailWithoutInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenOrderDeliverySlipDetailWithoutInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailWithoutInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenOrderDeliverySlipDetailWithoutInvoice(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailWithoutInvoice lprotoOrderDeliverySlipDetailWithoutInvoice = await client1.OpenOrderDeliverySlipDetailWithoutInvoiceAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailWithoutInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteOrderDeliverySlipDetailWithoutInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteOrderDeliverySlipDetailWithoutInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrderDeliverySlipDetailWithoutInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteOrderDeliverySlipDetailWithoutInvoice(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrderDeliverySlipDetailWithoutInvoice lprotoOrderDeliverySlipDetailWithoutInvoice = await client1.DeleteOrderDeliverySlipDetailWithoutInvoiceAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDeliverySlipDetailWithoutInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region RepackingDetail

      
        [HttpGet]
        [Route("CreateNewRepackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewRepackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewRepackingDetail()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoOrderDetails = await client1.CreateNewRepackingDetailAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        } [HttpGet]
        [Route("GetInitialDataForRepaking")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetInitialDataForRepaking")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(entDDLData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetInitialDataForRepaking()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    entDDLData lprotoOrderDetails = await client1.GetInitialDataForRepakingAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveRepackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveRepackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveRepackingDetail(protoRepackingDetail aprotoRepackingDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoRepackingDetail = await client1.SaveRepackingDetailAsync(aprotoRepackingDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateRepackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateRepackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateRepackingDetail(protoRepackingDetail aprotoRepackingDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoRepackingDetail = await client1.UpdateRepackingDetailAsync(aprotoRepackingDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenRepackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenRepackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenRepackingDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoRepackingDetail = await client1.OpenRepackingDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteRepackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteRepackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteRepackingDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoRepackingDetail = await client1.DeleteRepackingDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("GetOrderDetailsForRepaking")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetOrderDetailsForRepaking")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetOrderDetailsForRepaking(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoRepackingDetail = await client1.GetOrderDetailsForRepakingAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewRepackingDetailSearch")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewRepackingDetailSearch")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSearchParams))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewRepackingDetailSearch()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSearchParams lprotoCustomerSearch = await client1.CreateNewRepackingDetailSearchAsync(empty);
                    return await Task.FromResult(this.GetResponse(lprotoCustomerSearch));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SearchRepackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchRepackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetailResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchRepackingDetail(protoSearchParams aprotoRepackingDetailSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetailResult lprotoRepackingDetailResult = await client1.SearchRepackingDetailAsync(aprotoRepackingDetailSearch);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetailResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("GetOrderDetailsBasedOnValues")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetOrderDetailsBasedOnValues")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetOrderDetailsBasedOnValues(entPassingParam aentPassingParam)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoRepackingDetail = await client1.GetOrderDetailsBasedOnValuesAsync(aentPassingParam, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("UpdateApprovedRepackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateApprovedRepackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateApprovedRepackingDetail(protoRepackingDetail aentPassingParam)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingDetail lprotoRepackingDetail = await client1.UpdateApprovedRepackingDetailAsync(aentPassingParam, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region RepackingListDetail

        [HttpPost]
        [Route("CreateNewRepackingListDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewRepackingListDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingListDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewRepackingListDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingListDetail lprotoOrderDetails = await client1.CreateNewRepackingListDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveRepackingListDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveRepackingListDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingListDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveRepackingListDetail(protoRepackingListDetail aprotoRepackingListDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingListDetail lprotoRepackingListDetail = await client1.SaveRepackingListDetailAsync(aprotoRepackingListDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingListDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateRepackingListDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateRepackingListDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingListDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateRepackingListDetail(protoRepackingListDetail aprotoRepackingListDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingListDetail lprotoRepackingListDetail = await client1.UpdateRepackingListDetailAsync(aprotoRepackingListDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingListDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenRepackingListDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenRepackingListDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingListDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenRepackingListDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingListDetail lprotoRepackingListDetail = await client1.OpenRepackingListDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingListDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteRepackingListDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteRepackingListDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRepackingListDetailList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteRepackingListDetail(protoRepackingListDetail aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRepackingListDetailList lprotoRepackingListDetail = await client1.DeleteRepackingListDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRepackingListDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region FinalPackingDetail
        [HttpPost]
        [Route("SearchFinalPackingDetails")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchFinalPackingDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoFinalPackingDetailsSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchFinalPackingDetails(protoSearchParams aprotoFinalPackingDetailsSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoFinalPackingDetailsSearchResult lprotoFinalPackingDetailsSearchResult = await client1.SearchFinalPackingDetailsAsync(aprotoFinalPackingDetailsSearch);
                    return await Task.FromResult(this.GetResponse(lprotoFinalPackingDetailsSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpGet]
        [Route("CreateNewFinalPackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewFinalPackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoFinalPackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewFinalPackingDetail()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoFinalPackingDetail lprotoOrderDetails = await client1.CreateNewFinalPackingDetailAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveFinalPackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveFinalPackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoFinalPackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveFinalPackingDetail(protoFinalPackingDetail aprotoFinalPackingDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoFinalPackingDetail lprotoFinalPackingDetail = await client1.SaveFinalPackingDetailAsync(aprotoFinalPackingDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoFinalPackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateFinalPackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateFinalPackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoFinalPackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateFinalPackingDetail(protoFinalPackingDetail aprotoFinalPackingDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoFinalPackingDetail lprotoFinalPackingDetail = await client1.UpdateFinalPackingDetailAsync(aprotoFinalPackingDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoFinalPackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenFinalPackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenFinalPackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoFinalPackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenFinalPackingDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoFinalPackingDetail lprotoFinalPackingDetail = await client1.OpenFinalPackingDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoFinalPackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteFinalPackingDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteFinalPackingDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoFinalPackingDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteFinalPackingDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoFinalPackingDetail lprotoFinalPackingDetail = await client1.DeleteFinalPackingDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoFinalPackingDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region ExportInvoice


        [HttpPost]
        [Route("SearchExportInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchExportInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoiceSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchExportInvoice(protoSearchParams aprotoCustomerSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoiceSearchResult lprotoCustomerSearchResult = await client1.SearchExportInvoiceAsync(aprotoCustomerSearch);
                    return await Task.FromResult(this.GetResponse(lprotoCustomerSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewExportInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewExportInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewExportInvoice()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoice lprotoOrderDetails = await client1.CreateNewExportInvoiceAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveExportInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveExportInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveExportInvoice(protoExportInvoice aprotoExportInvoice)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoice lprotoExportInvoice = await client1.SaveExportInvoiceAsync(aprotoExportInvoice, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateExportInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateExportInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateExportInvoice(protoExportInvoice aprotoExportInvoice)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoice lprotoExportInvoice = await client1.UpdateExportInvoiceAsync(aprotoExportInvoice, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenExportInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenExportInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenExportInvoice(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoice lprotoExportInvoice = await client1.OpenExportInvoiceAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteExportInvoice")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteExportInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteExportInvoice(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoice lprotoExportInvoice = await client1.DeleteExportInvoiceAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoice));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region ExportInvoiceDetail

        [HttpGet]
        [Route("CreateNewExportInvoiceDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewExportInvoiceDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoiceDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewExportInvoiceDetail()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoiceDetail lprotoOrderDetails = await client1.CreateNewExportInvoiceDetailAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveExportInvoiceDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveExportInvoiceDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoiceDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveExportInvoiceDetail(protoExportInvoiceDetail aprotoExportInvoiceDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoiceDetail lprotoExportInvoiceDetail = await client1.SaveExportInvoiceDetailAsync(aprotoExportInvoiceDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoiceDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("GetInStockInvoiceDetailBasedPakgNO")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetInStockInvoiceDetailBasedPakgNO")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoiceDetailList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetInStockInvoiceDetailBasedPakgNO(protoStringData aprotoStringData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoiceDetailList lprotoExportInvoiceDetail = await client1.GetInStockInvoiceDetailBasedPakgNOAsync(aprotoStringData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoiceDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateExportInvoiceDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateExportInvoiceDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoiceDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateExportInvoiceDetail(protoExportInvoiceDetail aprotoExportInvoiceDetail)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoiceDetail lprotoExportInvoiceDetail = await client1.UpdateExportInvoiceDetailAsync(aprotoExportInvoiceDetail, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoiceDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenExportInvoiceDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenExportInvoiceDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoiceDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenExportInvoiceDetail(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoiceDetail lprotoExportInvoiceDetail = await client1.OpenExportInvoiceDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoiceDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteExportInvoiceDetail")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteExportInvoiceDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportInvoiceDetailList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteExportInvoiceDetail(protoExportInvoiceDetail aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportInvoiceDetailList lprotoExportInvoiceDetail = await client1.DeleteExportInvoiceDetailAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportInvoiceDetail));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Consignee

        [HttpPost]
        [Route("SearchConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoConsigneeSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchConsignee(protoSearchParams aprotoConsigneeSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoConsigneeSearchResult lprotoConsigneeSearchResult = await client1.SearchConsigneeAsync(aprotoConsigneeSearch);
                    return await Task.FromResult(this.GetResponse(lprotoConsigneeSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewConsignee()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoConsignee lprotoOrderDetails = await client1.CreateNewConsigneeAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveConsignee(protoConsignee aprotoConsignee)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoConsignee lprotoConsignee = await client1.SaveConsigneeAsync(aprotoConsignee, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoConsignee));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateConsignee(protoConsignee aprotoConsignee)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoConsignee lprotoConsignee = await client1.UpdateConsigneeAsync(aprotoConsignee, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoConsignee));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenConsignee(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoConsignee lprotoConsignee = await client1.OpenConsigneeAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoConsignee));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteConsignee(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoConsignee lprotoConsignee = await client1.DeleteConsigneeAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoConsignee));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Buyer

        [HttpPost]
        [Route("SearchBuyer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchBuyer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoBuyerSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchBuyer(protoSearchParams aprotoBuyerSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoBuyerSearchResult lprotoBuyerSearchResult = await client1.SearchBuyerAsync(aprotoBuyerSearch);
                    return await Task.FromResult(this.GetResponse(lprotoBuyerSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewBuyer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewBuyer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoBuyer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewBuyer()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoBuyer lprotoOrderDetails = await client1.CreateNewBuyerAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveBuyer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveBuyer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoBuyer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveBuyer(protoBuyer aprotoBuyer)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoBuyer lprotoBuyer = await client1.SaveBuyerAsync(aprotoBuyer, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoBuyer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateBuyer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateBuyer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoBuyer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateBuyer(protoBuyer aprotoBuyer)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoBuyer lprotoBuyer = await client1.UpdateBuyerAsync(aprotoBuyer, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoBuyer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenBuyer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenBuyer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoBuyer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenBuyer(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoBuyer lprotoBuyer = await client1.OpenBuyerAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoBuyer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteBuyer")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteBuyer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoBuyer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteBuyer(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoBuyer lprotoBuyer = await client1.DeleteBuyerAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoBuyer));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Preorder

        [HttpPost]
        [Route("SearchPreorder")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchPreorder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoPreorderSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchPreorder(protoSearchParams aprotoPreorderSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoPreorderSearchResult lprotoPreorderSearchResult = await client1.SearchPreorderAsync(aprotoPreorderSearch);
                    return await Task.FromResult(this.GetResponse(lprotoPreorderSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewPreorder")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewPreorder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoPreorder))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewPreorder()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoPreorder lprotoOrderDetails = await client1.CreateNewPreorderAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SavePreorder")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SavePreorder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoPreorder))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SavePreorder(protoPreorder aprotoPreorder)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoPreorder lprotoPreorder = await client1.SavePreorderAsync(aprotoPreorder, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoPreorder));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdatePreorder")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdatePreorder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoPreorder))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdatePreorder(protoPreorder aprotoPreorder)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoPreorder lprotoPreorder = await client1.UpdatePreorderAsync(aprotoPreorder, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoPreorder));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenPreorder")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenPreorder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoPreorder))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenPreorder(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoPreorder lprotoPreorder = await client1.OpenPreorderAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoPreorder));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeletePreorder")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeletePreorder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoPreorder))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeletePreorder(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoPreorder lprotoPreorder = await client1.DeletePreorderAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoPreorder));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("GetTodayPreorders")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetTodayPreorders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoPreorderList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetTodayPreorders(protoStringData aprotoStringData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoPreorderList lprotoPreorder = await client1.GetTodayPreordersAsync(aprotoStringData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoPreorder));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion


        #region Exporter

        [HttpPost]
        [Route("SearchExporter")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchExporter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExporterSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchExporter(protoSearchParams aprotoExporterSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExporterSearchResult lprotoExporterSearchResult = await client1.SearchExporterAsync(aprotoExporterSearch);
                    return await Task.FromResult(this.GetResponse(lprotoExporterSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewExporter")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewExporter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExporter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewExporter()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExporter lprotoOrderDetails = await client1.CreateNewExporterAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveExporter")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveExporter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExporter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveExporter(protoExporter aprotoExporter)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExporter lprotoExporter = await client1.SaveExporterAsync(aprotoExporter, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExporter));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateExporter")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateExporter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExporter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateExporter(protoExporter aprotoExporter)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExporter lprotoExporter = await client1.UpdateExporterAsync(aprotoExporter, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExporter));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenExporter")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenExporter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExporter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenExporter(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExporter lprotoExporter = await client1.OpenExporterAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExporter));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteExporter")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteExporter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExporter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteExporter(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExporter lprotoExporter = await client1.DeleteExporterAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExporter));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Party

        [HttpPost]
        [Route("SearchParty")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchParty")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoPartySearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchParty(protoSearchParams aprotoPartySearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoPartySearchResult lprotoPartySearchResult = await client1.SearchPartyAsync(aprotoPartySearch);
                    return await Task.FromResult(this.GetResponse(lprotoPartySearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewParty")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewParty")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoParty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewParty()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoParty lprotoOrderDetails = await client1.CreateNewPartyAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveParty")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveParty")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoParty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveParty(protoParty aprotoParty)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoParty lprotoParty = await client1.SavePartyAsync(aprotoParty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoParty));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateParty")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateParty")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoParty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateParty(protoParty aprotoParty)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoParty lprotoParty = await client1.UpdatePartyAsync(aprotoParty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoParty));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenParty")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenParty")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoParty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenParty(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoParty lprotoParty = await client1.OpenPartyAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoParty));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteParty")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteParty")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoParty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteParty(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoParty lprotoParty = await client1.DeletePartyAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoParty));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region ExportConsignee

        [HttpPost]
        [Route("SearchExportConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SearchExportConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportConsigneeSearchResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SearchExportConsignee(protoSearchParams aprotoExportConsigneeSearch)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportConsigneeSearchResult lprotoExportConsigneeSearchResult = await client1.SearchExportConsigneeAsync(aprotoExportConsigneeSearch);
                    return await Task.FromResult(this.GetResponse(lprotoExportConsigneeSearchResult));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("CreateNewExportConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "CreateNewExportConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewExportConsignee()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportConsignee lprotoOrderDetails = await client1.CreateNewExportConsigneeAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveExportConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "SaveExportConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveExportConsignee(protoExportConsignee aprotoExportConsignee)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportConsignee lprotoExportConsignee = await client1.SaveExportConsigneeAsync(aprotoExportConsignee, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportConsignee));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateExportConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "UpdateExportConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateExportConsignee(protoExportConsignee aprotoExportConsignee)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportConsignee lprotoExportConsignee = await client1.UpdateExportConsigneeAsync(aprotoExportConsignee, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportConsignee));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenExportConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "OpenExportConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenExportConsignee(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportConsignee lprotoExportConsignee = await client1.OpenExportConsigneeAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportConsignee));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteExportConsignee")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "DeleteExportConsignee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoExportConsignee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteExportConsignee(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoExportConsignee lprotoExportConsignee = await client1.DeleteExportConsigneeAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoExportConsignee));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        [HttpPost]
        [Route("GenerateExportInvoiceExcel")]
        [SwaggerOperation(Tags = new[] { "SalesReport" }, Summary = "GenerateExportInvoiceExcel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(entByteData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GenerateExportInvoiceExcel(protoStringData2 request)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    entByteData response = await client1.GenerateExportInvoiceExcelAsync(request, Metadata);
                    if (response.ByteData != null)
                    {
                        MemoryStream ms = new MemoryStream(response.ByteData.ToByteArray());
                        return await Task.FromResult(new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
                    }
                    else if (request != null)
                    {
                        return await Task.FromResult(this.GetResponse(response));
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        } [HttpPost]
        [Route("GenerateExportInvoiceWithoutPakageNoExcel")]
        [SwaggerOperation(Tags = new[] { "SalesReport" }, Summary = "GenerateExportInvoiceWithoutPakageNoExcel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(entByteData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GenerateExportInvoiceWithoutPakageNoExcel(protoStringData2 request)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    entByteData response = await client1.GenerateExportInvoiceWithoutPakageNoExcelAsync(request, Metadata);
                    if (response.ByteData != null)
                    {
                        MemoryStream ms = new MemoryStream(response.ByteData.ToByteArray());
                        return await Task.FromResult(new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
                    }
                    else if (request != null)
                    {
                        return await Task.FromResult(this.GetResponse(response));
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("GetOrdersDetailsMonthWise")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetOrdersDetailsMonthWise")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoOrdersMonthWiseList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetOrdersDetailsMonthWise(entPassingParam aentPassingParam)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoOrdersMonthWiseList lprotoOrdersMonthWiseList = await client1.GetOrdersDetailsMonthWiseAsync(aentPassingParam, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrdersMonthWiseList));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("GetOrdersDetailsForDashBoard")]
        [SwaggerOperation(Tags = new[] { "Application" }, Summary = "GetOrdersDetailsForDashBoard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoStringData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> GetOrdersDetailsForDashBoard(protoLongData data)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new ApplicationService.ApplicationServiceClient(channel1);
                    Empty empty = new Empty();
                    protoStringData lprotoStringData = await client1.GetOrdersDetailsForDashBoardAsync(data, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoStringData));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
    }
}
