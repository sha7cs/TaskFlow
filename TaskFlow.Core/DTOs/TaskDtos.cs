namespace TaskFlow.Core.DTOs;

public record CreateTaskRequest(
    string Title,
    string? Description,
    DateTime? DueDate,
    int ProjectId
);

public record UpdateTaskRequest(
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime? DueDate
);

public record TaskResponse(
    int Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime? DueDate,
    DateTime CreatedAt,
    int ProjectId);