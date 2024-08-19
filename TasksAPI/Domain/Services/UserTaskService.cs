using TasksAPI.Domain.DTOs;
using TasksAPI.Domain.Entities;
using TasksAPI.Domain.Interfaces;

namespace TasksAPI.Domain.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _userTaskRepository = default!;
        private readonly IQueueService _queueService = default!;

        public UserTaskService(IUserTaskRepository userTaskRepository, IQueueService queueService)
        {
            _userTaskRepository = userTaskRepository;
            _queueService = queueService;
        }

        public async Task<UserTask?> GetByIdAsync(int id)
          => await _userTaskRepository.GetByIdAsync(id);

        public async Task<List<UserTask>> GetAllAsync()
        {
            var result = await _userTaskRepository.GetAllAsync();
            return result is not null ? result : [];
        }

        public void Add(UserTaskDTO userTaskDTO)
            => _queueService.EnqueueUserTask(userTaskDTO);
    }
}