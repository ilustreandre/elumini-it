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

        public async Task<UserTaskDTO?> GetByIdAsync(int id)
        {
            var userTask = await _userTaskRepository.GetByIdAsync(id);
            var userTaskDTO = EntityToDTO(userTask);
            return userTaskDTO;
        }

        public async Task<List<UserTaskDTO>> GetAllAsync()
        {
            var list = await _userTaskRepository.GetAllAsync();

            if (list is null)
                return [];

            return list.Select(x => EntityToDTO(x)).ToList();
        }

        public void Add(UserTaskDTO userTaskDTO)
            => _queueService.EnqueueUserTask(userTaskDTO);

        private UserTaskDTO EntityToDTO(UserTask? userTask)
        {
            if (userTask is null)
                return new();

            return new UserTaskDTO()
            {
                Description = userTask.Description,
                InsertDate = userTask.InsertDate,
                Status = userTask.Status
            };
        }
    }
}