using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentications
    {
        Task<Dto_s.Token> LoginAsync(string usernameOrEmail,string password,int accessTokenLifeTime);
        Task<Dto_s.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
