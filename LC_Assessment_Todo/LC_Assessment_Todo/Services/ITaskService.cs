using LC_Assessment_Todo.Models;

namespace LC_Assessment_Todo.Services
{
    public interface ITaskService
    {
        TaskDto Create(TaskDto taskDto);
    }
}
