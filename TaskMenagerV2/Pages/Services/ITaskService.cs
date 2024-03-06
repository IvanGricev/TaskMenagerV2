using TaskMenager.Components.dbcontroll;

namespace TaskMenager.Components.services
{
    public interface ITaskService
    {
        Task<List<Tasks>> GetTasksAsync();
        Task<Tasks> AddTaskAsync(Tasks task);
        Task<Tasks> UpdateTaskAsync(Tasks task);
        Task DeleteTaskAsync(int taskId);
    }
}
