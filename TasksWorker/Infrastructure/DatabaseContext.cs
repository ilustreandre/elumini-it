using Microsoft.EntityFrameworkCore;
using TasksWorker.Domain.Entities;

namespace TasksWorker.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<UserTask> UserTasks { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string? connString = _configuration.GetConnectionString("mysql")?.ToString();
                if (!string.IsNullOrWhiteSpace(connString))
                {
                    optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
                }
            }
        }
    }
}