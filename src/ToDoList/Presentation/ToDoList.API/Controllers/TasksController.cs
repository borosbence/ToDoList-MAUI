using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.ApiKey;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class TasksController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public TasksController(ToDoDbContext context)
        {
            _context = context;
        }
        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetAllTasks()
        {
            return await _context.Tasks.OrderByDescending(x => x.ModifiedDate).ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskEntity>> GetTask(int id)
        {
            var todoTask = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (todoTask == null)
            {
                return NotFound();
            }
            return todoTask;
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskEntity todoTask)
        {
            if (id != todoTask.Id)
            {
                return BadRequest();
            }
            _context.Entry(todoTask).State = EntityState.Modified;
            try
            {
                // task.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<TaskEntity>> PostTask(TaskEntity todoTask)
        {
            _context.Tasks.Add(todoTask);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTask", new { id = todoTask.Id }, todoTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var todoTask = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (todoTask == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(todoTask);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
