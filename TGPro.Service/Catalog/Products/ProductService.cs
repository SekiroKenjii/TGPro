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
using TGPro.Service.DTOs.Products.ViewModel;

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

        public async Task<ApiResponse<ProductDetailsViewModel>> GetAllProductDetails()
        {
            var productDetailsVM = new ProductDetailsViewModel()
            {
                Categories = await _db.Categories.ToListAsync(),
                Vendors = await _db.Vendors.ToListAsync(),
                Trademarks = await _db.Trademarks.ToListAsync(),
                Demands = await _db.Demands.ToListAsync(),
                Conditions = await _db.Conditions.ToListAsync()
            };
            return new ApiSuccessResponse<ProductDetailsViewModel>(productDetailsVM);
        }

        public async Task<ApiResponse<ProductViewModel>> GetById(int productId)
        {
            var productFromDb = await _db.Products.FindAsync(productId);

            if (productFromDb == null)
                return new ApiErrorResponse<ProductViewModel>(ConstantStrings.FindByIdError(productId));

            var category = await _db.Categories.FindAsync(productFromDb.CategoryId);
            var trademark = await _db.Trademarks.FindAsync(productFromDb.TrademarkId);
            var vendor = await _db.Vendors.FindAsync(productFromDb.VendorId);
            var demand = await _db.Demands.FindAsync(productFromDb.DemandId);
            var condition = await _db.Conditions.FindAsync(productFromDb.ConditionId);
            var productImages = await _db.ProductImages.Where(x => x.ProductId == productFromDb.Id).ToListAsync();

            var productVM = _mapper.Map<ProductViewModel>(productFromDb);
            productVM.Category = _mapper.Map<CategoryViewModel>(category);
            productVM.Trademark = _mapper.Map<TrademarkViewModel>(trademark);
            productVM.Vendor = _mapper.Map<VendorViewModel>(vendor);
            productVM.Demand = _mapper.Map<DemandViewModel>(demand);
            productVM.Condition = _mapper.Map<ConditionViewModel>(condition);
            productVM.ProductImages = _mapper.Map<IEnumerable<ProductImageViewModel>>(productImages);

            return new ApiSuccessResponse<ProductViewModel>(productVM);
        }

        public async Task<ApiResponse<IEnumerable<ProductViewModel>>> GetListProduct()
        {
            List<Product> products = await _db.Products.OrderBy(x => x.Name).ToListAsync();

            if (products.Count == 0)
                return new ApiErrorResponse<IEnumerable<ProductViewModel>>(ConstantStrings.getAllError);

            var lstProductVM = new List<ProductViewModel>();

            foreach (var product in products)
            {
                var category = await _db.Categories.FindAsync(product.CategoryId);
                var trademark = await _db.Trademarks.FindAsync(product.TrademarkId);
                var vendor = await _db.Vendors.FindAsync(product.VendorId);
                var demand = await _db.Demands.FindAsync(product.DemandId);
                var condition = await _db.Conditions.FindAsync(product.ConditionId);
                var productImages = await _db.ProductImages.Where(x => x.ProductId == product.Id).ToListAsync();

                var productVM = _mapper.Map<ProductViewModel>(product);
                productVM.Category = _mapper.Map<CategoryViewModel>(category);
                productVM.Trademark = _mapper.Map<TrademarkViewModel>(trademark);
                productVM.Vendor = _mapper.Map<VendorViewModel>(vendor);
                productVM.Demand = _mapper.Map<DemandViewModel>(demand);
                productVM.Condition = _mapper.Map<ConditionViewModel>(condition);
                productVM.ProductImages = _mapper.Map<IEnumerable<ProductImageViewModel>>(productImages);
                lstProductVM.Add(productVM);
            }

            return new ApiSuccessResponse<IEnumerable<ProductViewModel>>(lstProductVM);
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
