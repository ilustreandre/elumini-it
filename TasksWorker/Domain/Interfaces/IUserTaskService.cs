using TasksWorker.Domain.Entities;

namespace TasksWorker.Domain.Interfaces
{
    public interface IUserTaskService
    {
        Task AddAsync(UserTask userTask);
    }
}