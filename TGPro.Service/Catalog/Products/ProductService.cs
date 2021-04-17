using AutoMapper;
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
using TGPro.Service.DTOs.Products;

namespace TGPro.Service.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly TGProDbContext _db;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;
        public ProductService(TGProDbContext db, IMapper mapper,
            IOptions<CloudinarySettings> config)
        {
            _db = db;
            _mapper = mapper;

            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<ApiResponse<string>> Create(ProductRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            var product = _mapper.Map<Product>(request);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            var uploadResult = new ImageUploadResult();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(ConstantStrings.PRODUCT_IMAGE_FOLDER + ConstantStrings.defaultProductImage),
                Folder = ConstantStrings.CL_PRODUCT_IMAGE_FOLDER,
                Transformation = new Transformation().Height(800).Width(800).Crop("fill")
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
            var productImage = new ProductImage()
            {
                ImageUrl = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId,
                Caption = SystemFunctions.ProductImageCaption(product.Name, 1),
                ProductId = _db.Products.OrderByDescending(x => x.Id).FirstOrDefault().Id,
                IsDefault = true,
                SortOrder = 1
            };
            _db.ProductImages.Add(productImage);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<string>> Delete(int productId)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(productId));
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.deleteSuccessfully);
        }

        public async Task<ApiResponse<Product>> GetById(int productId)
        {
            var productFromDb = await _db.Products.FindAsync(productId);
            if (productFromDb == null)
                return new ApiErrorResponse<Product>(ConstantStrings.FindByIdError(productId));
            var productVM = _mapper.Map<Product>(productFromDb);
            productVM.Category = await _db.Categories.FindAsync(productFromDb.CategoryId);
            productVM.Trademark = await _db.Trademarks.FindAsync(productFromDb.TrademarkId);
            productVM.Vendor = await _db.Vendors.FindAsync(productFromDb.VendorId);
            productVM.Demand = await _db.Demands.FindAsync(productFromDb.DemandId);
            productVM.Condition = await _db.Conditions.FindAsync(productFromDb.ConditionId);
            productVM.ProductImages = await _db.ProductImages.Where(x => x.ProductId == productFromDb.Id).ToListAsync();
            return new ApiSuccessResponse<Product>(productVM);
        }

        public async Task<ApiResponse<IEnumerable<Product>>> GetListProduct()
        {
            List<Product> products = await _db.Products.OrderBy(x => x.Name).ToListAsync();
            if (products.Count == 0)
                return new ApiErrorResponse<IEnumerable<Product>>(ConstantStrings.getAllError);
            var lstProductVM = new List<Product>();
            foreach (var product in products)
            {
                var productVM = _mapper.Map<Product>(product);
                productVM.Category = await _db.Categories.FindAsync(product.CategoryId);
                productVM.Trademark = await _db.Trademarks.FindAsync(product.TrademarkId);
                productVM.Vendor = await _db.Vendors.FindAsync(product.VendorId);
                productVM.Demand = await _db.Demands.FindAsync(product.DemandId);
                productVM.Condition = await _db.Conditions.FindAsync(product.ConditionId);
                productVM.ProductImages = await _db.ProductImages.Where(x => x.ProductId == product.Id).ToListAsync();
                lstProductVM.Add(productVM);
            }
            return new ApiSuccessResponse<IEnumerable<Product>>(lstProductVM);
        }

        public async Task<ApiResponse<string>> Update(int productId, ProductRequest request)
        {
            var productFromDb = await _db.Products.FindAsync(productId);
            if (productFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(productId));
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            productFromDb = _mapper.Map<Product>(request);
            _db.Products.Update(productFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }
    }
}
