using Microsoft.EntityFrameworkCore;
using TasksAPI.Domain.Entities;
using TasksAPI.Domain.Interfaces;
using TasksAPI.Infrastructure;

namespace TasksAPI.Domain.Repository
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly DatabaseContext _databaseContext = default!;
        private readonly ILogs _logs;

        public UserTaskRepository(DatabaseContext databaseContext, ILogs logs)
        {
            _databaseContext = databaseContext;
            _logs = logs;
        }

        public async Task<List<UserTask>> GetAllAsync()
        {
            var userTasks = new List<UserTask>();

            try
            {
                userTasks = await _databaseContext.UserTasks.ToListAsync();
            }
            catch (Exception ex)
            {
                _logs.LogInfo($"Exception ocurred at: {DateTimeOffset.Now}. Message: {ex.Message} | InnerException: {ex.InnerException}");
            }

            return userTasks;
        }
    }
}