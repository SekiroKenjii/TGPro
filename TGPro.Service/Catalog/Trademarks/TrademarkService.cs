using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Trademarks;

namespace TGPro.Service.Catalog.Trademarks
{
    public class TrademarkService : ITrademarkService
    {
        private readonly TGProDbContext _db;
        private readonly Cloudinary _cloudinary;

        public TrademarkService(TGProDbContext db, IOptions<CloudinarySettings> config)
        {
            _db = db;

            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<ApiResponse<string>> Create(TrademarkRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            var trademark = new Trademark()
            {
                Name = request.Name,
                Status = request.Status,
                Description = request.Description
            };
            var uploadResult = await UploadImage(request.Image);
            trademark.Image = uploadResult.SecureUrl.ToString();
            trademark.PublicId = uploadResult.PublicId;
            _db.Trademarks.Add(trademark);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<string>> Delete(int trademarkId)
        {
            var trademarkFromDb = await _db.Trademarks.FindAsync(trademarkId);
            if (trademarkFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(trademarkId));
            var result = await DeleteImage(trademarkFromDb.PublicId);
            if (result.Error != null)
                return new ApiErrorResponse<string>(ConstantStrings.cloudDeleteFailed);
            _db.Trademarks.Remove(trademarkFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.deleteSuccessfully);
        }

        public async Task<ApiResponse<Trademark>> GetById(int trademarkId)
        {
            var trademarkFromDb = await _db.Trademarks.FindAsync(trademarkId);
            if (trademarkFromDb == null)
                return new ApiErrorResponse<Trademark>(ConstantStrings.FindByIdError(trademarkId));
            return new ApiSuccessResponse<Trademark>(trademarkFromDb);
        }

        public async Task<ApiResponse<List<Trademark>>> GetListTrademark()
        {
            List<Trademark> lstTrademark = await _db.Trademarks.OrderBy(t => t.Name).ToListAsync();
            if (lstTrademark.Count == 0)
                return new ApiErrorResponse<List<Trademark>>(ConstantStrings.getAllError);
            return new ApiSuccessResponse<List<Trademark>>(lstTrademark);
        }

        public async Task<ApiResponse<string>> Update(int trademarkId, TrademarkRequest request)
        {
            var trademarkFromDb = await _db.Trademarks.FindAsync(trademarkId);
            if (trademarkFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(trademarkId));
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            trademarkFromDb.Name = request.Name;
            trademarkFromDb.Status = request.Status;
            trademarkFromDb.Description = request.Description;
            if (request.Image != null)
            {
                var result = await DeleteImage(trademarkFromDb.PublicId);
                if (result.Error != null)
                    return new ApiErrorResponse<string>(ConstantStrings.cloudDeleteFailed);
                var uploadResult = await UploadImage(request.Image);
                trademarkFromDb.Image = uploadResult.SecureUrl.ToString();
                trademarkFromDb.PublicId = uploadResult.PublicId;
            }
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }

        private async Task<ImageUploadResult> UploadImage(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if(file != null)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Folder = ConstantStrings.CL_TRADEMARK_IMAGE_FOLDER
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            else
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(ConstantStrings.TRADEMARK_IMAGE_FOLDER + ConstantStrings.defaultTrademarkImage),
                    Folder = ConstantStrings.CL_TRADEMARK_IMAGE_FOLDER
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
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
