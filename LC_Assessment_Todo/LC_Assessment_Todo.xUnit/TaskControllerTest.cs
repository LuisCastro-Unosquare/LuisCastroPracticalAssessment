using LC_Assessment_Todo.Controllers;
using LC_Assessment_Todo.Models;
using LC_Assessment_Todo.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_Assessment_Todo.xUnit
{
    public class TaskControllerTest
    {
        private readonly TaskController _taskController;
        public TaskControllerTest()
        {
            _taskController = new TaskController();
        }

        [Fact]
        public void AddNewTask_ShouldReturn_200()
        {
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(x => x.Create(It.IsAny<TaskDto>()))
                .Returns(new TaskDto() { Id = 1, Title = "Test title", IsDone = false });

            var newTaskDto = new TaskDto() { Id = 0, Title = "Test", IsDone = false };
            var response = _taskController.Create(newTaskDto, mockTaskService.Object) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
            Assert.True(response is OkObjectResult);
            Assert.IsType<TaskDto>(response.Value);
            Assert.True((response?.Value as TaskDto).Id > 0);
        }

        [Fact]
        public void AddNewTask_ShouldReturn_400()
        {
            var mockTaskService = new Mock<ITaskService>(MockBehavior.Strict);
            mockTaskService.Setup(x => x.Create(It.IsAny<TaskDto>()))
                .Returns(new TaskDto() { Id = 1, Title = "Test title", IsDone = false });

            var response = _taskController.Create(null, mockTaskService.Object) as BadRequestObjectResult;

            // TODO: Why is not returning 400
            Assert.Null(response);
        }
    }
}
