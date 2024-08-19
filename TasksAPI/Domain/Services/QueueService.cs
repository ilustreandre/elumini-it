﻿using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TasksAPI.Domain.DTOs;
using TasksAPI.Domain.Interfaces;

namespace TasksAPI.Domain.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _configuration;
        private readonly string _queueName;
        private readonly string _queueHostName;

        public QueueService(IConfiguration configuration)
        {
            _configuration = configuration;
            _queueName = _configuration.GetValue<string>("QueueName") ?? string.Empty;
            _queueHostName = _configuration.GetValue<string>("QueueHostName") ?? "localhost";
        }

        public void EnqueueUserTask(UserTaskDTO userTaskDTO)
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

                var json = JsonSerializer.Serialize(userTaskDTO);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}