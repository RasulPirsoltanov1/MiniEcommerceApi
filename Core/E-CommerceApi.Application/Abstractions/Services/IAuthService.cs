using E_CommerceApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string userNameOrEmail,string password,int accessTokenLifetime);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
