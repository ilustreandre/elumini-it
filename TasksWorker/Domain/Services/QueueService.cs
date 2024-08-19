using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TasksWorker.Domain.Entities;
using TasksWorker.Domain.Interfaces;

namespace TasksWorker.Domain.Services
{
    public class QueueService : IQueueService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration = default!;
        private readonly IUserTaskService _userTaskService = default!;

        private readonly string _queueName;
        private readonly string _queueHostName;

        public QueueService(ILogger<Worker> logger, IConfiguration configuration, IUserTaskService userTaskService)
        {
            _logger = logger;
            _configuration = configuration;
            _userTaskService = userTaskService;
            _queueName = _configuration.GetValue<string>("QueueName") ?? string.Empty;
            _queueHostName = _configuration.GetValue<string>("QueueHostName") ?? "localhost";
        }

        public async Task ConsumeUserTasks()
        {
            var factory = new ConnectionFactory { HostName = _queueHostName, Port = AmqpTcpEndpoint.UseDefaultPort };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var result = channel.BasicGet(queue: _queueName, autoAck: false);

                if (result is not null)
                {
                    try
                    {
                        var data = Encoding.UTF8.GetString(result.Body.Span);
                        var userTask = JsonSerializer.Deserialize<UserTask>(data);

                        if (userTask is not null)
                            await _userTaskService.AddAsync(userTask);

                        channel.BasicAck(result.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        if (_logger.IsEnabled(LogLevel.Information))
                        {
                            _logger.LogInformation("Exception ocurred at: {time}. Message: {message} | InnerException: {innerException}", DateTimeOffset.Now, ex.Message, ex.InnerException);
                        }

                        channel.BasicNack(result.DeliveryTag, false, true);
                    }
                }

            }
        }
    }
}
