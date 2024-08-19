using TasksAPI.Domain.Enums;

namespace TasksAPI.Domain.DTOs
{
    public sealed class UserTaskDTO
    {
        public string Description { get; set; } = default!;

        public DateTime InsertDate { get; set; } = default!;

        public UserTaskStatus Status { get; set; } = default!;
    }
}