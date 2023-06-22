using Azure.Core;
using E_CommerceApi.Application.Abstractions.Services;
using E_CommerceApi.Application.Abstractions.Tokens;
using E_CommerceApi.Application.DTOs;
using E_CommerceApi.Application.Exceptions;
using E_CommerceApi.Application.Features.Commands.AppUsers.LoginUser;
using E_CommerceApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private ITokenHandler _tokenHandler;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> LoginAsync(string userNameOrEmail, string password,int accessTokenLifetime)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(userNameOrEmail);
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(userNameOrEmail);
                if (appUser == null)
                {
                    throw new NotFoundUserException("Wrong Username or Passoword");
                }
            }
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);
            if (signInResult.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifetime);
                return token;
            }
            throw new AuthenticationErrorException();
        }
    }
}
