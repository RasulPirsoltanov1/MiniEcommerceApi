﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace E_CommerceApi.Api.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication webApplication, ILogger<T> logger)
        {
            webApplication.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError(contextFeature.Error.Message);
                    }
                    await context.Response.WriteAsync(JsonSerializer.Serialize(
                     new
                     {
                         StatusCode = context.Response.StatusCode,
                         Message = contextFeature.Error.Message,
                         Title = "error!"
                     }
                         ));
                });
            });
        }
    }
}
