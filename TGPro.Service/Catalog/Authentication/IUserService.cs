using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Authentication;
using TGPro.Service.DTOs.Authentication.ViewModel;

namespace TGPro.Service.Catalog.Authentication
{
    public interface IUserService
    {
        //Auth
        Task<ApiResponse<object>> Authenticate(LoginRequest request);
        Task<ApiResponse<string>> Register(RegisterRequest request);
        //Role
        Task<ApiResponse<string>> AddRole(RoleRequest request);
        Task<ApiResponse<List<AppRole>>> GetRoles();
        Task<ApiResponse<string>> UpdateRole(Guid roleId, RoleRequest request);
        Task<ApiResponse<string>> DeleteRole(Guid roleId);
        //Employee
        Task<ApiResponse<List<UserViewModel>>> GetUsers(string userRoleRequest);
        Task<ApiResponse<string>> AddUser(string userRoleRequest, UserRequest request);
        Task<ApiResponse<string>> UpdateUser(Guid userId, UserRequest request);
    }
}
