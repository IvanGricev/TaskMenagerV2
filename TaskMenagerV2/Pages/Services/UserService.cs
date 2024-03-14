using Microsoft.EntityFrameworkCore;
using TaskMenager.Components.dbcontroll;
using TaskMenagerV2.Pages.dbcontroll;

namespace TaskMenagerV2.Pages.Services
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _context;

        public UserService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при сохранении user: {ex.Message}");
                throw;
            }
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetUsersByEmailAsync(string email)
        {
            return await _context.Users
                .Where(user => user.Email == email)
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId);
        }
    }
}
