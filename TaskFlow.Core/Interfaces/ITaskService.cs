using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Interfaces;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<bool> UpdateAsync(int id, TaskItem updatedTask);
    Task<bool> DeleteAsync(int id);
}