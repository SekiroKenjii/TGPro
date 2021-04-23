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
            var productImage = new ProductImage()
            {
                ImageUrl = ConstantStrings.blankProductImageUrl,
                PublicId = ConstantStrings.blankProductImagePublicId,
                Caption = SystemFunctions.BlankProductImageCaption(product.Name),
                ProductId = _db.Products.Where(x => x.Name == request.Name).FirstOrDefaultAsync().Id,
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

        public async Task<ApiResponse<Product>> GetById(int productId)
        {
            var product = await _db.Products.AsNoTracking().Include(x => x.Category).Include(x => x.Vendor)
                                            .Include(x => x.Trademark).Include(x => x.Demand)
                                            .Include(x => x.Condition).Include(x => x.ProductImages)
                                            .FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
                return new ApiErrorResponse<Product>(ConstantStrings.FindByIdError(productId));

            return new ApiSuccessResponse<Product>(product);
        }

        public async Task<ApiResponse<IEnumerable<Product>>> GetListProduct()
        {
            var products = await _db.Products.AsNoTracking().Include(x => x.Category).Include(x => x.Vendor)
                                            .Include(x => x.Trademark).Include(x => x.Demand)
                                            .Include(x => x.Condition).Include(x => x.ProductImages).ToListAsync();

            if (products.Count == 0)
                return new ApiErrorResponse<IEnumerable<Product>>(ConstantStrings.getAllError);

            return new ApiSuccessResponse<IEnumerable<Product>>(products);
        }

        public async Task<ApiResponse<string>> Update(int productId, ProductRequest request)
        {
            var productFromDb = await _db.Products.FindAsync(productId);
            if (productFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(productId));
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            _mapper.Map<ProductRequest, Product>(request, productFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }
    }
}
