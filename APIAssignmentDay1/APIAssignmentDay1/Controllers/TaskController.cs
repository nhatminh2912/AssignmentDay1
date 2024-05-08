using APIAssignmentDay1.Models;
using Microsoft.AspNetCore.Mvc;
namespace APIAssignmentDay1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;

        // POST: api/tasks
        // 1. Create new task with unique ID.
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] TaskItem newTask)
        {
            newTask.Id = nextId++;
            tasks.Add(newTask);

            // Return the created task asynchronously
            return await Task.FromResult(CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask));
        }

        // GET: api/tasks
        // 2. List all tasks created.
        [HttpGet]
        public async Task<IEnumerable<TaskItem>> GetTasks()
        {
            // Asynchronously return the list of tasks
            return await Task.FromResult(tasks);
        }

        // GET: api/tasks/5
        // 3. Select a specified task.
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            // Asynchronously return the task
            return await Task.FromResult(task);
        }

        // DELETE: api/tasks/5
        // 4. Delete a specified task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            tasks.Remove(task);

            // Asynchronously return NoContent status
            return await Task.FromResult(NoContent());
        }

        // PUT: api/tasks/5
        // 5. Edit title / completion of a task.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, [FromBody] TaskItem updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            task.Title = updatedTask.Title;
            task.IsCompleted = updatedTask.IsCompleted;

            // Asynchronously return NoContent status
            return await Task.FromResult(NoContent());
        }

        

        // POST: api/tasks/bulk
        // 6. Add bulk of tasks in one request
        [HttpPost("bulk")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> PostBulkTasks([FromBody] List<TaskItem> newTasks)
        {
            foreach (var newTask in newTasks)
            {
                newTask.Id = nextId++;
                tasks.Add(newTask);
            }

            // Return the list of created tasks
            return await Task.FromResult(Ok(newTasks));
        }

        // DELETE: api/tasks/bulk
        // 7. Delete bulk of tasks in one request.
        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteBulkTasks([FromBody] List<int> taskIds)
        {
            var tasksToDelete = tasks.Where(t => taskIds.Contains(t.Id)).ToList();
            if (!tasksToDelete.Any())
            {
                return NotFound();
            }

            tasks.RemoveAll(t => tasksToDelete.Contains(t));

            // Asynchronously return NoContent status
            return await Task.FromResult(NoContent());
        }
    }
}

