using CHEExportsDataAccessLayer;
using CHEExportsDataObjects;
using CHEExportsProto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace CHEExportsService
{
    public class AdminService : CHEExportsProto.AdminService.AdminServiceBase
    {
        private readonly string token = string.Empty;
        public AdminService()
        {

        }

        [AllowAnonymous]
        public override async Task<protoLoggedInDetails> AuthenticateUser(protoLoginCredentials aprotoLoginCredentials, ServerCallContext context)
        {
            protoLoggedInDetails lprotoLoggedInDetails = new protoLoggedInDetails();
            try
            {
                if (aprotoLoginCredentials != null && !string.IsNullOrEmpty(aprotoLoginCredentials.UserLoginId) && !string.IsNullOrEmpty(aprotoLoginCredentials.Password))
                {
                    User user = new User();
                    user.user_login_id = aprotoLoginCredentials.UserLoginId;
                    user.keyword = aprotoLoginCredentials.Password;
                    DALUser lDALUser = new DALUser(user);
                    lprotoLoggedInDetails = lDALUser.AuthenticateUser();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lprotoLoggedInDetails);
        }

        public override async Task<protoUser> CreateNewUser(Empty empty, ServerCallContext context)
        {
            protoUser lprotoUser = new protoUser();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lprotoUser);
        }

        public override async Task<protoUser> SaveUser(protoUser aprotoUser, ServerCallContext context)
        {
            User user = new User();
            try
            {
                user.GetData(aprotoUser);
                DALUser lDALUser = new DALUser(user);
                lDALUser.SaveUser("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(user.GetProto());
        }

        public override async Task<protoUser> UpdateUser(protoUser aprotoUser, ServerCallContext context)
        {
            User user = new User();
            try
            {
                user.GetData(aprotoUser);
                DALUser lDALUser = new DALUser(user);
                lDALUser.UpdateUser("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(user.GetProto());
        }

        public override async Task<protoUser> OpenUser(protoLongData aprotoLongData, ServerCallContext context)
        {
            User user = new User();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    user.user_id = aprotoLongData.Data;
                    DALUser lDALUser = new DALUser(user);
                    lDALUser.OpenUser(context.RequestHeaders.GetValue("authorization"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(user.GetProto());
        }

        public override async Task<protoUser> DeleteUser(protoLongData aprotoLongData, ServerCallContext context)
        {
            User user = new User();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    user.user_id = aprotoLongData.Data;
                    DALUser lDALUser = new DALUser(user);
                    lDALUser.DeleteUser("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(user.GetProto());
        }
        #region MessageConfig
        public override async Task<protoMessageConfig> CreateNewMessageConfig(Empty empty, ServerCallContext context)
        {
            protoMessageConfig lprotoMessageConfig = new protoMessageConfig();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lprotoMessageConfig);
        }

        public override async Task<protoMessageConfig> SaveMessageConfig(protoMessageConfig aprotoMessageConfig, ServerCallContext context)
        {
            MessageConfig MessageConfig = new MessageConfig();
            try
            {
                MessageConfig.GetData(aprotoMessageConfig);
                DALMessageConfig lDALMessageConfig = new DALMessageConfig(MessageConfig);
                lDALMessageConfig.SaveMessage("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(MessageConfig.GetProto());
        }

        public override async Task<protoMessageConfig> UpdateMessageConfig(protoMessageConfig aprotoMessageConfig, ServerCallContext context)
        {
            MessageConfig MessageConfig = new MessageConfig();
            try
            {
                MessageConfig.GetData(aprotoMessageConfig);
                DALMessageConfig lDALMessageConfig = new DALMessageConfig(MessageConfig);
                lDALMessageConfig.UpdateMessage("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(MessageConfig.GetProto());
        }

        public override async Task<protoMessageConfig> OpenMessageConfig(protoLongData aprotoLongData, ServerCallContext context)
        {
            MessageConfig MessageConfig = new MessageConfig();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    MessageConfig.message_id = aprotoLongData.Data;
                    DALMessageConfig lDALMessageConfig = new DALMessageConfig(MessageConfig);
                    lDALMessageConfig.OpenMessage("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(MessageConfig.GetProto());
        }

        public override async Task<protoMessageConfig> DeleteMessageConfig(protoLongData aprotoLongData, ServerCallContext context)
        {
            MessageConfig MessageConfig = new MessageConfig();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    MessageConfig.message_id = aprotoLongData.Data;
                    DALMessageConfig lDALMessageConfig = new DALMessageConfig(MessageConfig);
                    lDALMessageConfig.DeleteMessage("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(MessageConfig.GetProto());
        }
        #endregion

        #region SubConfig
        public override async Task<protoSubConfig> CreateNewSubConfig(Empty empty, ServerCallContext context)
        {
            protoSubConfig lprotoSubConfig = new protoSubConfig();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lprotoSubConfig);
        }

        public override async Task<protoSubConfig> SaveSubConfig(protoSubConfig aprotoSubConfig, ServerCallContext context)
        {
            SubConfig SubConfig = new SubConfig();
            try
            {
                SubConfig.GetData(aprotoSubConfig);
                DALSubConfig lDALSubConfig = new DALSubConfig(SubConfig);
                lDALSubConfig.SaveSubConfig("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(SubConfig.GetProto());
        }

        public override async Task<protoSubConfig> UpdateSubConfig(protoSubConfig aprotoSubConfig, ServerCallContext context)
        {
            SubConfig SubConfig = new SubConfig();
            try
            {
                SubConfig.GetData(aprotoSubConfig);
                DALSubConfig lDALSubConfig = new DALSubConfig(SubConfig);
                lDALSubConfig.UpdateSubConfig("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(SubConfig.GetProto());
        }

        public override async Task<protoSubConfig> OpenSubConfig(protoLongData aprotoLongData, ServerCallContext context)
        {
            SubConfig SubConfig = new SubConfig();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    SubConfig.sub_config_id = aprotoLongData.Data;
                    DALSubConfig lDALSubConfig = new DALSubConfig(SubConfig);
                    lDALSubConfig.OpenSubConfig("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(SubConfig.GetProto());
        }

        public override async Task<protoSubConfig> DeleteSubConfig(protoLongData aprotoLongData, ServerCallContext context)
        {
            SubConfig SubConfig = new SubConfig();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    SubConfig.sub_config_id = aprotoLongData.Data;
                    DALSubConfig lDALSubConfig = new DALSubConfig(SubConfig);
                    lDALSubConfig.DeleteSubConfig("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(SubConfig.GetProto());
        }
        #endregion

        #region MasterConfig
        public override async Task<protoMasterConfig> CreateNewMasterConfig(Empty empty, ServerCallContext context)
        {
            protoMasterConfig lprotoMasterConfig = new protoMasterConfig();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lprotoMasterConfig);
        }

        public override async Task<protoMasterConfig> SaveMasterConfig(protoMasterConfig aprotoMasterConfig, ServerCallContext context)
        {
            MasterConfig MasterConfig = new MasterConfig();
            try
            {
                MasterConfig.GetData(aprotoMasterConfig);
                DALMasterConfig lDALMasterConfig = new DALMasterConfig(MasterConfig);
                lDALMasterConfig.SaveMasterConfig("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(MasterConfig.GetProto());
        }

        public override async Task<protoMasterConfig> UpdateMasterConfig(protoMasterConfig aprotoMasterConfig, ServerCallContext context)
        {
            MasterConfig MasterConfig = new MasterConfig();
            try
            {
                MasterConfig.GetData(aprotoMasterConfig);
                DALMasterConfig lDALMasterConfig = new DALMasterConfig(MasterConfig);
                lDALMasterConfig.UpdateMasterConfig("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(MasterConfig.GetProto());
        }

        public override async Task<protoMasterConfig> OpenMasterConfig(protoLongData aprotoLongData, ServerCallContext context)
        {
            MasterConfig MasterConfig = new MasterConfig();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    MasterConfig.master_config_id = aprotoLongData.Data;
                    DALMasterConfig lDALMasterConfig = new DALMasterConfig(MasterConfig);
                    lDALMasterConfig.OpenMasterConfig("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(MasterConfig.GetProto());
        }

        public override async Task<protoMasterConfig> DeleteMasterConfig(protoLongData aprotoLongData, ServerCallContext context)
        {
            MasterConfig MasterConfig = new MasterConfig();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    MasterConfig.master_config_id = aprotoLongData.Data;
                    DALMasterConfig lDALMasterConfig = new DALMasterConfig(MasterConfig);
                    lDALMasterConfig.DeleteMasterConfig("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(MasterConfig.GetProto());
        }
        #endregion

        #region Group

        //public override Task<protoGroupSearchResult> SearchGroup(protoSearchParams request, ServerCallContext context)
        //{
        //    SearchResultBase<GroupSearchResultset> searchResults = new SearchResultBase<GroupSearchResultset>();
        //    try
        //    {
        //        Group lGroup = new Group();
        //        DALGroup lDALGroup = new DALGroup(lGroup);
        //        searchResults = lDALGroup.SearchGroup(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return Task.FromResult(new GroupSearchResult().GetProto(searchResults));
        //}
        public override async Task<protoGroup> CreateNewGroup(Empty empty, ServerCallContext context)
        {
            Group lGroup = new Group();
            try
            {
                DALGroup lDALGroup = new DALGroup(lGroup);
                lDALGroup.CreateNewGroup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lGroup.GetProto());
        }

        public override async Task<protoGroup> SaveGroup(protoGroup aprotoGroup, ServerCallContext context)
        {
            Group lGroup = new Group();
            try
            {
                lGroup.GetData(aprotoGroup);
                string token = context.RequestHeaders.GetValue("authorization");
                lGroup.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                
                DALGroup lDALGroup = new DALGroup(lGroup);
                lDALGroup.SaveGroup(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lGroup.GetProto());
        }

        public override async Task<protoGroup> UpdateGroup(protoGroup aprotoGroup, ServerCallContext context)
        {
            Group lGroup = new Group();
            try
            {
                lGroup.GetData(aprotoGroup);
                string token = context.RequestHeaders.GetValue("authorization");
                lGroup.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lGroup.changed_date = DateTime.Now;
                lGroup.changed_by = lGroup.iLoggedInUserDetails.user_login_id;
                DALGroup lDALGroup = new DALGroup(lGroup);
                lDALGroup.UpdateGroup(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lGroup.GetProto());
        }

        public override async Task<protoGroup> OpenGroup(protoLongData aprotoLongData, ServerCallContext context)
        {
            Group Group = new Group();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Group.group_id = aprotoLongData.Data;
                    DALGroup lDALGroup = new DALGroup(Group);
                    lDALGroup.OpenGroup("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Group.GetProto());
        }

        public override async Task<protoGroup> DeleteGroup(protoLongData aprotoLongData, ServerCallContext context)
        {
            Group Group = new Group();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Group.group_id = aprotoLongData.Data;
                    DALGroup lDALGroup = new DALGroup(Group);
                    lDALGroup.DeleteGroup("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Group.GetProto());
        }

        #endregion

        #region UserGroup

        //public override Task<protoUserGroupSearchResult> SearchUserGroup(protoSearchParams request, ServerCallContext context)
        //{
        //    SearchResultBase<UserGroupSearchResultset> searchResults = new SearchResultBase<UserGroupSearchResultset>();
        //    try
        //    {
        //        UserGroup lUserGroup = new UserGroup();
        //        DALUserGroup lDALUserGroup = new DALUserGroup(lUserGroup);
        //        searchResults = lDALUserGroup.SearchUserGroup(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return Task.FromResult(new UserGroupSearchResult().GetProto(searchResults));
        //}
        public override async Task<protoUserGroup> CreateNewUserGroup(Empty empty, ServerCallContext context)
        {
            UserGroup lUserGroup = new UserGroup();
            try
            {
                DALUserGroup lDALUserGroup = new DALUserGroup(lUserGroup);
                lDALUserGroup.CreateNewUserGroup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lUserGroup.GetProto());
        }

        public override async Task<protoUserGroup> SaveUserGroup(protoUserGroup aprotoUserGroup, ServerCallContext context)
        {
            UserGroup lUserGroup = new UserGroup();
            try
            {
                lUserGroup.GetData(aprotoUserGroup);
                string token = context.RequestHeaders.GetValue("authorization");
                lUserGroup.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALUserGroup lDALUserGroup = new DALUserGroup(lUserGroup);
                lDALUserGroup.SaveUserGroup(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lUserGroup.GetProto());
        }

        public override async Task<protoUserGroup> UpdateUserGroup(protoUserGroup aprotoUserGroup, ServerCallContext context)
        {
            UserGroup lUserGroup = new UserGroup();
            try
            {
                lUserGroup.GetData(aprotoUserGroup);
                string token = context.RequestHeaders.GetValue("authorization");
                lUserGroup.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lUserGroup.changed_date = DateTime.Now;
                lUserGroup.changed_by = lUserGroup.iLoggedInUserDetails.user_login_id;
                DALUserGroup lDALUserGroup = new DALUserGroup(lUserGroup);
                lDALUserGroup.UpdateUserGroup(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lUserGroup.GetProto());
        }

        public override async Task<protoUserGroup> OpenUserGroup(protoLongData aprotoLongData, ServerCallContext context)
        {
            UserGroup UserGroup = new UserGroup();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    UserGroup.user_group_id = aprotoLongData.Data;
                    DALUserGroup lDALUserGroup = new DALUserGroup(UserGroup);
                    lDALUserGroup.OpenUserGroup("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(UserGroup.GetProto());
        }

        public override async Task<protoUserGroup> DeleteUserGroup(protoLongData aprotoLongData, ServerCallContext context)
        {
            UserGroup UserGroup = new UserGroup();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    UserGroup.user_group_id = aprotoLongData.Data;
                    DALUserGroup lDALUserGroup = new DALUserGroup(UserGroup);
                    lDALUserGroup.DeleteUserGroup("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(UserGroup.GetProto());
        }

        #endregion

        #region Role

        //public override Task<protoRoleSearchResult> SearchRole(protoSearchParams request, ServerCallContext context)
        //{
        //    SearchResultBase<RoleSearchResultset> searchResults = new SearchResultBase<RoleSearchResultset>();
        //    try
        //    {
        //        Role lRole = new Role();
        //        DALRole lDALRole = new DALRole(lRole);
        //        searchResults = lDALRole.SearchRole(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return Task.FromResult(new RoleSearchResult().GetProto(searchResults));
        //}
        public override async Task<protoRole> CreateNewRole(Empty empty, ServerCallContext context)
        {
            Role lRole = new Role();
            try
            {
                DALRole lDALRole = new DALRole(lRole);
                lDALRole.CreateNewRole();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRole.GetProto());
        }

        public override async Task<protoRole> SaveRole(protoRole aprotoRole, ServerCallContext context)
        {
            Role lRole = new Role();
            try
            {
                lRole.GetData(aprotoRole);
                string token = context.RequestHeaders.GetValue("authorization");
                lRole.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);

                DALRole lDALRole = new DALRole(lRole);
                lDALRole.SaveRole(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRole.GetProto());
        }

        public override async Task<protoRole> UpdateRole(protoRole aprotoRole, ServerCallContext context)
        {
            Role lRole = new Role();
            try
            {
                lRole.GetData(aprotoRole);
                string token = context.RequestHeaders.GetValue("authorization");
                lRole.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lRole.changed_date = DateTime.Now;
                lRole.changed_by = lRole.iLoggedInUserDetails.user_login_id;
                DALRole lDALRole = new DALRole(lRole);
                lDALRole.UpdateRole(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRole.GetProto());
        }

        public override async Task<protoRole> OpenRole(protoLongData aprotoLongData, ServerCallContext context)
        {
            Role Role = new Role();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Role.role_id = aprotoLongData.Data;
                    DALRole lDALRole = new DALRole(Role);
                    lDALRole.OpenRole("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Role.GetProto());
        }

        public override async Task<protoRole> DeleteRole(protoLongData aprotoLongData, ServerCallContext context)
        {
            Role Role = new Role();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Role.role_id = aprotoLongData.Data;
                    DALRole lDALRole = new DALRole(Role);
                    lDALRole.DeleteRole("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Role.GetProto());
        }

        #endregion

        #region RoleGroup

        //public override Task<protoRoleGroupSearchResult> SearchRoleGroup(protoSearchParams request, ServerCallContext context)
        //{
        //    SearchResultBase<RoleGroupSearchResultset> searchResults = new SearchResultBase<RoleGroupSearchResultset>();
        //    try
        //    {
        //        RoleGroup lRoleGroup = new RoleGroup();
        //        DALRoleGroup lDALRoleGroup = new DALRoleGroup(lRoleGroup);
        //        searchResults = lDALRoleGroup.SearchRoleGroup(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return Task.FromResult(new RoleGroupSearchResult().GetProto(searchResults));
        //}
        public override async Task<protoRoleGroup> CreateNewRoleGroup(Empty empty, ServerCallContext context)
        {
            RoleGroup lRoleGroup = new RoleGroup();
            try
            {
                DALRoleGroup lDALRoleGroup = new DALRoleGroup(lRoleGroup);
                lDALRoleGroup.CreateNewRoleGroup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRoleGroup.GetProto());
        }

        public override async Task<protoRoleGroup> SaveRoleGroup(protoRoleGroup aprotoRoleGroup, ServerCallContext context)
        {
            RoleGroup lRoleGroup = new RoleGroup();
            try
            {
                lRoleGroup.GetData(aprotoRoleGroup);
                string token = context.RequestHeaders.GetValue("authorization");
                lRoleGroup.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);

                DALRoleGroup lDALRoleGroup = new DALRoleGroup(lRoleGroup);
                lDALRoleGroup.SaveRoleGroup(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRoleGroup.GetProto());
        }

        public override async Task<protoRoleGroup> UpdateRoleGroup(protoRoleGroup aprotoRoleGroup, ServerCallContext context)
        {
            RoleGroup lRoleGroup = new RoleGroup();
            try
            {
                lRoleGroup.GetData(aprotoRoleGroup);
                string token = context.RequestHeaders.GetValue("authorization");
                lRoleGroup.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lRoleGroup.changed_date = DateTime.Now;
                lRoleGroup.changed_by = lRoleGroup.iLoggedInUserDetails.user_login_id;
                DALRoleGroup lDALRoleGroup = new DALRoleGroup(lRoleGroup);
                lDALRoleGroup.UpdateRoleGroup(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRoleGroup.GetProto());
        }

        public override async Task<protoRoleGroup> OpenRoleGroup(protoLongData aprotoLongData, ServerCallContext context)
        {
            RoleGroup RoleGroup = new RoleGroup();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    RoleGroup.role_group_id = aprotoLongData.Data;
                    DALRoleGroup lDALRoleGroup = new DALRoleGroup(RoleGroup);
                    lDALRoleGroup.OpenRoleGroup("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(RoleGroup.GetProto());
        }

        public override async Task<protoRoleGroup> DeleteRoleGroup(protoLongData aprotoLongData, ServerCallContext context)
        {
            RoleGroup RoleGroup = new RoleGroup();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    RoleGroup.role_group_id = aprotoLongData.Data;
                    DALRoleGroup lDALRoleGroup = new DALRoleGroup(RoleGroup);
                    lDALRoleGroup.DeleteRoleGroup("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(RoleGroup.GetProto());
        }

        #endregion

        #region Resource

        //public override Task<protoResourceSearchResult> SearchResource(protoSearchParams request, ServerCallContext context)
        //{
        //    SearchResultBase<ResourceSearchResultset> searchResults = new SearchResultBase<ResourceSearchResultset>();
        //    try
        //    {
        //        Resource lResource = new Resource();
        //        DALResource lDALResource = new DALResource(lResource);
        //        searchResults = lDALResource.SearchResource(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return Task.FromResult(new ResourceSearchResult().GetProto(searchResults));
        //}
        public override async Task<protoResource> CreateNewResource(Empty empty, ServerCallContext context)
        {
            Resource lResource = new Resource();
            try
            {
                DALResource lDALResource = new DALResource(lResource);
                lDALResource.CreateNewResource();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lResource.GetProto());
        }

        public override async Task<protoResource> SaveResource(protoResource aprotoResource, ServerCallContext context)
        {
            Resource lResource = new Resource();
            try
            {
                lResource.GetData(aprotoResource);
                string token = context.RequestHeaders.GetValue("authorization");
                lResource.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);

                DALResource lDALResource = new DALResource(lResource);
                lDALResource.SaveResource(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lResource.GetProto());
        }

        public override async Task<protoResource> UpdateResource(protoResource aprotoResource, ServerCallContext context)
        {
            Resource lResource = new Resource();
            try
            {
                lResource.GetData(aprotoResource);
                string token = context.RequestHeaders.GetValue("authorization");
                lResource.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lResource.changed_date = DateTime.Now;
                lResource.changed_by = lResource.iLoggedInUserDetails.user_login_id;
                DALResource lDALResource = new DALResource(lResource);
                lDALResource.UpdateResource(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lResource.GetProto());
        }

        public override async Task<protoResource> OpenResource(protoLongData aprotoLongData, ServerCallContext context)
        {
            Resource Resource = new Resource();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Resource.resource_id = aprotoLongData.Data;
                    DALResource lDALResource = new DALResource(Resource);
                    lDALResource.OpenResource("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Resource.GetProto());
        }

        public override async Task<protoResource> DeleteResource(protoLongData aprotoLongData, ServerCallContext context)
        {
            Resource Resource = new Resource();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    Resource.resource_id = aprotoLongData.Data;
                    DALResource lDALResource = new DALResource(Resource);
                    lDALResource.DeleteResource("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Resource.GetProto());
        }

        #endregion

        #region RoleResource

        //public override Task<protoRoleResourceSearchResult> SearchRoleResource(protoSearchParams request, ServerCallContext context)
        //{
        //    SearchResultBase<RoleResourceSearchResultset> searchResults = new SearchResultBase<RoleResourceSearchResultset>();
        //    try
        //    {
        //        RoleResource lRoleResource = new RoleResource();
        //        DALRoleResource lDALRoleResource = new DALRoleResource(lRoleResource);
        //        searchResults = lDALRoleResource.SearchRoleResource(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return Task.FromResult(new RoleResourceSearchResult().GetProto(searchResults));
        //}
        public override async Task<protoRoleResource> CreateNewRoleResource(Empty empty, ServerCallContext context)
        {
            RoleResource lRoleResource = new RoleResource();
            try
            {
                DALRoleResource lDALRoleResource = new DALRoleResource(lRoleResource);
                lDALRoleResource.CreateNewRoleResource();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRoleResource.GetProto());
        }

        public override async Task<protoRoleResource> SaveRoleResource(protoRoleResource aprotoRoleResource, ServerCallContext context)
        {
            RoleResource lRoleResource = new RoleResource();
            try
            {
                lRoleResource.GetData(aprotoRoleResource);
                string token = context.RequestHeaders.GetValue("authorization");
                lRoleResource.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                DALRoleResource lDALRoleResource = new DALRoleResource(lRoleResource);
                lDALRoleResource.SaveRoleResource(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRoleResource.GetProto());
        }

        public override async Task<protoRoleResource> UpdateRoleResource(protoRoleResource aprotoRoleResource, ServerCallContext context)
        {
            RoleResource lRoleResource = new RoleResource();
            try
            {
                lRoleResource.GetData(aprotoRoleResource);
                string token = context.RequestHeaders.GetValue("authorization");
                lRoleResource.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                lRoleResource.changed_date = DateTime.Now;
                lRoleResource.changed_by = lRoleResource.iLoggedInUserDetails.user_login_id;
                DALRoleResource lDALRoleResource = new DALRoleResource(lRoleResource);
                lDALRoleResource.UpdateRoleResource(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(lRoleResource.GetProto());
        }

        public override async Task<protoRoleResource> OpenRoleResource(protoLongData aprotoLongData, ServerCallContext context)
        {
            RoleResource RoleResource = new RoleResource();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    RoleResource.role_resource_id = aprotoLongData.Data;
                    DALRoleResource lDALRoleResource = new DALRoleResource(RoleResource);
                    lDALRoleResource.OpenRoleResource("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(RoleResource.GetProto());
        }

        public override async Task<protoRoleResource> DeleteRoleResource(protoLongData aprotoLongData, ServerCallContext context)
        {
            RoleResource RoleResource = new RoleResource();
            try
            {
                if (aprotoLongData != null && aprotoLongData.Data > 0)
                {
                    RoleResource.role_resource_id = aprotoLongData.Data;
                    DALRoleResource lDALRoleResource = new DALRoleResource(RoleResource);
                    lDALRoleResource.DeleteRoleResource("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(RoleResource.GetProto());
        }

        #endregion
    }
}
