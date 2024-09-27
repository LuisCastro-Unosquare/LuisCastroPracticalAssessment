using LC_Assessment_Todo.Constants;
using LC_Assessment_Todo.Controllers;
using LC_Assessment_Todo.Models;
using LC_Assessment_Todo.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        #region Success Section
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
            Assert.IsType<Result<TaskDto>>(response.Value);
            Assert.True((response?.Value as Result<TaskDto>).Data.Id > 0);
        }

        [Fact]
        public void UpdateTask_ShouldReturn_200()
        {
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(x => x.Update(It.IsAny<TaskDto>()))
                .Returns(new TaskDto() { Id = 1, Title = "Test title", IsDone = false });

            var newTaskDto = new TaskDto() { Id = 0, Title = "Test", IsDone = false };
            var response = _taskController.Update(newTaskDto, mockTaskService.Object) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
            Assert.True(response is OkObjectResult);
            Assert.IsType<Result<TaskDto>>(response.Value);
            Assert.True((response?.Value as Result<TaskDto>).Data.Id > 0);
        }

        [Fact]
        public void DeleteTask_ShouldReturn_200()
        {
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(new TaskDto() { Id = 1, Title = "Test title", IsDone = false });

            var newTaskDto = new TaskDto() { Id = 0, Title = "Test", IsDone = false };
            var response = _taskController.Delete(1, mockTaskService.Object) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
            Assert.True(response is OkObjectResult);
            Assert.IsType<Result<TaskDto>>(response.Value);
            Assert.True((response?.Value as Result<TaskDto>).Data.Id > 0);
        }

        [Fact]
        public void ListTask_ShouldReturn_200()
        {
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(x => x.GetTasks(It.IsAny<int>()))
                .Returns(new List<TaskDto>(){new TaskDto{ Id = 1, Title = "Test title", IsDone = false } });

            var newTaskDto = new TaskDto() { Id = 0, Title = "Test", IsDone = false };
            var response = _taskController.List(mockTaskService.Object) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
            Assert.True(response is OkObjectResult);
            Assert.IsType<Result<List<TaskDto>>>(response.Value);
            Assert.Equal(1, (response?.Value as Result<List<TaskDto>>).Data.Count);
        }

        #endregion

        #region Internal Errors Section
        [Fact]
        public void AddNewTask_ShouldReturn_500_WhenDbDontSave()
        {
            var mockTaskService = new Mock<ITaskService>(MockBehavior.Strict);
            mockTaskService.Setup(x => x.Create(It.IsAny<TaskDto>()))
                .Returns<TaskDto>(null);

            var response = _taskController.Create(new TaskDto(), mockTaskService.Object) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.StatusCode);

            var value = response.Value as Result<TaskDto>;
            Assert.Equal("Not created", value.Error);
        }

        [Fact]
        public void UpdateTask_ShouldReturn_500_WhenDbDontSave()
        {
            var mockTaskService = new Mock<ITaskService>(MockBehavior.Strict);
            mockTaskService.Setup(x => x.Update(It.IsAny<TaskDto>()))
                .Returns<TaskDto>(null);

            var response = _taskController.Update(new TaskDto(), mockTaskService.Object) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.StatusCode);

            var value = response.Value as Result<TaskDto>;
            Assert.Equal("Not updated", value.Error);
        }

        [Fact]
        public void DeleteTask_ShouldReturn_500_WhenDbDontSave()
        {
            var mockTaskService = new Mock<ITaskService>(MockBehavior.Strict);
            mockTaskService.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns<TaskDto>(null);

            var response = _taskController.Delete(1, mockTaskService.Object) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.StatusCode);

            var value = response.Value as Result<TaskDto>;
            Assert.Equal("Not deleted", value.Error);
        }

        [Fact]
        public void ListTasks_ShouldReturn_500_WhenDbDontSave()
        {
            var mockTaskService = new Mock<ITaskService>(MockBehavior.Strict);
            mockTaskService.Setup(x => x.GetTasks(It.IsAny<int>()))
                .Returns<TaskDto>(null);

            var response = _taskController.List(mockTaskService.Object) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.StatusCode);

            var value = response.Value as Result<TaskDto>;
            Assert.Equal(ErrorConsts.NO_TASK_FOUND_ERROR_MESSAGE, value.Error);
        }
        #endregion
    }
}
