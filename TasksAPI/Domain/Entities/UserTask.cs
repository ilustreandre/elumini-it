using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasksAPI.Domain.Enums;

namespace TasksAPI.Domain.Entities
{
    public class UserTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        [Required]
        public DateTime InsertDate { get; set; } = default!;

        [Required]
        public UserTaskStatus Status { get; set; } = default!;
    }
}