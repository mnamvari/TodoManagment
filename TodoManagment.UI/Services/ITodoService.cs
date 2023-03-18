using TodoManagment.UI.Models;
using TodoManagment.UI.Services.Base;

namespace TodoManagment.UI.Services
{
    public interface ITodoService
    {
        Task<List<TodoVM>> GetTodoList();
        Task<TodoVM> GetTodo(int id);
        Task<Response<int>> AddTodo(TodoForCreationVM todo);
        Task<Response<int>> UpdateTodo(int id, TodoVM todo);
        Task<Response<int>> DeleteTodo(int id);
    }
}