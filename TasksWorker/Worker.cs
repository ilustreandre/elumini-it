using TasksWorker.Domain.Interfaces;

namespace TasksWorker
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public Worker(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var queueService = scope.ServiceProvider.GetRequiredService<IQueueService>();
                    await queueService.ConsumeUserTasks();
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}