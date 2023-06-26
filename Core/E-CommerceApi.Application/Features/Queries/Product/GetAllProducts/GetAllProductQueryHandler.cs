using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Application.RequestParameters;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        public IProductReadRepository _productReadRepository { get; set; }
        private ILogger<GetAllProductQueryHandler> _logger;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("getting all products");
            throw new Exception("Asadsadsadasdsadas...");
            var totalCount = _productReadRepository.GetAll().Count();

            var products = _productReadRepository.GetAll().Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.CreatedDate,
                p.UpdatedDate
            }).Skip(request.Size * request.Page).Take(request.Size).ToList();
            return new()
            {
                TotalCount = totalCount,
                Products = products
            };
        }
    }
}
