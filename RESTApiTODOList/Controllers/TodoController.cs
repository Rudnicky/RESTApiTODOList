using Microsoft.AspNetCore.Mvc;
using RESTApiTODOList.Core.Domain;
using RESTApiTODOList.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTApiTODOList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;

            CreateDummyData();
        }

        // GET: api/Todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _todoRepository.GetAll();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var todoItem = _todoRepository.Get(id);
            if (todoItem != null)
            {
                return todoItem;
            }
            return NotFound();
        }

        private void CreateDummyData()
        {
            var listOfTodos = _todoRepository.GetAll().ToList();
            if (listOfTodos != null && listOfTodos.Count == 0)
            {
                _todoRepository.Add(new Core.Domain.TodoItem() { Name = "Get up early" });
                _todoRepository.Add(new Core.Domain.TodoItem() { Name = "Have a cigarette with coffee" });
                _todoRepository.Add(new Core.Domain.TodoItem() { Name = "Take a shower" });
            }
        }
    }
}
