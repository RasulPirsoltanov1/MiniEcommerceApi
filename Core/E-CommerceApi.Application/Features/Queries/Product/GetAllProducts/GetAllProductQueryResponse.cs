﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount{ get; set; }
        public object Products { get; internal set; }
    }
}
