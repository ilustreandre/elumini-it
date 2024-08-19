using Serilog;
using TasksAPI.Domain.Interfaces;

namespace TasksAPI.Infrastructure
{
    public class Logs : ILogs
    {
        private readonly IConfiguration _configuration;

        public Logs(IConfiguration configuration)
        {
            _configuration = configuration;

            string logPath = _configuration.GetValue<string>("LogFullPath") ?? "logs\\logs.txt";

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel
            .Debug()
            .WriteTo
            .File(logPath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }

        public void LogInfo(string message)
            => Log.Information(message);
    }
}
