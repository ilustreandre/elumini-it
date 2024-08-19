using TasksAPI.Domain.Enums;

namespace TasksAPI.Domain.DTOs
{
    public sealed class UserTaskDTO
    {
        public string Description { get; set; } = default!;

        public DateTime InsertDate { get; set; } = default!;

        public string InsertDateFormatted { get => InsertDate.ToLongDateString(); }

        public UserTaskStatus Status { get; set; } = default!;

        public string StatusFormatted
        {
            get
            {
                return Status switch
                {
                    UserTaskStatus.NewTask => "Nova Tarefa",
                    UserTaskStatus.InProgress => "Em Progresso",
                    UserTaskStatus.Completed => "Completada",
                    UserTaskStatus.Canceled => "Cancelada",
                    _ => string.Empty
                };
            }
        }
    }
}