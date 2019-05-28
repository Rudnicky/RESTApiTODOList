using RESTApiTODOList.Core.Domain;
using RESTApiTODOList.Core.Interfaces;

namespace RESTApiTODOList.Persistence.Repositories
{
    public sealed class TodoRepository : Repository<TodoItem>, ITodoRepository
    {
        public TodoRepository(TodoDbContext context) : base(context)
        {
        }
    }
}
