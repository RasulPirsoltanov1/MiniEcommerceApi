using E_CommerceApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Commands.AppUsers.RefreshToken
{
    public class RefreshTokenCommandResponse
    {
        public Token Token{ get; set; }
    }
}
