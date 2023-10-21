﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<ToDoTask>>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTask>> GetTask(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, ToDoTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            _context.Entry(task).State = EntityState.Modified;
            try
            {
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
        public async Task<ActionResult<ToDoTask>> PostTask(ToDoTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
