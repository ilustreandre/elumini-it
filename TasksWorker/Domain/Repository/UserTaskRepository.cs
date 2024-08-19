using TasksWorker.Domain.Entities;
using TasksWorker.Domain.Interfaces;
using TasksWorker.Infrastructure;

namespace TasksWorker.Domain.Repository
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly DatabaseContext _databaseContext = default!;

        public UserTaskRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddAsync(UserTask userTask)
        {
            try
            {
                await _databaseContext.UserTasks.AddAsync(userTask);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}