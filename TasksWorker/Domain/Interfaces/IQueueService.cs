namespace TasksWorker.Domain.Interfaces
{
    public interface IQueueService
    {
        Task ConsumeUserTasks();
    }
}