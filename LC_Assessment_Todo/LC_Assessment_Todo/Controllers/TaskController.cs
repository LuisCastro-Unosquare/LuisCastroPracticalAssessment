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
                return this.Ok(createdTask);
            } else {
                // TODO: Implement exception handler
                throw new NotImplementedException("TODO implement");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskDto task, [FromServices] ITaskService taskService)
        {
            if (task == null)
                return BadRequest();

            var updatedTask = taskService.Update(task);
            if (updatedTask != null)
            {
                return this.Ok(updatedTask);
            }
            else
            {
                // TODO: Implement exception handler
                throw new NotImplementedException("TODO implement");
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
                return this.Ok(tasksDto);
            }
            else
            {
                return NotFound();
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
