using TaskFlow.Core.DTOs;

namespace TaskFlow.Core.Interfaces;

public interface IProjectService
{
    Task<List<ProjectResponse>> GetAllAsync();
    Task<ProjectDetailsResponse?> GetByIdAsync(int id);
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request);
    Task<bool> UpdateAsync(int id, UpdateProjectRequest request);
    Task<bool> DeleteAsync(int id);

}