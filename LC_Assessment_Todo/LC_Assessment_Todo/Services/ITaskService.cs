using LC_Assessment_Todo.Models;

namespace LC_Assessment_Todo.Services
{
    public interface ITaskService
    {
        TaskDto Create(TaskDto taskDto);
        TaskDto Delete(int taskId);
        public TaskDto? GetTaskDetails(int taskId);
        TaskDto Update(TaskDto task);
        List<TaskDto> GetTasks(int userId);
    }
}
