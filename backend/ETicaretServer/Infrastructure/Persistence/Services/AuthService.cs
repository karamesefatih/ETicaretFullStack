using Application.Abstractions.Services;
using Application.Abstractions.Token;
using Application.Dto_s;
using Application.Exceptions;
using Application.Features.Commands.AppUser.LoginUser;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password,int accessTokenLifeTime)
        {
            //username olup olmadığını kontrol ediyoruz
            User user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
            {
                //Email olup olmadığını kontrol ediyoruz
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            }
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            //Authetice olup olmadığını kontrol eder
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                //yetkiler belirlenecek
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,user);
                await _userService.UpdateRefreshToken(token.refreshToken, user, token.Expiration, 2);
                return token;
            }
            throw new NotFoundUserException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            User? user =  await _userManager.Users.FirstOrDefaultAsync(u=>u.RefreshToken == refreshToken);
            if(user != null && user?.RefreshTokenEndDate > DateTime.UtcNow.AddHours(3))
            {
                Token token = _tokenHandler.CreateAccessToken(15,user);
                await _userService.UpdateRefreshToken(token.refreshToken, user, token.Expiration, 15);
                return token;
            }
            else
            {
                throw new NotFoundUserException();
            }
        }
    }
}
