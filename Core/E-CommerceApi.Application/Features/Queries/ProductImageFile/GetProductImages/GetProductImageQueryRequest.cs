using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImageQueryRequest:IRequest<List<GetProductImageQueryResponse>>
    {
        public string id { get; set; }
    }
}
