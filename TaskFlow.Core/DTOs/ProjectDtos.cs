namespace TaskFlow.Core.DTOs;

public record CreateProjectRequest(string Name, string? Description);

public record UpdateProjectRequest(string Name, string? Description);

// For the list: summary info only
public record ProjectResponse(
    int Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    int TaskCount
);

// For a single project: includes its tasks
public record ProjectDetailsResponse(
    int Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    List<TaskResponse> Tasks
);