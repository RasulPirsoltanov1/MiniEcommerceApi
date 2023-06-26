using E_CommerceApi.Application.DTOs;
using E_CommerceApi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Abstractions.Tokens
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute,AppUser appUser);
        string CreateRefreshToken();
    }
}
