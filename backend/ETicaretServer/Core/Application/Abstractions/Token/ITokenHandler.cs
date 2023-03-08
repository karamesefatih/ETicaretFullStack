using Domain.Entities.Identity;

namespace Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Dto_s.Token CreateAccessToken(int minute,User user);
        string CreateRefreshToken();
    }
}
