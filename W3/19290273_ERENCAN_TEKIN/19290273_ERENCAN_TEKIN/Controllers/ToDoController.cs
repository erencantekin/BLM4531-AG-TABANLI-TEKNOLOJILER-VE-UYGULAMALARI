using _19290273_ERENCAN_TEKIN.Data;
using _19290273_ERENCAN_TEKIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace _19290273_ERENCAN_TEKIN.Controllers
{
    [ApiController]
    [Route("api/ToDoTasks")]
    public class ToDoController : Controller
    {
        private readonly TasksAPIDbContext dbContext;
        public ToDoController(TasksAPIDbContext dbContext)
        {
            this.dbContext = dbContext;   
        }
        [HttpGet]
        [Route("{id:guid}")] // to get Id only
        public async Task<IActionResult> GetSpecificTask([FromRoute] Guid id) 
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if(task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet] // Get All Tasks
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await dbContext.Tasks.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddTasks(AddToDoTasksRequest addToDoTasksRequest)
        {
            var toDotasks = new ToDoTasks()
            {
                Id = Guid.NewGuid(),
                TaskTitle = addToDoTasksRequest.TaskTitle,
                TaskDescription = addToDoTasksRequest.TaskDescription,
                Priority = addToDoTasksRequest.Priority,
                isDone = addToDoTasksRequest.isDone,
            };

            await dbContext.Tasks.AddAsync(toDotasks);
            await dbContext.SaveChangesAsync();

            return Ok(toDotasks);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTasks([FromRoute] Guid id, UpdateToDoTasksRequest updateToDoTasksRequest) 
        {
            var task = await dbContext.Tasks.FindAsync(id);
            if(task != null)
            {
                task.TaskTitle = updateToDoTasksRequest.TaskTitle;
                task.TaskDescription = updateToDoTasksRequest.TaskDescription;
                task.Priority = updateToDoTasksRequest.Priority;
                task.isDone = updateToDoTasksRequest.isDone;

                await dbContext.SaveChangesAsync();

                return Ok(task); // Success Response
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if(task != null) // it is found
            {
                dbContext.Remove(task);
                await dbContext.SaveChangesAsync();
                return Ok(task);
            }

            return NotFound();
        }

        [HttpGet("MAXPriorityTasks")] // Get All MAX Priority Tasks
        public async Task<IActionResult> GetTasksMAXPriorityTasks()
        {
            var MAXPriorityTasks = await dbContext.Tasks.ToListAsync();
            List<ToDoTasks> tasksToReturn = new List<ToDoTasks>();

            foreach(var task in MAXPriorityTasks)
            {
                if(task.Priority == 2)
                {
                    tasksToReturn.Add(task);
                }
            }
            return Ok(tasksToReturn);
        }
        [HttpGet("MINPriorityTasks")] // Get All MIN Priority Tasks
        public async Task<IActionResult> GetTasksMINPriorityTasks()
        {
            var MAXPriorityTasks = await dbContext.Tasks.ToListAsync();
            List<ToDoTasks> tasksToReturn = new List<ToDoTasks>();

            foreach (var task in MAXPriorityTasks)
            {
                if (task.Priority == 0)
                {
                    tasksToReturn.Add(task);
                }
            }
            return Ok(tasksToReturn);
        }
        [HttpGet("AVGPriorityTasks")] // Get All AVG Priority Tasks
        public async Task<IActionResult> GetTasksAVGPriorityTasks()
        {
            var MAXPriorityTasks = await dbContext.Tasks.ToListAsync();
            List<ToDoTasks> tasksToReturn = new List<ToDoTasks>();

            foreach (var task in MAXPriorityTasks)
            {
                if (task.Priority == 1)
                {
                    tasksToReturn.Add(task);
                }
            }
            return Ok(tasksToReturn);
        }

    }
}
