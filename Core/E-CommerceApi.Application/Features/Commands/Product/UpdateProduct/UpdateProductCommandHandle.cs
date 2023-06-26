using E_CommerceApi.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandle : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        public IProductReadRepository _productReadRepository { get; set; }
        private ILogger<UpdateProductCommandHandle> _logger;

        public UpdateProductCommandHandle(IProductReadRepository productReadRepository, ILogger<UpdateProductCommandHandle> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            E_CommerceApi.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Name = request.Name;
            product.Price = request.Price;
            await _productReadRepository.SaveAsync();
            _logger.LogInformation("product updated...");
            return new() { };
        }
    }
}
