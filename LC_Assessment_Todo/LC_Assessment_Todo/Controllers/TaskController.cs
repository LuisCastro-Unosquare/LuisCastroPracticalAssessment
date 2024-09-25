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
    }
}
