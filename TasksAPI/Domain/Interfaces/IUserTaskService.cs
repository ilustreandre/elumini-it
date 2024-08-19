using TasksAPI.Domain.DTOs;
using TasksAPI.Domain.Entities;

namespace TasksAPI.Domain.Interfaces
{
    public interface IUserTaskService
    {
        Task<UserTask?> GetByIdAsync(int id);
        Task<List<UserTask>> GetAllAsync();
        void Add(UserTaskDTO userTaskDTO);
    }
}