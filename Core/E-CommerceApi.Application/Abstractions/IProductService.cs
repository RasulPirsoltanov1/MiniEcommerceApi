﻿using E_CommerceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Abstractions
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}