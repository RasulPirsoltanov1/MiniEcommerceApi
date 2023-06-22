using E_CommerceApi.Application.Abstractions.Services;
using E_CommerceApi.Application.DTOs.User;
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
        private IUserService _userService;

        public CreateUserCommandHandler(UserManager<AppUser> userManager, IUserService userService)
        {
            this._userManager = userManager;
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           CreateUserResponse createUserResponse= await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                UserName = request.UserName,
            });
            return new()
            {
                Message = createUserResponse.Message,
                Succeded = createUserResponse.Succeded
            }; 
        }
    }
}
