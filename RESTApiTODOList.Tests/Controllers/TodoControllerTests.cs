using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTApiTODOList.Controllers;
using RESTApiTODOList.Core.Domain;
using RESTApiTODOList.Core.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace RESTApiTODOList.Tests.Controllers
{
    /// <summary>
    /// Unit tests for the TodoController class
    /// Naming convention: MethodName_ExpectedBehavior_StateUnderTest
    /// </summary>
    public sealed class TodoControllerTests
    {
        [Fact]
        public void GetAll_ShouldReturnOkHttpStatusCodeWithItems_InitializeCollection()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(GetAllTodoItems());
            var controller = new TodoController(mockRepo.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<TodoItem>>>(result);
        }

        [Fact]
        public void GetAll_ShouldReturnNotFoundHttpStatusCodeWithItems_CollectionEqualsNull()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Get_ShouldReturnSpecificItem_InitializeCollection()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Get_ShouldReturnNotFound_GivenIdDoesNotExists()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Post_ShouldAddTodoItemToCollection_CreateTodoItem()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Post_ShouldReturnBadRequestHttpStatusCode_CreateWrongTodoItem()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Put_ShouldReturnOkHttpStatusCodeWithUdpatedItem_EditExistingTodoItemName()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Put_ShouldReturnNoContentHttpStatusCodeWithUdpatedItem_CreateItemWithWrongId()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Delete_ShouldReturnNoContentHttpStatusCodeWithDeletedItem_GivenCorrectId()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Delete_ShouldReturnBadRequestHttpStatusCodeWithDeletedItem_GivenWrongId()
        {
            // Arrange

            // Act

            // Assert
        }

        private List<TodoItem> GetAllTodoItems()
        {
            var todoItems = new List<TodoItem>()
            {
                new TodoItem() { Name = "Get up early" },
                new TodoItem() { Name = "Time for coffee & a cigarette" },
                new TodoItem() { Name = "Take a shower" },
                new TodoItem() { Name = "Have a breakfast" },
                new TodoItem() { Name = "Time to work bro" }
            };
            return todoItems;
        }
    }
}
