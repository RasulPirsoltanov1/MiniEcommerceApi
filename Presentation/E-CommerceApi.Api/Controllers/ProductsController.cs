using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Application.Features.Queries.CreateProduct;
using E_CommerceApi.Application.Features.Queries.GetAllProducts;
using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Application.RequestParameters;
using E_CommerceApi.Application.Services;
using E_CommerceApi.Application.ViewModels.Products;
using E_CommerceApi.Domain.Entities;
using E_CommerceApi.Infrastructure.Services;
using E_CommerceApi.Persistence.Concretes.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_CommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private IFileReadRepository _fileReadRepository;
        private IFileWriteRepository _fileWriteRepository;
        private IProductImageFileReadRepository _productImageFileReadRepository;
        private IProductImageFileWriteRepository _productImageFileWriteRepository;
        private IInvoiceFileReadRepository _invoiceFileReadRepository;
        private IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private IStorageService _storageService;
        private readonly IMediator _mediator;
        public ProductsController(
                                  IProductReadRepository productReadRepository,
                                  IProductWriteRepository productWriteRepository,
                                  IWebHostEnvironment webHostEnvironment,
                                  IFileService fileService,
                                  IFileWriteRepository fileWriteRepository,
                                  IFileReadRepository fileReadRepository,
                                  IInvoiceFileReadRepository invoiceFileReadRepository,
                                  IProductImageFileReadRepository productImageFileReadRepository,
                                  IProductImageFileWriteRepository productImageFileWriteRepository,
                                  IInvoiceFileWriteRepository invoiceFileWriteRepository,
                                  IStorageService storageService,
                                  IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileWriteRepository = fileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            await Task.Delay(5);
            return Ok(await _mediator.Send(getAllProductQueryRequest));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse createProductCommandResponse = await _mediator.Send(createProductCommandRequest);
            return Ok(HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productReadRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await Task.Delay(5);
            try
            {
                await _productWriteRepository.RemoveAsync(id);
                await _productWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            Random random = new Random();
            try
            {
                //var datas = _storageService.UploadAsync(Request.Form.Files, "rr", "ew");

                //_invoiceFileWriteRepository.AddRangeAsync(new List<InvoiceFile>(datas.Select(d => new InvoiceFile()
                //{
                //    FileName = d.fileName,
                //    Path = d.path,
                //    Price = new Random().Next()
                //}).ToList()));
                //await _invoiceFileWriteRepository.SaveAsync();
                var product = await _productReadRepository.GetByIdAsync(id);
                var datas = await _storageService.UploadAsync(Request.Form.Files, "rr", "ew");
                await _productImageFileWriteRepository.AddRangeAsync(datas.Select(f => new ProductImageFile()
                {
                    FileName = f.fileName,
                    Path = f.path,
                    Storage = _storageService.StorageName,
                    Products = new List<Product>() { product }
                }).ToList());
                await _productImageFileWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            var product = await _productReadRepository.Table.Include(pi => pi.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            if (product != null)
            {
                await Task.Delay(300);
                return Ok(product.ProductImageFiles.Select(p => new
                {
                    p.Path,
                    p.FileName,
                    p.Id
                }));
            }
            return Ok();
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            await Task.Delay(50);
            try
            {
                var product = await _productReadRepository.Table.Include(pi => pi.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
                ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
                product.ProductImageFiles.Remove(productImageFile);
                await _productImageFileReadRepository.SaveAsync();
                await _productImageFileWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }
    }
}
