using TasksAPI.Domain.DTOs;

namespace TasksAPI.Domain.Interfaces
{
    public interface IUserTaskService
    {
        Task<UserTaskDTO?> GetByIdAsync(int id);
        Task<List<UserTaskDTO>> GetAllAsync();
        void Add(UserTaskDTO userTaskDTO);
    }
}