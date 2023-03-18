using AutoMapper;
using TodoManagment.UI.Models;
using TodoManagment.UI.Services.Base;

namespace TodoManagment.UI.Services
{
    public class TodoService : ITodoService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;

        public TodoService(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Response<int>> AddTodo(TodoForCreationVM todo)
        {
            try
            {
                var response = new Response<int>();
                TodoForCreationDto dto = _mapper.Map<TodoForCreationDto>(todo);
                var apiResponse =  await _client.TodosPOSTAsync(dto);
                return new Response<int>() { Sucsess = true, Data= apiResponse.Id };
            }
            catch (Exception)
            {

                return new Response<int>() { Sucsess = false, Message = "Error on create!" };
            }
        }

        public async Task<Response<int>> DeleteTodo(int id)
        {
            try
            {
                await _client.TodosDELETEAsync(id);
                return new Response<int>() { Sucsess = true };
            }
            catch (Exception ex)
            {

                return new Response<int>() { Sucsess = false, Message = "Error on delete!" };
            }
        }

        public async Task<TodoVM> GetTodo(int id)
        {
            var todo = await _client.GetTodoAsync(id);
            return _mapper.Map<TodoVM>(todo);
        }

        public async Task<List<TodoVM>> GetTodoList()
        {
            var todos = await _client.TodosAllAsync();
            return _mapper.Map<List<TodoVM>>(todos);
        }

        public async Task<Response<int>> UpdateTodo(int id, TodoVM todo)
        {
            try
            {
                TodoDto dto = _mapper.Map<TodoDto>(todo);
                await _client.TodosPUTAsync(id, dto);
                return new Response<int>() { Sucsess = true };
            }
            catch (Exception ex)
            {

                return new Response<int>() { Sucsess = false, Message = "Error on update!" };
            }

        }
    }
}
