using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Core.Mappings;
using TaskFlow.Core.DTOs;

namespace TaskFlow.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskResponse>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .Select(t => t.ToResponse())
            .ToListAsync();
    }

    public async Task<TaskResponse?> GetByIdAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        return task?.ToResponse();
    }

    public async Task<TaskResponse?> CreateAsync(CreateTaskRequest request)
    {
        var project = await _context.Projects.AnyAsync(p => p.Id == request.ProjectId);
        if (!project)
            return null;

        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow,
            ProjectId = request.ProjectId
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return task.ToResponse();
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskRequest request)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task is null)
            return false;

        task.Title = request.Title;
        task.Description = request.Description;
        task.IsCompleted = request.IsCompleted;
        task.DueDate = request.DueDate;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task is null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}