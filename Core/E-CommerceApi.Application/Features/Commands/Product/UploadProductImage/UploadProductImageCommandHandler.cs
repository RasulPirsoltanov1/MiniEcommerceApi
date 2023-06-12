﻿using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Commands.Product.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IStorageService _storageService;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.id);
            var datas = await _storageService.UploadAsync(request.FormFileColection, "rr", "ew");
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(f => new ProductImageFile()
            {
                FileName = f.fileName,
                Path = f.path,
                Storage = _storageService.StorageName,
                Products = new List<E_CommerceApi.Domain.Entities.Product>() { product }
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
