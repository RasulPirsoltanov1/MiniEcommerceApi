using E_CommerceApi.Application.Abstractions;
using E_CommerceApi.Application.Features.Commands.Product.RemoveProduct;
using E_CommerceApi.Application.Features.Commands.Product.RemoveProductImage;
using E_CommerceApi.Application.Features.Commands.Product.UpdateProduct;
using E_CommerceApi.Application.Features.Commands.Product.UploadProductImage;
using E_CommerceApi.Application.Features.Queries.CreateProduct;
using E_CommerceApi.Application.Features.Queries.GetAllProducts;
using E_CommerceApi.Application.Features.Queries.Product.GetByIdProduct;
using E_CommerceApi.Application.Features.Queries.ProductImageFile.GetProductImages;
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
            return Ok(await _mediator.Send(getAllProductQueryRequest));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse getByIdProductQueryResponse = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(getByIdProductQueryResponse);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse createProductCommandResponse = await _mediator.Send(createProductCommandRequest);
            return Ok(HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            await _mediator.Send(removeProductCommandRequest);
            return Ok(HttpStatusCode.OK);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            try
            {
                uploadProductImageCommandRequest.FormFileColection = Request.Form.Files;
                await _mediator.Send(uploadProductImageCommandRequest);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImageQueryRequest getProductImageQueryRequest)
        {
            List<GetProductImageQueryResponse> getProductImageQueryResponse = await _mediator.Send(getProductImageQueryRequest);

            return Ok(getProductImageQueryResponse);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            await Task.Delay(50);
            try
            {
                removeProductImageCommandRequest.imageId = imageId;
                await _mediator.Send(removeProductImageCommandRequest);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }
    }
}
