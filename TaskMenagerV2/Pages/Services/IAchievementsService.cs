using TaskMenager.Components.dbcontroll;
using TaskMenagerV2.Pages.dbcontroll;

namespace TaskMenagerV2.Pages.Services
{
    public interface IAchievementsService
    {
        Task<List<Achievements>> GetAchievementsAsync();
        Task<Achievements> UpdateAchievementAsync(Achievements achievement);

    }
}
