using Microsoft.EntityFrameworkCore;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Core.Interfaces;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Mappings;
using TaskFlow.Core.Entities;

namespace TaskFlow.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectResponse>> GetAllAsync()
    {
        return await _context.Projects
            .AsNoTracking()
            .Select(p => new ProjectResponse(
                p.Id,
                p.Name,
                p.Description,
                p.CreatedAt,
                p.Tasks.Count))
            .ToListAsync();

    }

    public async Task<ProjectDetailsResponse?> GetByIdAsync(int id)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .Include(p => p.Tasks) // this brings any tasks too if any, if we dont do this then we cannot call this project tasks
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
            return null;
        return new ProjectDetailsResponse(
            project.Id,
            project.Name,
            project.Description,
            project.CreatedAt,
            project.Tasks.Select(t => t.ToResponse()).ToList()
        );
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return new ProjectResponse(
            project.Id,
            project.Name,
            project.Description,
            project.CreatedAt,
            project.Tasks.Count);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProjectRequest request)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project is null)
            return false;

        project.Name = request.Name;
        project.Description = request.Description;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project is null)
            return false;
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }
}