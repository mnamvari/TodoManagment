using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoManagement.Appliaction.Contracts.Persistence;
using TodoManagement.Appliaction.DTOs;
using TodoManagement.Domains;

namespace TodoManagment.API.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TodoController(ILogger<TodoController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/<TodoController>
        [HttpGet]
        public async Task<ActionResult<List<TodoDto>>> Get()
        {
            var todos = await _unitOfWork.Todos.GetAll();
            var model = _mapper.Map<List<Todo>, List<TodoDto>>(todos.ToList());
            return Ok(model);
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoDto> Get(int id)
        {
            var todo = _unitOfWork.Todos.Get(id).Result;
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TodoDto>(todo));
        }

        // POST api/<TodoController>
        [HttpPost]
        public ActionResult<TodoDto> Post(TodoForCreationDto todo)
        {
            var todoEntity = _mapper.Map<Todo>(todo);
            _unitOfWork.Todos.Add(todoEntity);
            _unitOfWork.SaveChanges();

            var todoToReturn = _mapper.Map<TodoDto>(todoEntity);
            return Ok(todoToReturn);
        }

        // PUT api/<TodoController>/4
        [HttpPut("{id}")]
        public ActionResult Put(int id, TodoDto todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            if (!_unitOfWork.Todos.Exists(id))
            {
                return NotFound();
            }

            var todoEntity = _mapper.Map<Todo>(todo);
            _unitOfWork.Todos.Update(todoEntity);
            _unitOfWork.SaveChanges();

            return Ok();
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todoFromRepo = _unitOfWork.Todos.Get(id).Result;
            if (todoFromRepo == null)
            {
                return NotFound();
            }
            
            _unitOfWork.Todos.Delete(todoFromRepo);
            _unitOfWork.SaveChanges();

            return Ok();

        }
    }
}
