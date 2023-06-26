﻿using E_CommerceApi.Application.Features.Commands.AppUsers.LoginUser;
using E_CommerceApi.Application.Features.Commands.AppUsers.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse loginUserCommandResponse = await _mediator.Send(loginUserCommandRequest);
            return Ok(loginUserCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromForm]RefreshTokenCommandRequest refreshTokenCommandRequest)
        {
            RefreshTokenCommandResponse refreshTokenCommandResponse = await _mediator.Send(refreshTokenCommandRequest);
            return Ok(refreshTokenCommandResponse);
        }
    }
}
