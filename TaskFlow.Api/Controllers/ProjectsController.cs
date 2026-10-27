using TaskFlow.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.DTOs;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    // GET: api/projects
    [HttpGet]
    public async Task<ActionResult<List<ProjectResponse>>> GetAll()
    {
        return Ok(await _projectService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDetailsResponse>> GetById(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project is null)
            return NotFound();

        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponse>> Create(CreateProjectRequest request)
    {
        var created = await _projectService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateProjectRequest request)
    {
        var updated = await _projectService.UpdateAsync(id, request);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _projectService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}