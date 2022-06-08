using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);

        //Task<bool> SaveAllAsync();

        Task<AppUser> GetUserByIdAsync (int id);

        Task<AppUser> GetUserByUserNameAsync(string username);
        
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams); //get all Members

        Task<MemberDto> GetMemberAsync(string userName); //get member by username
            
                 
    }
}