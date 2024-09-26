using AutoMapper;
using LC_Assessment_Todo.Models;
using LC_Assessment_Todo.Models.Entities;

namespace LC_Assessment_Todo.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TodoTask, TaskDto>()
                .ReverseMap();
        }
    }
}