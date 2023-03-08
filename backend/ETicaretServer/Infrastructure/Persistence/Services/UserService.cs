using Application.Abstractions.Services;
using Application.Dto_s.User;
using Application.Exceptions;
using Application.Features.Commands.AppUser.CreateUser;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                nameSurname = model.NameSurname,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "User Has Been Created";
            else
                foreach (var error in result.Errors)
                    response.Message += $" {error.Code} - {error.Description},";
            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, User user,DateTime AccessTokenDate, int refreshTokenLifeTime)
        {
            if(user != null)
            {
                user.RefreshToken= refreshToken;
                user.RefreshTokenEndDate = AccessTokenDate.AddMinutes(refreshTokenLifeTime);
                await _userManager.UpdateAsync(user);
            }
            else
            throw new NotFoundUserException();
        }
    }
}
