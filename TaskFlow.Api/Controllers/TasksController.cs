using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.Interfaces;
using TaskFlow.Core.DTOs;
namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    // GET: api/tasks
    [HttpGet]
    public async Task<ActionResult<List<TaskResponse>>> GetAll()
    {
        return Ok(await _taskService.GetAllAsync());
    }

    // GET: api/tasks/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskResponse>> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskResponse>> Create(CreateTaskRequest request)
    {
        var created = await _taskService.CreateAsync(request);
        if (created is null)
            return BadRequest("Project not found for the given ProjectId.");
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/tasks/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateTaskRequest request)
    {
        var updated = await _taskService.UpdateAsync(id, request);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/tasks/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _taskService.DeleteAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}