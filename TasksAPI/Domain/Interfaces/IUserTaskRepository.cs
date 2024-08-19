using TasksAPI.Domain.Entities;

namespace TasksAPI.Domain.Interfaces
{
    public interface IUserTaskRepository
    {
        Task<UserTask?> GetByIdAsync(int id);
        Task<List<UserTask>> GetAllAsync();
    }
}