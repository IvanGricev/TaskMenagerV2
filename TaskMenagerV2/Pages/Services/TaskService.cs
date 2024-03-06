using TaskMenager.Components.dbcontroll;
using Microsoft.EntityFrameworkCore;

namespace TaskMenager.Components.services
{
    public class Taskservice : ITaskService
    {
        private readonly MyDbContext _context;

        public Taskservice(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tasks>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Tasks> AddTaskAsync(Tasks task)
        {
            _context.Tasks.Add(task);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при сохранении задачи: {ex.Message}");
                throw;
            }
            //await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Tasks> UpdateTaskAsync(Tasks task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(int Id)
        {
            var task = await _context.Tasks.FindAsync(Id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
