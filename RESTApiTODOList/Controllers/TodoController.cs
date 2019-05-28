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
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            var listOfTodos = _todoRepository.GetAll().ToList();
            if (listOfTodos != null && listOfTodos.Count > 0)
            {
                return Ok(_todoRepository.GetAll());
            }
            return NotFound();
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

        // POST: api/Todo
        [HttpPost]
        public ActionResult<TodoItem> Post(TodoItem item)
        {
            if (item != null)
            {
                _todoRepository.Add(item);
                return CreatedAtAction(nameof(Post), item);
            }
            return BadRequest();
        }

        // PUT: api/Todo
        [HttpPut]
        public ActionResult Put(TodoItem item)
        {
            if (item != null)
            {
                var todoItem = _todoRepository.Get(item.Id);
                if (todoItem != null)
                {
                    todoItem.Name = item.Name;
                    _todoRepository.Update(todoItem);
                }
            }
            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todoItem = _todoRepository.Get(id);
            if (todoItem != null)
            {
                _todoRepository.Delete(todoItem);
                return NoContent();
            }
            return BadRequest();
        }

        private void CreateDummyData()
        {
            var listOfTodos = _todoRepository.GetAll().ToList();
            if (listOfTodos != null && listOfTodos.Count == 0)
            {
                _todoRepository.Add(new TodoItem() { Name = "Get up early" });
                _todoRepository.Add(new TodoItem() { Name = "Have a cigarette with coffee" });
                _todoRepository.Add(new TodoItem() { Name = "Take a shower" });
            }
        }
    }
}
