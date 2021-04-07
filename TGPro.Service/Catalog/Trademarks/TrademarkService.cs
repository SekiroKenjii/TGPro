using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.SystemResources;
using TGPro.Service.ViewModel.Trademarks;

namespace TGPro.Service.Catalog.Trademarks
{
    public class TrademarkService : ITrademarkService
    {
        private readonly TGProDbContext _db;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public TrademarkService(TGProDbContext db, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _db = db;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [System.Obsolete]
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
            var uploadResult = new ImageUploadResult();
            if (request.Image != null)
            {
                using (var stream = request.Image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(request.Image.Name, stream),
                        Folder = ConstantStrings.CL_TRADEMARK_IMAGE_FOLDER
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
                trademark.Image = uploadResult.Uri.ToString();
                trademark.PublicId = uploadResult.PublicId;
            }
            else
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(ConstantStrings.TRADEMARK_IMAGE_FOLDER + ConstantStrings.defaultTrademarkImage),
                    Folder = ConstantStrings.CL_TRADEMARK_IMAGE_FOLDER
                };
                uploadResult = _cloudinary.Upload(uploadParams);
                trademark.Image = uploadResult.Uri.ToString();
                trademark.PublicId = uploadResult.PublicId;
            }
            _db.Trademarks.Add(trademark);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<string>> Delete(int trademarkId)
        {
            var trademarkFromDb = await _db.Trademarks.FindAsync(trademarkId);
            if (trademarkFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(trademarkId));
            var delResult = DeleteTMImage(trademarkFromDb.PublicId);
            if (delResult != "ok")
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

        [System.Obsolete]
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
                var delResult = DeleteTMImage(trademarkFromDb.PublicId);
                if (delResult != "ok")
                    return new ApiErrorResponse<string>(ConstantStrings.cloudDeleteFailed);
                using (var stream = request.Image.OpenReadStream())
                {
                    var uploadResult = new ImageUploadResult();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(request.Image.Name, stream),
                        Folder = ConstantStrings.CL_TRADEMARK_IMAGE_FOLDER
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                    trademarkFromDb.Image = uploadResult.Uri.ToString();
                    trademarkFromDb.PublicId = uploadResult.PublicId;
                }
            }
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }

        private string DeleteTMImage(string publicID)
        {
            var deletionParams = new DeletionParams(publicID)
            {
                ResourceType = ResourceType.Image
            };
            var deletionResult = _cloudinary.Destroy(deletionParams);
            return deletionResult.Result;
        }
    }
}
