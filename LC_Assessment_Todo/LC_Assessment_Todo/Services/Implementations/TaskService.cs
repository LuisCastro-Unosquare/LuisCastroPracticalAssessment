using AutoMapper;
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
        private IMapper _mapper;

        /// <summary>
        /// Initialices the service, injecting <see cref="TodoDbContext"/>
        /// </summary>
        /// <param name="todoDbContext"></param>
        public TaskService([FromServices] TodoDbContext todoDbContext, [FromServices] IMapper mapper)
        {
            _todoDbContext = todoDbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the task details by task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskDto? GetTaskDetails(int taskId)
        {
            var taskEntity = _todoDbContext.Tasks.FirstOrDefault(x => x.Id == taskId);

            if (taskEntity == null)
                return null;

            return new TaskDto() { 
                Id = taskEntity.Id,
                Title = taskEntity.Title,
                IsDone = taskEntity.IsDone
            };
        }

        /// <summary>
        /// Deletes a task by id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskDto? Delete(int taskId)
        {
            var taskEntity = _todoDbContext.Tasks.FirstOrDefault(x => x.Id == taskId);

            if (taskEntity == null)
                return null;

            _todoDbContext.Tasks.Remove(taskEntity);
            _todoDbContext.SaveChanges();

            return _mapper.Map<TaskDto>(taskEntity);
        }

        /// <summary>
        /// Create a new <see cref="TodoTask"/> from <see cref="TaskDto"/> and added to the <see cref="TodoDbContext.Tasks"/>
        /// </summary>
        /// <param name="taskDto"></param>
        /// <returns></returns>
        public TaskDto Create(TaskDto taskDto)
        {
            var newTodoTaskRecord = _mapper.Map<TodoTask>(taskDto);
            
            _todoDbContext.Tasks.Add(newTodoTaskRecord);
            
            if(_todoDbContext.SaveChanges() == 1)
            {
                taskDto.Id = newTodoTaskRecord.Id;
            }

            return taskDto;
        }

        /// <summary>
        /// Update a task
        /// </summary>
        /// <param name="taskDto"></param>
        /// <returns></returns>
        public TaskDto Update(TaskDto taskDto)
        {

            var taskEntity = _todoDbContext.Tasks.FirstOrDefault(x => x.Id == taskDto.Id);

            if (taskEntity == null)
                return null;

            taskEntity = _mapper.Map<TodoTask>(taskDto);

            _todoDbContext.Update(taskEntity);

            if (_todoDbContext.SaveChanges() != 1)
            {
                return null;
            }

            return taskDto;
        }

        /// <summary>
        /// Get the list with all the tasks registered by the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<TaskDto> GetTasks(int userId)
        {
            var userTasks = _todoDbContext.Tasks.Where(x => x.UserId == userId);

            if (userTasks.Any()) {
                return _mapper.Map<List<TaskDto>>(userTasks);
            }
            else
            {
                return null;
            }
        }
    }
}
