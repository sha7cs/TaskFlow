using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;

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
    public async Task<ActionResult<List<TaskItem>>> GetAll()
    {
        return Ok(await _taskService.GetAllAsync());
    }

// GET: api/tasks/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        var created = await _taskService.CreateAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/tasks/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, TaskItem updatedTask)
    {
        var updated = await _taskService.UpdateAsync(id, updatedTask);
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