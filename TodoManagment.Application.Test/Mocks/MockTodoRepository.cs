using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoManagement.Appliaction.Contracts.Persistence;
using TodoManagement.Domains;

namespace TodoManagment.Application.Test.Mocks
{
    public static class MockTodoRepository
    {
        public static Mock<ITodoRepository> GetTodoRepository() 
        {
            var todoList = new List<Todo>()
            {
                 new Todo
                {
                    Id = 1,
                    Title = "Complete project",
                    Description = "Finish project report and presentation",
                    DueDate = DateTime.Now,
                },
                new Todo
                {
                    Id = 2,
                    Title = "Call client",
                    Description = "Follow up with client on project status",
                    DueDate = DateTime.Now,
                },
                new Todo
                {
                    Id = 3,
                    Title = "Prepare for meeting",
                    Description = "Research and prepare for project meeting",
                    DueDate = DateTime.Now,
                }
            };
            
            var mockRepo = new Mock<ITodoRepository>();

            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(todoList);

            mockRepo.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => { return todoList.Find(x => x.Id == id); });
            
            mockRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns((int id) => { return todoList.Any(x => x.Id == id); });

            mockRepo.Setup(x => x.Add(It.IsAny<Todo>())).ReturnsAsync((Todo todo) => { todoList.Add(todo); return todo; });
            
            return mockRepo; 
        }
    }
}
