using BackEndAPI.Entities;

namespace BackEndAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}