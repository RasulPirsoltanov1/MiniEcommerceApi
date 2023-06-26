using E_CommerceApi.Application.DTOs.User;
using E_CommerceApi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser createUser);
        Task UpdateRefreshToken(string refreshToken,AppUser appUser,DateTime accessTokenTime, int refreshTokenTime);
    }
}
