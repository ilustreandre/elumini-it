using TasksAPI.Domain.DTOs;
using TasksAPI.Domain.Entities;
using TasksAPI.Domain.Interfaces;

namespace TasksAPI.Domain.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _userTaskRepository = default!;
        private readonly IQueueService _queueService = default!;
        private readonly ILogs _logs;

        public UserTaskService(IUserTaskRepository userTaskRepository, IQueueService queueService, ILogs logs)
        {
            _userTaskRepository = userTaskRepository;
            _queueService = queueService;
            _logs = logs;
        }

        public async Task<List<UserTaskDTO>> GetAllAsync()
        {
            try
            {
                var list = await _userTaskRepository.GetAllAsync();

                if (list is not null)
                    return list.Select(x => EntityToDTO(x)).ToList();
            }
            catch (Exception ex)
            {
                _logs.LogInfo($"Exception ocurred at: {DateTimeOffset.Now}. Message: {ex.Message} | InnerException: {ex.InnerException}");
            }

            return [];
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