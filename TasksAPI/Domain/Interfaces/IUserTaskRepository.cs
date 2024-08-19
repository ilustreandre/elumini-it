using TasksAPI.Domain.Entities;

namespace TasksAPI.Domain.Interfaces
{
    public interface IUserTaskRepository
    {
        Task<List<UserTask>> GetAllAsync();
    }
}