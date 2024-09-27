using LC_Assessment_Todo.Constants;
using LC_Assessment_Todo.Models;
using LC_Assessment_Todo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LC_Assessment_Todo.Controllers
{
    [Authorize,Route("task")]
    public class TaskController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody]TaskDto task, [FromServices] ITaskService taskService)
        {
            if (task == null)
                return BadRequest();

            var createdTask = taskService.Create(task);
            if (createdTask != null)
            {
                return this.Ok(new Result<TaskDto>(createdTask));
            } else {
                return this.BadRequest(new Result<TaskDto>("Not created"));
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskDto task, [FromServices] ITaskService taskService)
        {
            if (task == null)
                return BadRequest("Task object must be provided");

            var updatedTask = taskService.Update(task);
            if (updatedTask != null)
            {
                return this.Ok(new Result<TaskDto>(updatedTask));
            }
            else
            {
                return this.BadRequest(new Result<TaskDto>("Not updated"));
            }
        }

        [HttpGet("{taskId:int}")]
        public IActionResult Read([FromRoute] int taskId, [FromServices] ITaskService taskService)
        {
            if (taskId <= 0)
                return BadRequest();

            var taskDetailsDto = taskService.GetTaskDetails(taskId);
            if (taskDetailsDto != null)
            {
                return this.Ok(taskDetailsDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("list")]
        public IActionResult List([FromServices] ITaskService taskService)
        {
            var tasksDto = taskService.GetTasks(0);
            if (tasksDto != null)
            {
                return this.Ok(new Result<List<TaskDto>>(tasksDto));
            }
            else
            {
                return this.NotFound(new Result<List<TaskDto>>(ErrorConsts.NO_TASK_FOUND_ERROR_MESSAGE));
            }
        }

        [HttpDelete("{taskId:int}")]
        public IActionResult Delete([FromRoute] int taskId, [FromServices] ITaskService taskService)
        {
            if (taskId <= 0)
                return BadRequest();

            var taskDetailsDto = taskService.Delete(taskId);
            if (taskDetailsDto != null)
            {
                return this.Ok(taskDetailsDto);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
