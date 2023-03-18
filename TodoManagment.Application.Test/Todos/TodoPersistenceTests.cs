using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoManagement.Domains;
using TodoManagement.Persistence;
using TodoManagement.Persistence.Repositories;
using Xunit;

namespace TodoManagment.Application.Test.Todos
{
    public class TodoPersistenceTests
    {
        private DbContextOptions<TodoManagementDbContext> options;

        public TodoPersistenceTests()
        {
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            options = new DbContextOptionsBuilder<TodoManagementDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new TodoManagementDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.SaveChanges();
            }
        }
        
        [Fact]
        public async Task GetTodos_ReturnsThreeTodos()
        {
            // Arrange
            using (var context = new TodoManagementDbContext(options))
            {
                var todoRepository = new TodoRepository(context);

                // Act
                var todos = await todoRepository.GetAll();

                // Assert
                todos.Count().Should().Be(3);
            }
        }

        [Fact]
        public async Task GetValidTodoById_ReturnsTodo()
        {
            // Arrange
            using (var context = new TodoManagementDbContext(options))
            {
                var todoRepository = new TodoRepository(context);

                // Act
                var todo = await todoRepository.Get(1);

                // Assert
                todo.Should().NotBeNull();
                todo.Id.Should().Be(1);
            }
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(20, false)]
        public void ExistsValidTodoById_ReturnsTrue(int id, bool isExists)
        {
            // Arrange
            using (var context = new TodoManagementDbContext(options))
            {
                var todoRepository = new TodoRepository(context);

                // Act
                var result = todoRepository.Exists(id);

                // Assert
                result.Should().Be(isExists);
            }
        }

        [Fact]
        public async Task DeleteValidTodo_Passes()
        {
            // Arrange
            using (var context = new TodoManagementDbContext(options))
            {
                var todoRepository = new TodoRepository(context);
                var todo = await todoRepository.Get(1);
                // Act
                Action act = () => todoRepository.Delete(todo);
                // Assert
                act.Should().NotThrow();
            }
        }

        [Fact]
        public async Task AddValidTodo_Passes()
        {
            // Arrange
            using (var context = new TodoManagementDbContext(options))
            {
                var todoRepository = new TodoRepository(context);
                var todo = new Todo 
                {
                    Title = "Complete project",
                    Description = "Finish project report and presentation",
                    DueDate = DateTime.Now,
                };
                // Act
                var result = await todoRepository.Add(todo);
                // Assert
                result.Should().NotBeNull();
                ;
            }
        }

        [Fact]
        public async Task UpdateValidTodo_Passes()
        {
            // Arrange
            using (var context = new TodoManagementDbContext(options))
            {
                var todoRepository = new TodoRepository(context);
                var todo = await todoRepository.Get(1);
                todo.Title = "Cahnged";
                // Act
                Action act = () => todoRepository.Update(todo);
                // Assert
                act.Should().NotThrow();
                ;
            }
        }
        /*
        [Fact]
        public async void CreateTodo_ReturnsSuccess()
        {
            TodoController(ILogger < TodoController > logger, IUnitOfWork unitOfWork, IMapper mapper)
            var todoEntity = _mapper.Map<TodoForCreationDto>(_todoDto);
            _mockUow.Object.Todos.Add(todoEntity);
            _unitOfWork.SaveChanges();

            var todoToReturn = _mapper.Map<TodoDto>(todoEntity);
            return Ok(todoToReturn);

            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            leaveTypes.Count.ShouldBe(4);

            //Arange
            var connection = new SqliteConnection("Data Source=:memory:");

            var options = new DbContextOptionsBuilder<Mc2Context>()
                .UseSqlite(connection)
                .Options;
            using (var context = new Mc2Context(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Customers.Add(new Customer()
                {
                    Id = Guid.Parse("A8C1D49C-6E12-4C71-92DF-D0452FFCE464"),
                    Firstname = "John",
                    Lastname = "Doe",
                    DateOfBirth = DateTime.Now.AddYears(-30),
                    Email = "john@gmail.com",
                    PhoneNumber = 989111003311,
                    BankAccountNumber = "xyx-012545-079"
                });
                context.SaveChanges();
            }

            using (var context = new Mc2Context(options))
            {
                var repository = new Mc2Repository(context);
                var mapper = GetMapper();
                var customerToadd = new CustomerForManipulationDto()
                {
                    Firstname = "Mohammad",
                    Lastname = "Namvari",
                    DateOfBirth = DateTime.Now.AddYears(-40),
                    Email = "namvari@gmail.com",
                    PhoneNumber = "+989144055368",
                    BankAccountNumber = "012545154"
                };
                var command = new AddCustomerCommand() { CustomerForManipulationDto = customerToadd };
                var handler = new AddCustomerCommandHandler(repository, mapper);

                //Act
                var result = await handler.Handle(command, new System.Threading.CancellationToken());

                //Asert
                Assert.NotNull(result);
                Assert.NotEqual(Guid.Empty, result.Id);
                Assert.Equal("+989144055368", result.PhoneNumber);
                Assert.Equal("Namvari", result.Lastname);
            }
        }
   */

    }
}
