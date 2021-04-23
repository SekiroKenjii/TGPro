using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TGPro.Service.DTOs.Authentication;
using TGPro.Data.Entities;
using System.Security.Claims;
using System;
using TGPro.Service.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TGPro.Data.EF;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TGPro.Data.Enums;
using TGPro.Service.DTOs.Authentication.ViewModel;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace TGPro.Service.Catalog.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly TGProDbContext _db;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                            RoleManager<AppRole> roleManager, ITokenService tokenService,
                            TGProDbContext db, IOptions<CloudinarySettings> config, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _db = db;
            _mapper = mapper;

            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
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

        public async Task<ApiResponse<List<UserViewModel>>> GetUsers(string userRole)
        {
            List<UserViewModel> userVM = new List<UserViewModel>();
            var emp = await _userManager.GetUsersInRoleAsync(userRole);
            if (emp.Count == 0)
                return new ApiErrorResponse<List<UserViewModel>>(ConstantStrings.getAllError);
            foreach(var e in emp)
            {
                var roles = await _userManager.GetRolesAsync(e);
                userVM.Add( new UserViewModel {
                    User = e,
                    Roles = string.Join(", ", roles)
                });
            }
            return new ApiSuccessResponse<List<UserViewModel>>(userVM);
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

        public async Task<ApiResponse<string>> AddUser(string userRoleRequest, UserRequest request)
        {
            var user = _mapper.Map<AppUser>(request);
            user.LockoutEnd = DateTime.Now;
            if (request.ProfilePicture != null)
            {
                var result = await UploadImage(request.ProfilePicture, null);
                user.ProfilePicture = result.SecureUrl.ToString();
                user.PublicId = result.PublicId;
            }
            else
            {
                if (request.Gender == Gender.Female) {
                    var result = await UploadImage(request.ProfilePicture, request.Gender);
                    user.ProfilePicture = result.SecureUrl.ToString();
                    user.PublicId = result.PublicId;
                }
                else
                {
                    var result = await UploadImage(request.ProfilePicture, request.Gender);
                    user.ProfilePicture = result.SecureUrl.ToString();
                    user.PublicId = result.PublicId;
                }
                    
            }
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (createResult.Succeeded)
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
            {
                await DeleteImage(user.PublicId);
                return new ApiErrorResponse<string>(ConstantStrings.addUnuccessfully);
            }
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

        public async Task<ApiResponse<string>> UpdateUser(Guid userId, UserRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var currentRole = await _userManager.GetRolesAsync(user);
            if (user == null)
                return new ApiErrorResponse<string>(ConstantStrings.userNotExist);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            if (!string.IsNullOrEmpty(request.CurrentPassword) && !string.IsNullOrEmpty(request.Password))
            {
                var passwordChanged = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.Password);
                if (!passwordChanged.Succeeded)
                {
                    foreach(var error in passwordChanged.Errors)
                    {
                        if (error.Code.Contains("Mismatch"))
                        {
                            return new ApiErrorResponse<string>(passwordChanged.Errors.ToString());
                        }
                        return new ApiErrorResponse<string>(error.Description);
                    }
                }
            }
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;
            user.City = request.City;
            user.Country = request.Country;
            user.Gender = request.Gender;
            if (!string.IsNullOrEmpty(request.SubRole))
            {
                for (var i = 1; i< currentRole.Count; i++)
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRole[i]);
                    await _userManager.AddToRoleAsync(user, request.SubRole);
                }
            }
            if (request.ProfilePicture != null)
            {
                var delResult = await DeleteImage(user.PublicId);
                if (delResult.Error  != null)
                    return new ApiErrorResponse<string>(ConstantStrings.cloudDeleteFailed);
                var result = await UploadImage(request.ProfilePicture, null);
                user.ProfilePicture = result.SecureUrl.ToString();
                user.PublicId = result.PublicId;
            }
            await _userManager.UpdateAsync(user);
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }

        public async Task<ApiResponse<string>> LockUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new ApiErrorResponse<string>(ConstantStrings.userNotExist);
            user.LockoutEnd = DateTime.Now.AddYears(1000);
            user.LockoutEnabled = true;
            await _userManager.UpdateAsync(user);
            return new ApiSuccessResponse<string>(ConstantStrings.lockSuccessfully);
        }

        public async Task<ApiResponse<string>> UnlockUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new ApiErrorResponse<string>(ConstantStrings.userNotExist);
            user.LockoutEnd = DateTime.Now;
            user.LockoutEnabled = false;
            await _userManager.UpdateAsync(user);
            return new ApiSuccessResponse<string>(ConstantStrings.unlockSuccessfully);
        }

        private async Task<ImageUploadResult> UploadImage(IFormFile file, Gender? gender)
        {
            var uploadResult = new ImageUploadResult();
            if(gender == null)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Folder = ConstantStrings.CL_USER_IMAGE_FOLDER
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            else
            {
                if(gender == Gender.Female)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(ConstantStrings.USER_IMAGE_FOLDER + ConstantStrings.defaultFemaleImage),
                        Folder = ConstantStrings.CL_USER_IMAGE_FOLDER
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
                else
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(ConstantStrings.USER_IMAGE_FOLDER + ConstantStrings.defaultMaleImage),
                        Folder = ConstantStrings.CL_USER_IMAGE_FOLDER
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }
            return uploadResult;
        }

        private async Task<DeletionResult> DeleteImage(string publicID)
        {
            var deletionParams = new DeletionParams(publicID);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            return deletionResult;
        }
    }
}
