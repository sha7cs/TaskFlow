using TaskFlow.Core.DTOs;

namespace TaskFlow.Core.Interfaces;

public interface ITaskService
{
    Task<List<TaskResponse>> GetAllAsync();
    Task<TaskResponse?> GetByIdAsync(int id);
    Task<TaskResponse?> CreateAsync(CreateTaskRequest request);
    Task<bool> UpdateAsync(int id, UpdateTaskRequest request);
    Task<bool> DeleteAsync(int id);
}