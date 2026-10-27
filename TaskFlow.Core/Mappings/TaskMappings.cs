using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Mappings;

public static class TaskMappings
{
    public static TaskResponse ToResponse(this TaskItem task)
    {
        return new TaskResponse(
            task.Id,
            task.Title,
            task.Description,
            task.IsCompleted,
            task.DueDate,
            task.CreatedAt,
            task.ProjectId
        );
    }
}