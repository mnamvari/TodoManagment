using AutoMapper;
using Moq;
using System;
using TodoManagement.Appliaction.Contracts.Persistence;
using TodoManagement.Appliaction.DTOs;
using TodoManagement.Appliaction.Profiles;
using TodoManagment.Application.Test.Mocks;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TodoManagment.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoManagment.Application.Test.Todos
{
    public class TodoApiTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly TodoForCreationDto _todoDto;


        public TodoApiTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _todoDto = new TodoForCreationDto
            {
                Title = "Test Todo DTO",
                Description = "Test Todo",
                DueDate = DateTime.Now
            };
        }


        [Fact]
        public async void ApiApplicationGet_ShouldReturn200Status()
        {
            await using var application = new
            WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var response = await client.GetAsync("/api/todos");
            var data = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAllTodo_ShouldReturn200Status()
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            /// Act
            var result = (OkObjectResult?) (await controller.Get()).Result;
            //  Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(200);
        }

        [Theory]
        [InlineData(1, "Complete project")]
        [InlineData(2, "Call client")]
        public void GetValidTodo_ShouldReturn200Status(int targetId,string targetTitle)
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            /// Act
            var callResult = (controller.Get(targetId));
            var status = (OkObjectResult?) callResult.Result;
            /// Assert
            callResult.Should().NotBeNull();
            status.Should().NotBeNull();
            status?.StatusCode.Should().Be(200);
            callResult?.Value?.Title.Should().Be(targetTitle);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(-1)]
        public void GetInvalidTodo_ShouldReturn404Status(int targetId)
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            /// Act
            var callResult = (controller.Get(targetId));
            var status = (NotFoundResult?) callResult.Result;
            /// Assert
            callResult.Should().NotBeNull();
            callResult.Value.Should().BeNull();
            status.Should().NotBeNull();
            status?.StatusCode.Should().Be(404);
        }

        [Fact]
        public void RemoveValidTodo_ShouldReturn200Status()
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            /// Act
            var callResult = (OkResult) controller.Delete(1);
            /// Assert
            callResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public void RemoveInvalidTodo_ShouldReturn404Status()
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            /// Act
            var callResult = (NotFoundResult) controller.Delete(6);
            /// Assert
            callResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public void AddValidTodo_ShouldReturn200Status()
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            /// Act
            var callResult = controller.Post(_todoDto);
            /// Assert
            var status = (OkObjectResult?)callResult.Result;
            callResult.Should().NotBeNull();
            status.Should().NotBeNull();
            status?.StatusCode.Should().Be(200);
            callResult?.Value?.Title.Should().Be(_todoDto.Title);
        }

        [Fact]
        public void UpdateValidTodo_ShouldReturn200Status()
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            var item = _mockUow.Object.Todos.Get(1).Result;
            var itemDto = _mapper.Map<TodoDto>(item);
            /// Act
            var callResult = controller.Put(item.Id, itemDto);
            /// Assert
            var status = (OkResult) callResult;
            status?.StatusCode.Should().Be(200);
        }

        [Fact]
        public void UpdateInvalidTodo_ShouldReturn400Status()
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            var item = _mockUow.Object.Todos.Get(1).Result;
            var itemDto = _mapper.Map<TodoDto>(item);
            /// Act
            var callResult = controller.Put(10, itemDto);
            /// Assert
            var status = (BadRequestResult) callResult;
            status?.StatusCode.Should().Be(400);
        }

        [Fact]
        public void UpdateNotExitstTodo_ShouldReturn404Status()
        {
            /// Arrange
            var _logger = new Mock<ILogger<TodoController>>();
            var controller = new TodoController(_logger.Object, _mockUow.Object, _mapper);
            var notExistsTodo = new TodoDto
            {
                Id = 10,
                Title = "Test Todo DTO",
                Description = "Test Todo",
                DueDate = DateTime.Now
            };
            /// Act
            var callResult = controller.Put(10, notExistsTodo);
            /// Assert
            var status = (NotFoundResult) callResult;
            status.StatusCode.Should().Be(404);
        }

    }
}
