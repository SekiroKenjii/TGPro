using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using TGPro.Service.Utility;

namespace TGPro.Service.Common
{
    public class FileStorageService : IStorageService
    {
        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            ConstantStrings._productFolder = Path.Combine(webHostEnvironment.WebRootPath, ConstantStrings.PRODUCT_IMAGE_FOLDER);
            ConstantStrings._trademarkFolder = Path.Combine(webHostEnvironment.WebRootPath, ConstantStrings.TRADEMARK_IMAGE_FOLDER);
            ConstantStrings._userFolder = Path.Combine(webHostEnvironment.WebRootPath, ConstantStrings.USER_IMAGE_FOLDER);
        }

        public async Task DeleteProductFileAsync(string fileName)
        {
            var filePath = Path.Combine(ConstantStrings._productFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public async Task DeleteTrademarkFileAsync(string fileName)
        {
            var filePath = Path.Combine(ConstantStrings._productFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetProductFileUrl(string fileName)
        {
            return $"/{ConstantStrings.PRODUCT_IMAGE_FOLDER}/{fileName}";
        }

        public string GetTrademarkFileUrl(string fileName)
        {
            return $"/{ConstantStrings.TRADEMARK_IMAGE_FOLDER}/{fileName}";
        }

        public async Task SaveProductFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(ConstantStrings._productFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task SaveTrademarkFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(ConstantStrings._trademarkFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }
    }
}
