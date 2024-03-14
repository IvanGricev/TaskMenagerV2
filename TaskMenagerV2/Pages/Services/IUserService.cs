using TaskMenager.Components.dbcontroll;
using TaskMenagerV2.Pages.dbcontroll;

namespace TaskMenagerV2.Pages.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int UserId);
        Task<List<User>> GetUsersByEmailAsync(string Email);
        Task<User> GetUserByIdAsync(int userId);
    }
}
