﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.Exceptions;
using TGPro.Service.Utility;
using TGPro.Service.ViewModel.Trademarks;

namespace TGPro.Service.Catalog.Trademarks
{
    public class TrademarkService : ITrademarkService
    {
        private readonly TGProDbContext _db;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly IStorageService _storageService;
        public TrademarkService(TGProDbContext db, IOptions<CloudinarySettings> cloudinaryConfig, IStorageService storageService)
        {
            _db = db;
            _cloudinaryConfig = cloudinaryConfig;
            _storageService = storageService;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [System.Obsolete]
        public async Task<int> Create(TrademarkRequest request)
        {
            if (request.Name == string.Empty) throw new TGProException("Name cannot be null");
            else
            {
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
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int trademarkId)
        {
            var trademarkFromDb = await _db.Trademarks.FindAsync(trademarkId);
            if (trademarkFromDb == null) throw new TGProException($"Cannot find any trdemarks with Id: {trademarkId}");
            else
            {
                var delResult = DeleteTMImage(trademarkFromDb.PublicId);
                if (delResult != "ok") throw new TGProException("Failed to delete image from cloud");
                _db.Trademarks.Remove(trademarkFromDb);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<Trademark> GetById(int trademarkId)
        {
            var trademarkFromDb = await _db.Trademarks.FindAsync(trademarkId);
            if (trademarkFromDb == null) throw new TGProException($"Cannot find any trdemarks with Id: {trademarkId}");
            else return trademarkFromDb;
        }

        public async Task<List<Trademark>> GetListTrademark()
        {
            List<Trademark> lstTrademark = await _db.Trademarks.OrderBy(t => t.Name).ToListAsync();
            if (lstTrademark.Count == 0) throw new TGProException("Cannot find any trdemarks");
            else return lstTrademark;
        }

        [System.Obsolete]
        public async Task<int> Update(int trademarkId, TrademarkRequest request)
        {
            var trademarkFromDb = await _db.Trademarks.FindAsync(trademarkId);
            if (trademarkFromDb == null) throw new TGProException($"Cannot find any trdemarks with Id: {trademarkId}");
            else
            {
                trademarkFromDb.Name = request.Name;
                trademarkFromDb.Status = request.Status;
                trademarkFromDb.Description = request.Description;
                if (request.Image != null)
                {
                    var delResult = DeleteTMImage(trademarkFromDb.PublicId);
                    if (delResult != "ok") throw new TGProException("Failed to delete image from cloud");
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
                return await _db.SaveChangesAsync();
            }
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