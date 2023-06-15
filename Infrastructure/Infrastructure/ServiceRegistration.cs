using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Application.Abstractions.Tokens;
using E_CommerceApi.Application.DTOs;
using E_CommerceApi.Application.Services;
using E_CommerceApi.Infrastructure.Enums;
using E_CommerceApi.Infrastructure.Services;
using E_CommerceApi.Infrastructure.Services.Storage;
using E_CommerceApi.Infrastructure.Services.Storage.Local;
using E_CommerceApi.Infrastructure.Services.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage,IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.AWS:
                    break;
                case StorageType.Azure:
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
