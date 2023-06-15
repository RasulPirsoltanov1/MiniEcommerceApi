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
        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            AppUser? appUser = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
                if (appUser == null)
                {
                    throw new NotFoundUserException("Wrong Username or Passoword");
                }
            }
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);
            if (signInResult.Succeeded)
            {
               Token token=_tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse()
                {
                    Token=token,
                };
            }
            throw new AuthenticationErrorException();
        }
    }
}
