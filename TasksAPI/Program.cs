using Microsoft.EntityFrameworkCore;
using TasksAPI.Controllers;
using TasksAPI.Domain.Interfaces;
using TasksAPI.Domain.Repository;
using TasksAPI.Domain.Services;
using TasksAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddScoped<IUserTaskService, UserTaskService>();
builder.Services.AddScoped<IQueueService, QueueService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapUserTaskEndpoints();

app.Run();
