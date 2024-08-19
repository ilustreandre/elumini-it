using Microsoft.EntityFrameworkCore;
using TasksWorker;
using TasksWorker.Domain.Interfaces;
using TasksWorker.Domain.Repository;
using TasksWorker.Domain.Services;
using TasksWorker.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IUserTaskRepository, UserTaskRepository>();

builder.Services.AddScoped<IUserTaskService, UserTaskService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddSingleton<ILogs, Logs>();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
}, ServiceLifetime.Singleton);

var host = builder.Build();
host.Run();
