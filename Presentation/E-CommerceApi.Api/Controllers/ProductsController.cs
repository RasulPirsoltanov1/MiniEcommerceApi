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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_CommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
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
