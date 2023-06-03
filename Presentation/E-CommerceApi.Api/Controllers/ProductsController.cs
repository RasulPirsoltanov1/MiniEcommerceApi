using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Application.Repositories;
using E_CommerceApi.Application.RequestParameters;
using E_CommerceApi.Application.Services;
using E_CommerceApi.Application.ViewModels.Products;
using E_CommerceApi.Domain.Entities;
using E_CommerceApi.Infrastructure.Services;
using E_CommerceApi.Persistence.Concretes.Customers;
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
                                  IStorageService storageService)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            await Task.Delay(5);

            var totalCount = _productReadRepository.GetAll().Count();

            var products = _productReadRepository.GetAll().Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.CreatedDate,
                p.UpdatedDate
            }).Skip(pagination.Size * pagination.Page).Take(pagination.Size);
            return Ok(new
            {
                products = products,
                totalCount = totalCount
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new Product
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return Ok();
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
        public async Task<IActionResult> Upload()
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
                await _invoiceFileWriteRepository.SaveAsync();
                var datas =await _storageService.UploadAsync(Request.Form.Files, "rr", "ew");
                await _productImageFileWriteRepository.AddRangeAsync(datas.Select(f=>new ProductImageFile()
                {
                    FileName=f.fileName,
                    Path=f.path,
                    Storage= _storageService.StorageName
                }).ToList());
                await _productImageFileWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }
    }
}
