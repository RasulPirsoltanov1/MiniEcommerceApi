using E_CommerceApi.Application.Exceptions;
using E_CommerceApi.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Features.Commands.AppUsers.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            string errorList = String.Empty;
           IdentityResult identityResult= await _userManager.CreateAsync(new AppUser()
           {
               Id=Guid.NewGuid().ToString(),
               NameSurname = request.NameSurname,
               Email = request.Email,
               UserName = request.UserName,
           },request.Password);
            if (identityResult.Succeeded)
            {
                return new()
                {
                    Succeded=true,
                    Message="User created Succesfully"
                };
            }
            else
            {
                foreach(var error in identityResult.Errors)
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
    }
}
