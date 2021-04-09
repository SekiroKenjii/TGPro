using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Authentication;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Authentication;

namespace TGPro.BackendAPI.Controllers
{
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/api/user/authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Authenticate(request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [Authorize(Roles = ConstantStrings.AdminRole)]
        [HttpPost("/api/user/role/add")]
        public async Task<IActionResult> AddRole(RoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.AddRole(request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [Authorize(Roles = ConstantStrings.AdminRole)]
        [HttpGet("/api/user/role/all")]
        public async Task<IActionResult> GetAllRole()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.GetRoles();
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [Authorize(Roles = ConstantStrings.AdminRole)]
        [HttpPut("/api/user/role/update/{roleId}")]
        public async Task<IActionResult> UpdateUserRole(Guid roleId, RoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateRole(roleId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [Authorize(Roles = ConstantStrings.AdminRole)]
        [HttpDelete("/api/user/role/delete/{roleId}")]
        public async Task<IActionResult> DeleteUserRole(Guid roleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.DeleteRole(roleId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [Authorize(Roles = ConstantStrings.AdminRole)]
        [HttpGet("/api/user/{userRoleRequest}/all")]
        public async Task<IActionResult> GetUserByRole(string userRoleRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.GetUsers(userRoleRequest);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [Authorize(Roles = ConstantStrings.AdminRole)]
        [HttpPost("/api/user/add/{userRoleRequest}")]
        public async Task<IActionResult> CreateUser(string userRoleRequest,[FromForm] UserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.AddUser(userRoleRequest, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [Authorize(Roles = ConstantStrings.AdminRole)]
        [HttpPut("/api/user/update/{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromForm] UserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateUser(userId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }
    }
}
