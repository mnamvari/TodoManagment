using Moq;
using TodoManagement.Appliaction.Contracts.Persistence;

namespace TodoManagment.Application.Test.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockTodoRepo = MockTodoRepository.GetTodoRepository();

            mockUow.Setup(r => r.Todos).Returns(mockTodoRepo.Object);
            mockUow.Setup(r => r.SaveChanges()).Returns(1);

            return mockUow;
        }
    }
}
