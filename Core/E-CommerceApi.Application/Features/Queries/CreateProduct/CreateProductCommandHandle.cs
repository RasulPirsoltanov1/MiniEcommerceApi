using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Queries.CreateProduct
{
    public class CreateProductCommandHandle : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public IProductWriteRepository _productWriteRepository { get; set; }

        public CreateProductCommandHandle(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
