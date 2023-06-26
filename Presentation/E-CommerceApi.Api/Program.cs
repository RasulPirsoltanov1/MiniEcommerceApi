using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Application.Validators.Products;
using E_CommerceApi.Infrastructure;
using E_CommerceApi.Infrastructure.Filters;
using E_CommerceApi.Persistence;
using E_CommerceApi.Persistence.Concretes;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Features;
using E_CommerceApi.Infrastructure.Services.Storage.Local;
using E_CommerceApi.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using System.Security.Claims;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using Microsoft.AspNetCore.HttpLogging;
using E_CommerceApi.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});
builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage(E_CommerceApi.Infrastructure.Enums.StorageType.Local);
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddMvc();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200",
    "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

Logger logger1 = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/log.txt").WriteTo.MSSqlServer(builder.Configuration["ConnectionStrings:SqlServer"], "logs", autoCreateSqlTable: true)
    .MinimumLevel.Information().CreateLogger();
builder.Host.UseSerilog(logger1);
// Add services to the container.

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllersWithViews(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(configurtion =>
                {
                    configurtion.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
                }).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Token:Auidence"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.Name
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Use( async (context, next) =>
{
     await next();
});

app.MapControllers();

app.Run();
