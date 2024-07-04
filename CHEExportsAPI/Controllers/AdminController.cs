using CHEExportsProto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CHEExportsAPI
{
    [ApiController]
    [Route("Admin")]
    public class AdminController : ControllerBase
    {
        private Metadata Metadata = new Metadata();
        private readonly string Token;
        public const string SERVICE_ENDPOINT = "http://localhost:5243";
        public AdminController(IHttpContextAccessor context)
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
        [Route("AuthenticateUser")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "AuthenticateUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoStringData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> AuthenticateUser(protoLoginCredentials aprotoLoginCredentials)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoLoggedInDetails lprotoLoggedInDetails = await client1.AuthenticateUserAsync(aprotoLoginCredentials,Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoLoggedInDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("SaveUser")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUser))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveUser(protoUser aprotoUser)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUser lprotoUser = await client1.SaveUserAsync(aprotoUser);
                    return await Task.FromResult(this.GetResponse(lprotoUser));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("UpdateUser")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUser))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateUser(protoUser aprotoUser)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUser lprotoUser = await client1.UpdateUserAsync(aprotoUser);
                    return await Task.FromResult(this.GetResponse(lprotoUser));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenUser")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUser))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenUser(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUser lprotoUser = await client1.OpenUserAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoUser));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteUser")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUser))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteUser(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUser lprotoUser = await client1.DeleteUserAsync(aprotoLongData);
                    return await Task.FromResult(this.GetResponse(lprotoUser));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #region MasterConfig
        [HttpGet]
        [Route("CreateNewMasterConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewMasterConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMasterConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewMasterConfig()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMasterConfig lprotoMasterConfig = await client1.CreateNewMasterConfigAsync(empty);
                    return await Task.FromResult(this.GetResponse(lprotoMasterConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveMasterConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveMasterConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMasterConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveMasterConfig(protoMasterConfig aprotoMasterConfig)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMasterConfig lprotoMasterConfig = await client1.SaveMasterConfigAsync(aprotoMasterConfig);
                    return await Task.FromResult(this.GetResponse(lprotoMasterConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateMasterConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateMasterConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMasterConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateMasterConfig(protoMasterConfig aprotoMasterConfig)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMasterConfig lprotoMasterConfig = await client1.UpdateMasterConfigAsync(aprotoMasterConfig);
                    return await Task.FromResult(this.GetResponse(lprotoMasterConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenMasterConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenMasterConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMasterConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenMasterConfig(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMasterConfig lprotoMasterConfig = await client1.OpenMasterConfigAsync(aprotoLongData);
                    return await Task.FromResult(this.GetResponse(lprotoMasterConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteMasterConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteMasterConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMasterConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteMasterConfig(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMasterConfig lprotoMasterConfig = await client1.DeleteMasterConfigAsync(aprotoLongData);
                    return await Task.FromResult(this.GetResponse(lprotoMasterConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region MessageConfig
        [HttpGet]
        [Route("CreateNewMessageConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewMessageConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMessageConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewMessageConfig()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMessageConfig lprotoMessageConfig = await client1.CreateNewMessageConfigAsync(empty);
                    return await Task.FromResult(this.GetResponse(lprotoMessageConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveMessageConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveMessageConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMessageConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveMessageConfig(protoMessageConfig aprotoMessageConfig)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMessageConfig lprotoMessageConfig = await client1.SaveMessageConfigAsync(aprotoMessageConfig);
                    return await Task.FromResult(this.GetResponse(lprotoMessageConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateMessageConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateMessageConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMessageConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateMessageConfig(protoMessageConfig aprotoMessageConfig)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMessageConfig lprotoMessageConfig = await client1.UpdateMessageConfigAsync(aprotoMessageConfig);
                    return await Task.FromResult(this.GetResponse(lprotoMessageConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenMessageConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenMessageConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMessageConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenMessageConfig(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMessageConfig lprotoMessageConfig = await client1.OpenMessageConfigAsync(aprotoLongData);
                    return await Task.FromResult(this.GetResponse(lprotoMessageConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteMessageConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteMessageConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoMessageConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteMessageConfig(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoMessageConfig lprotoMessageConfig = await client1.DeleteMessageConfigAsync(aprotoLongData);
                    return await Task.FromResult(this.GetResponse(lprotoMessageConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region SubConfig
        [HttpGet]
        [Route("CreateNewSubConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewSubConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSubConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewSubConfig()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSubConfig lprotoSubConfig = await client1.CreateNewSubConfigAsync(empty);
                    return await Task.FromResult(this.GetResponse(lprotoSubConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("SaveSubConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveSubConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSubConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveSubConfig(protoSubConfig aprotoSubConfig)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSubConfig lprotoSubConfig = await client1.SaveSubConfigAsync(aprotoSubConfig);
                    return await Task.FromResult(this.GetResponse(lprotoSubConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateSubConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateSubConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSubConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateSubConfig(protoSubConfig aprotoSubConfig)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSubConfig lprotoSubConfig = await client1.UpdateSubConfigAsync(aprotoSubConfig);
                    return await Task.FromResult(this.GetResponse(lprotoSubConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenSubConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenSubConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSubConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenSubConfig(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSubConfig lprotoSubConfig = await client1.OpenSubConfigAsync(aprotoLongData);
                    return await Task.FromResult(this.GetResponse(lprotoSubConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteSubConfig")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteSubConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoSubConfig))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteSubConfig(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoSubConfig lprotoSubConfig = await client1.DeleteSubConfigAsync(aprotoLongData);
                    return await Task.FromResult(this.GetResponse(lprotoSubConfig));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Group

        //[HttpPost]
        //[Route("SearchGroup")]
        //[SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SearchGroup")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoGroupSearchResult))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        //public async Task<IActionResult> SearchGroup(protoSearchParams aprotoGroupSearch)
        //{
        //    try
        //    {
        //        using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
        //        {
        //            var client1 = new AdminService.AdminServiceClient(channel1);
        //            Empty empty = new Empty();
        //            protoGroupSearchResult lprotoGroupSearchResult = await client1.SearchGroupAsync(aprotoGroupSearch);
        //            return await Task.FromResult(this.GetResponse(lprotoGroupSearchResult));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Task.FromResult(this.GetErrorMessage(ex));
        //    }
        //}

        [HttpGet]
        [Route("CreateNewGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewGroup()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoGroup lprotoOrderDetails = await client1.CreateNewGroupAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveGroup(protoGroup aprotoGroup)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoGroup lprotoGroup = await client1.SaveGroupAsync(aprotoGroup, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateGroup(protoGroup aprotoGroup)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoGroup lprotoGroup = await client1.UpdateGroupAsync(aprotoGroup, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenGroup(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoGroup lprotoGroup = await client1.OpenGroupAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteGroup(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoGroup lprotoGroup = await client1.DeleteGroupAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region UserGroup

        //[HttpPost]
        //[Route("SearchUserGroup")]
        //[SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SearchUserGroup")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUserGroupSearchResult))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        //public async Task<IActionResult> SearchUserGroup(protoSearchParams aprotoUserGroupSearch)
        //{
        //    try
        //    {
        //        using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
        //        {
        //            var client1 = new AdminService.AdminServiceClient(channel1);
        //            Empty empty = new Empty();
        //            protoUserGroupSearchResult lprotoUserGroupSearchResult = await client1.SearchUserGroupAsync(aprotoUserGroupSearch);
        //            return await Task.FromResult(this.GetResponse(lprotoUserGroupSearchResult));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Task.FromResult(this.GetErrorMessage(ex));
        //    }
        //}

        [HttpGet]
        [Route("CreateNewUserGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewUserGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUserGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewUserGroup()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUserGroup lprotoOrderDetails = await client1.CreateNewUserGroupAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveUserGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveUserGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUserGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveUserGroup(protoUserGroup aprotoUserGroup)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUserGroup lprotoUserGroup = await client1.SaveUserGroupAsync(aprotoUserGroup, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoUserGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateUserGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateUserGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUserGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateUserGroup(protoUserGroup aprotoUserGroup)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUserGroup lprotoUserGroup = await client1.UpdateUserGroupAsync(aprotoUserGroup, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoUserGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenUserGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenUserGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUserGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenUserGroup(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUserGroup lprotoUserGroup = await client1.OpenUserGroupAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoUserGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteUserGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteUserGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoUserGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteUserGroup(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoUserGroup lprotoUserGroup = await client1.DeleteUserGroupAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoUserGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Role

        //[HttpPost]
        //[Route("SearchRole")]
        //[SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SearchRole")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleSearchResult))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        //public async Task<IActionResult> SearchRole(protoSearchParams aprotoRoleSearch)
        //{
        //    try
        //    {
        //        using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
        //        {
        //            var client1 = new AdminService.AdminServiceClient(channel1);
        //            Empty empty = new Empty();
        //            protoRoleSearchResult lprotoRoleSearchResult = await client1.SearchRoleAsync(aprotoRoleSearch);
        //            return await Task.FromResult(this.GetResponse(lprotoRoleSearchResult));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Task.FromResult(this.GetErrorMessage(ex));
        //    }
        //}

        [HttpGet]
        [Route("CreateNewRole")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRole))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewRole()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRole lprotoOrderDetails = await client1.CreateNewRoleAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveRole")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRole))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveRole(protoRole aprotoRole)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRole lprotoRole = await client1.SaveRoleAsync(aprotoRole, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRole));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateRole")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRole))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateRole(protoRole aprotoRole)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRole lprotoRole = await client1.UpdateRoleAsync(aprotoRole, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRole));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenRole")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRole))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenRole(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRole lprotoRole = await client1.OpenRoleAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRole));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteRole")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRole))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteRole(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRole lprotoRole = await client1.DeleteRoleAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRole));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region RoleGroup

        //[HttpPost]
        //[Route("SearchRoleGroup")]
        //[SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SearchRoleGroup")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleGroupSearchResult))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        //public async Task<IActionResult> SearchRoleGroup(protoSearchParams aprotoRoleGroupSearch)
        //{
        //    try
        //    {
        //        using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
        //        {
        //            var client1 = new AdminService.AdminServiceClient(channel1);
        //            Empty empty = new Empty();
        //            protoRoleGroupSearchResult lprotoRoleGroupSearchResult = await client1.SearchRoleGroupAsync(aprotoRoleGroupSearch);
        //            return await Task.FromResult(this.GetResponse(lprotoRoleGroupSearchResult));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Task.FromResult(this.GetErrorMessage(ex));
        //    }
        //}

        [HttpGet]
        [Route("CreateNewRoleGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewRoleGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewRoleGroup()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleGroup lprotoOrderDetails = await client1.CreateNewRoleGroupAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveRoleGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveRoleGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveRoleGroup(protoRoleGroup aprotoRoleGroup)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleGroup lprotoRoleGroup = await client1.SaveRoleGroupAsync(aprotoRoleGroup, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRoleGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateRoleGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateRoleGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateRoleGroup(protoRoleGroup aprotoRoleGroup)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleGroup lprotoRoleGroup = await client1.UpdateRoleGroupAsync(aprotoRoleGroup, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRoleGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenRoleGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenRoleGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenRoleGroup(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleGroup lprotoRoleGroup = await client1.OpenRoleGroupAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRoleGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteRoleGroup")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteRoleGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleGroup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteRoleGroup(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleGroup lprotoRoleGroup = await client1.DeleteRoleGroupAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRoleGroup));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region Resource

        //[HttpPost]
        //[Route("SearchResource")]
        //[SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SearchResource")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoResourceSearchResult))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        //public async Task<IActionResult> SearchResource(protoSearchParams aprotoResourceSearch)
        //{
        //    try
        //    {
        //        using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
        //        {
        //            var client1 = new AdminService.AdminServiceClient(channel1);
        //            Empty empty = new Empty();
        //            protoResourceSearchResult lprotoResourceSearchResult = await client1.SearchResourceAsync(aprotoResourceSearch);
        //            return await Task.FromResult(this.GetResponse(lprotoResourceSearchResult));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Task.FromResult(this.GetErrorMessage(ex));
        //    }
        //}

        [HttpGet]
        [Route("CreateNewResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewResource()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoResource lprotoOrderDetails = await client1.CreateNewResourceAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveResource(protoResource aprotoResource)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoResource lprotoResource = await client1.SaveResourceAsync(aprotoResource, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoResource));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateResource(protoResource aprotoResource)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoResource lprotoResource = await client1.UpdateResourceAsync(aprotoResource, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoResource));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenResource(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoResource lprotoResource = await client1.OpenResourceAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoResource));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteResource(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoResource lprotoResource = await client1.DeleteResourceAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoResource));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion

        #region RoleResource

        //[HttpPost]
        //[Route("SearchRoleResource")]
        //[SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SearchRoleResource")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleResourceSearchResult))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        //public async Task<IActionResult> SearchRoleResource(protoSearchParams aprotoRoleResourceSearch)
        //{
        //    try
        //    {
        //        using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
        //        {
        //            var client1 = new AdminService.AdminServiceClient(channel1);
        //            Empty empty = new Empty();
        //            protoRoleResourceSearchResult lprotoRoleResourceSearchResult = await client1.SearchRoleResourceAsync(aprotoRoleResourceSearch);
        //            return await Task.FromResult(this.GetResponse(lprotoRoleResourceSearchResult));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Task.FromResult(this.GetErrorMessage(ex));
        //    }
        //}

        [HttpGet]
        [Route("CreateNewRoleResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "CreateNewRoleResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> CreateNewRoleResource()
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleResource lprotoOrderDetails = await client1.CreateNewRoleResourceAsync(empty, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoOrderDetails));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        [HttpPost]
        [Route("SaveRoleResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "SaveRoleResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> SaveRoleResource(protoRoleResource aprotoRoleResource)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleResource lprotoRoleResource = await client1.SaveRoleResourceAsync(aprotoRoleResource, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRoleResource));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }

        [HttpPost]
        [Route("UpdateRoleResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "UpdateRoleResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> UpdateRoleResource(protoRoleResource aprotoRoleResource)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleResource lprotoRoleResource = await client1.UpdateRoleResourceAsync(aprotoRoleResource, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRoleResource));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("OpenRoleResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "OpenRoleResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> OpenRoleResource(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleResource lprotoRoleResource = await client1.OpenRoleResourceAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRoleResource));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }


        [HttpPost]
        [Route("DeleteRoleResource")]
        [SwaggerOperation(Tags = new[] { "Admin" }, Summary = "DeleteRoleResource")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(protoRoleResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(protoMessage))]
        public async Task<IActionResult> DeleteRoleResource(protoLongData aprotoLongData)
        {
            try
            {
                using (var channel1 = GrpcChannel.ForAddress(SERVICE_ENDPOINT))
                {
                    var client1 = new AdminService.AdminServiceClient(channel1);
                    Empty empty = new Empty();
                    protoRoleResource lprotoRoleResource = await client1.DeleteRoleResourceAsync(aprotoLongData, Metadata);
                    return await Task.FromResult(this.GetResponse(lprotoRoleResource));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(this.GetErrorMessage(ex));
            }
        }
        #endregion
    }
}
