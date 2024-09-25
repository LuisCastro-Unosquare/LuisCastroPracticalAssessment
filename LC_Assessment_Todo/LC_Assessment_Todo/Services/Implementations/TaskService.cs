using LC_Assessment_Todo.Context;
using LC_Assessment_Todo.Models;
using LC_Assessment_Todo.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LC_Assessment_Todo.Services.Implementations
{
    /// <summary>
    /// Service to manage CRUD operation over the <see cref="TodoDbContext.Tasks"/>
    /// </summary>
    public class TaskService : ITaskService
    {
        private TodoDbContext _todoDbContext;

        /// <summary>
        /// Initialices the service, injecting <see cref="TodoDbContext"/>
        /// </summary>
        /// <param name="todoDbContext"></param>
        public TaskService([FromServices] TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        /// <summary>
        /// Create a new <see cref="TodoTask"/> from <see cref="TaskDto"/> and added to the <see cref="TodoDbContext.Tasks"/>
        /// </summary>
        /// <param name="taskDto"></param>
        /// <returns></returns>
        public TaskDto Create(TaskDto taskDto)
        {
            var newTodoTaskRecord = new TodoTask()
            {
                Title = taskDto.Title,
                IsDone = taskDto.IsDone
            };

            _todoDbContext.Tasks.Add(newTodoTaskRecord);
            
            if(_todoDbContext.SaveChanges() == 1)
            {
                taskDto.Id = newTodoTaskRecord.Id;
            }

            return taskDto;
        }
    }
}
