using BackEndAPI.DTOs;
using BackEndAPI.Entities;
using BackEndAPI.Helpers;
using Microsoft.Extensions.Localization;

namespace BackEndAPI.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser appUser);
        // Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAysnc(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberAsync(string username);
        Task<string> GetUserGender(string username);
    }
}