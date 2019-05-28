using Microsoft.EntityFrameworkCore;
using RESTApiTODOList.Core.Domain;

namespace RESTApiTODOList.Persistence
{
    public sealed class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
