using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application
{
    public static class ServiceReqistration
    {
        public static void AddApplicationServices(this IServiceCollection service)
        {
            service.AddMediatR(typeof(ServiceReqistration).Assembly);
        }
    }
}
