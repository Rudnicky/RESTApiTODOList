using Microsoft.EntityFrameworkCore;
using RESTApiTODOList.Core.Domain;
using RESTApiTODOList.Persistence;
using RESTApiTODOList.Persistence.Repositories;
using System.Linq;
using Xunit;

namespace RESTApiTODOList.Tests.Repositories
{

    /// <summary>
    /// Unit tests for the TodoRepository class
    /// Naming convention: MethodName_ExpectedBehavior_StateUnderTest
    /// </summary>
    public sealed class TodoRepositoryTests
    {
        [Fact]
        public void GetTodos_ShouldReturnTodos_WhenBunchOfTodosWereAdded()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoDb")
                .Options;

            var context = new TodoDbContext(options);
            var repository = new TodoRepository(context);

            // Act
            InitializeTodos(context);
            var result = repository.GetAll().ToList();

            // Assert
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void GetTodoItem_ShouldReturnSpecificTodoItem_AfterGivenIdOfPickedTodoItem()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoDb")
                .Options;

            var context = new TodoDbContext(options);
            var repository = new TodoRepository(context);

            // Act
            InitializeTodos(context);
            var result = repository.Get(1);

            // Assert
            Assert.Equal("Get up early", result.Name);
        }

        [Fact]
        public void AddTodoItem_ShouldReturnSingleTodoItem_WhenOneTodoItemAdded()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoDb")
                .Options;

            var context = new TodoDbContext(options);
            var repository = new TodoRepository(context);

            // Act
            repository.Add(new TodoItem() { Name = "Go to sleep! it's already super late..." });
            var result = repository.GetAll().ToList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void UpdateTodoItem_ShouldReturnDifferentTodoItemName_WhenTodoItemNameHasBeenModified()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoDb")
                .Options;

            var context = new TodoDbContext(options);
            var repository = new TodoRepository(context);

            // Act
            InitializeTodos(context);
            var result = repository.GetAll().ToList();
            var originalTodoItem = result.First();
            var originalTodoItemNameProperty = originalTodoItem.Name;

            originalTodoItem.Name = "Have a coffee";
            repository.Update(originalTodoItem);

            var updatedTodoItemName = repository.Get(1).Name;

            // Assert
            Assert.NotEqual(originalTodoItemNameProperty, updatedTodoItemName);
        }

        [Fact]
        public void DeleteTodoItem_ShouldReturnDecrementedCounter_WhenOneOfTheTodoItemsWereRemoved()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoDb")
                .Options;

            var context = new TodoDbContext(options);
            var repository = new TodoRepository(context);

            // Act
            InitializeTodos(context);
            var result = repository.GetAll().ToList();
            repository.Delete(result.First());
            var resultAfterDeletePerformed = repository.GetAll().ToList();

            // Assert
            Assert.Equal(resultAfterDeletePerformed.Count, result.Count - 1);
        }

        private void InitializeTodos(TodoDbContext context)
        {
            var todoItems = new TodoItem[]
            {
                new TodoItem() { Name = "Get up early" },
                new TodoItem() { Name = "Time for coffee & a cigarette" },
                new TodoItem() { Name = "Take a shower" },
                new TodoItem() { Name = "Have a breakfast" },
                new TodoItem() { Name = "Time to work bro" }
            };

            context.TodoItems.AddRange(todoItems);
            context.SaveChanges();
        }
    }
}
