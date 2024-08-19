using TasksWorker.Domain.Entities;

namespace TasksWorker.Domain.Interfaces
{
    public interface IUserTaskRepository
    {
        Task AddAsync(UserTask userTask);
    }
}