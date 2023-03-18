using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoManagment.UI.Models;
using TodoManagment.UI.Services;

namespace TodoManagment.UI.Controllers
{
    public class TodoListController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoListController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: TodoListController
        public async Task<ActionResult> Index()
        {
            var model = await _todoService.GetTodoList();
            return View(model);
        }

        // GET: TodoListController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _todoService.GetTodo(id);
            return View(model);
        }

        // GET: TodoListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoForCreationVM todo)
        {
            try
            {
                var response = await _todoService.AddTodo(todo);
                if(response.Sucsess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.Message);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        // GET: TodoListController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _todoService.GetTodo(id);
            return View(model);
        }

        // POST: TodoListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TodoVM todo)
        {
            try
            {
                var response = await _todoService.UpdateTodo(id, todo);
                if (response.Sucsess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _todoService.DeleteTodo(id);
                if (response.Sucsess)
                {
                    return RedirectToAction(nameof(Index));
                }    
                ModelState.AddModelError("", response.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return BadRequest();
        }
    }
}
