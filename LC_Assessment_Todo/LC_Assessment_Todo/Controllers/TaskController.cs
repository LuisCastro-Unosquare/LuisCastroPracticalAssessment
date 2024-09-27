using LC_Assessment_Todo.Constants;
using LC_Assessment_Todo.Models;
using LC_Assessment_Todo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LC_Assessment_Todo.Controllers
{
    [Authorize,Route("task")]
    public class TaskController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody]TaskDto task, [FromServices] ITaskService taskService)
        {
            if (task == null)
                return this.StatusCode((int)HttpStatusCode.BadRequest, new Result<TaskDto>("Bad request"));

            var createdTask = taskService.Create(task);
            if (createdTask != null)
            {
                return this.Ok(new Result<TaskDto>(createdTask));
            } else {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, new Result<TaskDto>("Not created"));
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
                return this.StatusCode((int)HttpStatusCode.InternalServerError, new Result<TaskDto>("Not updated"));
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
                return this.Ok(new Result<TaskDto>(taskDetailsDto));
            }
            else
            {
                return this.NotFound(new Result<TaskDto>("Task not found"));
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
                return this.StatusCode((int)HttpStatusCode.InternalServerError, new Result<TaskDto>(ErrorConsts.NO_TASK_FOUND_ERROR_MESSAGE));
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
                return this.Ok(new Result<TaskDto>(taskDetailsDto));
            }
            else
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, new Result<TaskDto>("Not deleted"));
            }
        }
    }
}
