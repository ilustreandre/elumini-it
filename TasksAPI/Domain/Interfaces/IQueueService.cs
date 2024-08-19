using TasksAPI.Domain.DTOs;

namespace TasksAPI.Domain.Interfaces
{
    public interface IQueueService
    {
        void EnqueueUserTask(UserTaskDTO userTaskDTO);
    }
}