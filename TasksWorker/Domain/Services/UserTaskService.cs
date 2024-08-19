using TasksWorker.Domain.Entities;
using TasksWorker.Domain.Interfaces;

namespace TasksWorker.Domain.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _userTaskRepository = default!;

        public UserTaskService(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        public async Task AddAsync(UserTask userTask)
        {
            try
            {
                await _userTaskRepository.AddAsync(userTask);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}