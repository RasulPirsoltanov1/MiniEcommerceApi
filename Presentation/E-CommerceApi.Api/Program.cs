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

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});
builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage(E_CommerceApi.Infrastructure.Enums.StorageType.Local);
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddMvc();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200",
    "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllersWithViews(options=>options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(configurtion =>
                {
                    configurtion.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
                }).ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
