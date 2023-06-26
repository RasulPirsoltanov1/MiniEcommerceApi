using Azure.Core;
using E_CommerceApi.Application.Abstractions.Services;
using E_CommerceApi.Application.DTOs.User;
using E_CommerceApi.Application.Exceptions;
using E_CommerceApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Persistence.Services
{
    public class UserService : IUserService
    {
        private UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser createUser)
        {

            string errorList = String.Empty;
            IdentityResult identityResult = await _userManager.CreateAsync(new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = createUser.NameSurname,
                Email = createUser.Email,
                UserName = createUser.UserName,
            }, createUser.Password);
            if (identityResult.Succeeded)
            {
                return new()
                {
                    Succeded = true,
                    Message = "User created Succesfully"
                };
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    errorList += $"{error.Code}: {error.Description}\n";
                }
                return new()
                {
                    Message = errorList,
                    Succeded = false
                };
            }
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser appUser,DateTime accessTokenTime, int refreshTokenTime)
        {
            if (appUser != null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenEndDate = accessTokenTime.AddMinutes(refreshTokenTime);
                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                throw new NotFoundUserException();
            }
        }
    }
}
