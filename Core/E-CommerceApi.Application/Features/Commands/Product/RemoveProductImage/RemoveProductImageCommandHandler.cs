using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace E_CommerceApi.Application.Features.Commands.Product.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        private IProductReadRepository _productReadRepository;
        private IProductImageFileReadRepository _productImageFileReadRepository;

        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileReadRepository productImageFileReadRepository)
        {
            _productReadRepository = productReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table.Include(pi => pi.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.id));
            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.imageId));
            product.ProductImageFiles.Remove(productImageFile);
            await _productImageFileReadRepository.SaveAsync();
            return new();
        }
    }
}
