using BackEndAPI.Entities;

namespace BackEndAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser appUser);
    }
}