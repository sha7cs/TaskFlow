using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.Entities;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> _tasks = new();
    private static int _nextId = 1;

    // GET: api/tasks
    [HttpGet]
    public ActionResult<List<TaskItem>> GetAll()
    {
        return Ok(_tasks);
    }

    // GET: api/tasks/5
    [HttpGet("{id:int}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
            return NotFound();

        return Ok(task);
    }

    // POST: api/tasks
    [HttpPost]
    public ActionResult<TaskItem> Create(TaskItem task)
    {
        task.Id = _nextId++;
        task.CreatedAt = DateTime.UtcNow;
        _tasks.Add(task);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    // PUT: api/tasks/5
    [HttpPut("{id:int}")]
    public ActionResult Update(int id, TaskItem updatedTask)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
            return NotFound();
        
        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;
        return NoContent();
    }

    // DELETE: api/tasks/5
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t=> t.Id==id);
        if (task is null)
            return NotFound();
        _tasks.Remove(task);
        return NoContent();
    }
}