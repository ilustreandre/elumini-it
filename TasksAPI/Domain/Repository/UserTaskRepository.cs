using Microsoft.EntityFrameworkCore;
using TasksAPI.Domain.Entities;
using TasksAPI.Domain.Interfaces;
using TasksAPI.Infrastructure;

namespace TasksAPI.Domain.Repository
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly DatabaseContext _databaseContext = default!;

        public UserTaskRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<UserTask?> GetByIdAsync(int id)
            => await _databaseContext.UserTasks.FindAsync(id);
        public async Task<List<UserTask>> GetAllAsync()
            => await _databaseContext.UserTasks.ToListAsync();
    }
}