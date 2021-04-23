using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.DTOs.ProductImages;

namespace TGPro.Service.Catalog.ProductImages
{
    public class ProductImageService : IProductImageService
    {
        private readonly TGProDbContext _db;
        private readonly Cloudinary _cloudinary;
        public ProductImageService(TGProDbContext db, IOptions<CloudinarySettings> config)
        {
            _db = db;

            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<ApiResponse<string>> Create(int productId, ProductImageRequest request)
        {
            var productFromDb = await _db.Products.FindAsync(productId);
            if (productFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(productId));
            var productImageFromDb = await _db.ProductImages.Where(x => x.ProductId == productFromDb.Id)
                .OrderByDescending(x => x.SortOrder).ToListAsync();
            if (productFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(productId));
            if (request.ProductImages == null)
                return new ApiErrorResponse<string>(ConstantStrings.imgIsEmptyOrNull);
            var images = request.ProductImages.ToList();
            for (int i = 0; i < images.Count; i++)
            {
                var uploadResult = await UploadImage(images[i]);
                if (uploadResult.Error != null)
                    return new ApiErrorResponse<string>(uploadResult.Error.ToString());
                var productImage = new ProductImage()
                {
                    ImageUrl = uploadResult.SecureUrl.ToString(),
                    PublicId = uploadResult.PublicId,
                    Caption = SystemFunctions.ProductImageCaption(productFromDb.Name, productImageFromDb.FirstOrDefault().SortOrder + i + 1),
                    ProductId = productFromDb.Id,
                    SortOrder = productImageFromDb.FirstOrDefault().SortOrder + i + 1
                };
                _db.ProductImages.Add(productImage);
            }
            _db.ProductImages.Remove(productImageFromDb.Where(x=>x.PublicId == ConstantStrings.blankProductImagePublicId).FirstOrDefault());
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<string>> Update(int productId, ProductImageRequest request)
        {
            var productFromDb = await _db.Products.FindAsync(productId);
            if (productFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(productId));
            var productImageFromDb = await _db.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
            if (productImageFromDb.Count == 0)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(productId));
            if (request.ProductImages == null)
                return new ApiErrorResponse<string>(ConstantStrings.imgIsEmptyOrNull);
            var images = request.ProductImages.ToList();
            for (int i = 0; i < images.Count; i++)
            {
                foreach(var item in productImageFromDb)
                {
                    if(images[i].Name == item.Caption)
                    {
                        var deleteResult = await DeleteImage(item.PublicId);
                        if (deleteResult.Error != null)
                            return new ApiErrorResponse<string>(deleteResult.Error.ToString());
                        var uploadResult = await UploadImage(images[i]);
                        if (uploadResult.Error != null)
                            return new ApiErrorResponse<string>(uploadResult.Error.ToString());
                        item.ImageUrl = uploadResult.SecureUrl.ToString();
                        item.PublicId = uploadResult.PublicId;
                        break;
                    }
                }
            }
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }

        public async Task<ApiResponse<string>> Delete(int productImageId)
        {
            var productImage = await _db.ProductImages.FindAsync(productImageId);
            if (productImage == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(productImageId));
            var deleteResult = await DeleteImage(productImage.PublicId);
            if (deleteResult.Error != null)
                return new ApiErrorResponse<string>(deleteResult.Error.ToString());
            _db.ProductImages.Remove(productImage);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.deleteSuccessfully);
        }

        private async Task<ImageUploadResult> UploadImage(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = ConstantStrings.CL_PRODUCT_IMAGE_FOLDER,
                Transformation = new Transformation().Height(800).Width(800).Crop("fill")
            };
            return await _cloudinary.UploadAsync(uploadParams);
        }

        private async Task<DeletionResult> DeleteImage(string publicID)
        {
            var deletionParams = new DeletionParams(publicID);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            return deletionResult;
        }
    }
}
