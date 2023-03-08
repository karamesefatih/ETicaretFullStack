namespace Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Dto_s.Token CreateAccessToken(int minute);
    }
}
