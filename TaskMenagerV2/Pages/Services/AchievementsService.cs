using Microsoft.EntityFrameworkCore;
using TaskMenagerV2.Pages.dbcontroll;

namespace TaskMenagerV2.Pages.Services
{
    public class AchievementsService : IAchievementsService
    {
        private readonly MyDbContext _context;

        public AchievementsService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Achievements>> GetAchievementsAsync()
        {
            return await _context.Achievements.ToListAsync();
        }

        public async Task<Achievements> UpdateAchievementAsync(Achievements achievement)
        {
            _context.Achievements.Update(achievement);
            await _context.SaveChangesAsync();
            return achievement;
        }
    }
}
