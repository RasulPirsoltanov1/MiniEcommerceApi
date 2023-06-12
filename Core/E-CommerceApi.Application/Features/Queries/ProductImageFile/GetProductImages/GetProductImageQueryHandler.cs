using E_CommerceApi.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        private IProductReadRepository _productReadRepository;

        public GetProductImageQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<List<GetProductImageQueryResponse>> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table.Include(pi => pi.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.id));
            return product.ProductImageFiles.Select(p => new GetProductImageQueryResponse
            {
                Id=p.Id,
                FileName=p.FileName,
                Path=p.Path
            }).ToList();
        }

    }
}
