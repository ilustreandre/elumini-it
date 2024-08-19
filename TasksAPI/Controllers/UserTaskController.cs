using Microsoft.AspNetCore.Mvc;
using TasksAPI.Domain.DTOs;
using TasksAPI.Domain.Interfaces;

namespace TasksAPI.Controllers;

public static class UserTaskController
{
    public static void MapUserTaskEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/UserTask").WithTags("UserTasks");

        group.MapGet("/", async (IUserTaskService userTaskService) =>
        {
            return await userTaskService.GetAllAsync();
        })
        .WithName("GetAll")
        .WithOpenApi();

        group.MapPost("/", ([FromBody] UserTaskDTO model, IUserTaskService userTaskService) =>
        {
            userTaskService.Add(model);
            return Results.Ok();
        })
        .WithName("Create")
        .WithOpenApi();
    }
}