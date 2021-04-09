using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TGPro.Service.ViewModel.Authentication;
using TGPro.Data.Entities;
using System.Security.Claims;
using System;
using TGPro.Service.Common;
using TGPro.Service.SystemResources;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TGPro.Data.EF;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TGPro.Data.Enums;

namespace TGPro.Service.Catalog.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly TGProDbContext _db;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                            RoleManager<AppRole> roleManager, ITokenService tokenService, TGProDbContext db,
                            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _db = db;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<ApiResponse<string>> AddRole(RoleRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            if (await _roleManager.RoleExistsAsync(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.roleExisted);
            var role = new AppRole
            {
                Name = request.Name,
                NormalizedName = request.Name,
                Description = request.Description
            };
            await _roleManager.CreateAsync(role);
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<object>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResponse<object>(ConstantStrings.userNotExist);
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded) return new ApiErrorResponse<object>(ConstantStrings.userInfoIncorrect);
            var roles = await _userManager.GetRolesAsync(user);
            var fullName = user.FirstName + " " + user.LastName;
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.GivenName, fullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles))
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.AccessToken = accessToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(user);

            return new ApiSuccessResponse<object>(new { 
                accessToken = accessToken,
                refreshToken = refreshToken
            });
        }

        public async Task<ApiResponse<IList<AppUser>>> GetUsers(string userRole)
        {
            var emp = await _userManager.GetUsersInRoleAsync(userRole);
            if (emp.Count == 0)
                return new ApiErrorResponse<IList<AppUser>>(ConstantStrings.getAllError);
            return new ApiSuccessResponse<IList<AppUser>>(emp);
        }

        public async Task<ApiResponse<List<AppRole>>> GetRoles()
        {
            var roles = await _db.AppRoles.ToListAsync();
            if (roles.Count == 0)
                return new ApiErrorResponse<List<AppRole>>(ConstantStrings.getAllError);
            return new ApiSuccessResponse<List<AppRole>>(roles);
        }

        public Task<ApiResponse<string>> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public async Task<ApiResponse<string>> AddUser(string userRoleRequest, UserRequest request)
        {
            var user = new AppUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                Sex = request.Sex
            };
            var uploadResult = new ImageUploadResult();
            if (request.ProfilePicture != null)
            {
                using (var stream = request.ProfilePicture.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(request.ProfilePicture.Name, stream),
                        Folder = ConstantStrings.CL_USER_IMAGE_FOLDER
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
                user.ProfilePicture = uploadResult.Uri.ToString();
                user.PublicId = uploadResult.PublicId;
            }
            else
            {
                if (request.Sex == Sex.Male || request.Sex == Sex.Null) {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(ConstantStrings.USER_IMAGE_FOLDER + ConstantStrings.defaultMaleImage),
                        Folder = ConstantStrings.CL_USER_IMAGE_FOLDER
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                    user.ProfilePicture = uploadResult.Uri.ToString();
                    user.PublicId = uploadResult.PublicId;
                }
                else
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(ConstantStrings.USER_IMAGE_FOLDER + ConstantStrings.defaultFemaleImage),
                        Folder = ConstantStrings.CL_USER_IMAGE_FOLDER
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                    user.ProfilePicture = uploadResult.Uri.ToString();
                    user.PublicId = uploadResult.PublicId;
                }
                    
            }
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                if (userRoleRequest.ToLower() == ConstantStrings.AdminRole.ToLower())
                {
                    await _userManager.AddToRoleAsync(user, ConstantStrings.AdminRole);
                }
                else if (userRoleRequest.ToLower() == ConstantStrings.EmployeeRole.ToLower())
                {
                    IEnumerable<string> empRoles = new List<string>() { userRoleRequest, request.SubRole };
                    await _userManager.AddToRolesAsync(user, empRoles);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, ConstantStrings.CustomerRole);
                }
            }
            else
                return new ApiErrorResponse<string>(ConstantStrings.addUnuccessfully);
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<string>> UpdateRole(Guid roleId, RoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return new ApiErrorResponse<string>(ConstantStrings.roleNotExisted);
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            role.Name = request.Name;
            role.Description = request.Description;
            role.NormalizedName = request.Name;
            await _roleManager.UpdateAsync(role);
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }

        public async Task<ApiResponse<string>> DeleteRole(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return new ApiErrorResponse<string>(ConstantStrings.roleNotExisted);
            await _roleManager.DeleteAsync(role);
            return new ApiSuccessResponse<string>(ConstantStrings.deleteSuccessfully);
        }
    }
}
