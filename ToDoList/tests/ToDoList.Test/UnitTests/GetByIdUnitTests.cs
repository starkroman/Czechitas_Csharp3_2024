using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace ToDoList.Test.UnitTests
{
    public class GetByIdUnitTests
    {
         [Fact]
        public async Task Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var someId = 1;
            repositoryMock.ReadById(someId).Returns(new ToDoItem { Name = "testItem", Description = "testDescription", IsCompleted = false, Category = "testCategory" });

            // Act
            var result = await controller.ReadById(someId);
            var resultResult = result.ExecuteResultAsync;

            // Assert
            Assert.IsType<OkObjectResult>(resultResult);
            repositoryMock.Received(1).ReadById(someId);
        }

        [Fact]
        public async Task Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var someId = 1;
            repositoryMock.ReadById(someId).ReturnsNull();

            // Act
            var result = await controller.ReadById(someId);
            var resultResult = result.ExecuteResultAsync;

            // Assert
            Assert.IsType<NotFoundResult>(resultResult);
            repositoryMock.Received(1).ReadById(someId);
        }

    [Fact]
        public async Task Get_ReadByIdUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var someId = 1;

            repositoryMock.ReadById(someId).Throws(new Exception());

            // Act
            var result = await controller.ReadById(someId);
            var resultResult = result.ExecuteResultAsync;
            // Assert
            Assert.IsType<ObjectResult>(resultResult);
            repositoryMock.Received(1).ReadById(someId);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
        }
    }
}
