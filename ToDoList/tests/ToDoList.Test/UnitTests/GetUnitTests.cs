namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetUnitTests
{
    [Fact]
    public async Task Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.Read().Returns([new ToDoItem { Name = "testItem", Description = "testDescription", IsCompleted = false }]);

        // Act
        var result = await controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        repositoryMock.Received(1).Read();
    }

    [Fact]
    public async Task Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.Read().ReturnsNull();

        // Act
        var result = await controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
        repositoryMock.Received(1).Read();
    }

    [Fact]
    public async Task Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        //repositoryMock.When(r => r.ReadAll()).Do(r => throw new Exception());
        repositoryMock.Read().Throws(new Exception());

        // Act
        var result = await controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        repositoryMock.Received(1).Read();
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }
}
