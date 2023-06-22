using E_CommerceApi.Application.Abstractions.Services;
using E_CommerceApi.Application.Abstractions.Tokens;
using E_CommerceApi.Application.DTOs;
using E_CommerceApi.Application.Exceptions;
using E_CommerceApi.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Commands.AppUsers.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private ITokenHandler _tokenHandler;
        private IAuthService _authService;
        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password,5);
            if (token != null)
            {
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            return new LoginUserErrorCommandResponse()
            {
                Message = "you cant login!"
            };
        }
    }
}
