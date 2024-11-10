namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;

public class PostUnitTests
{
    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        //var context = new ToDoItemsContext("Data Source=../../data/localdb.db");
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();  // nová věc...
        var controller = new ToDoItemsController(repositoryMock);   // context nahrazen repository

        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);
    }

    [Fact]
    public void Post_Exception_Returns500InternalServerError()
    {
        // Arrange
        //var context = new ToDoItemsContext("Data Source=../../data/localdb.db");
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();  // nový věci...
        // dočasný hack, nez z controlleru odstranime context
        var controller = new ToDoItemsController(repositoryMock);   // context nahrazen repository

        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );
        // chování Mocku - když vytvoříme pomocí Create jakýkoliv argument - vyhodí to vždy exception
        repositoryMock.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception());
        // pro Read
        //repositoryMock.Read(Arg.Any<ToDoItem>()).Returns(r => new NotFoundObjectResult);


        // Act
        var result = controller.Create(request);
        var resultResult = result.Result;
        //var value = result.GetValue();

        // Assert
        /*
        Assert.IsType<ObjectResult>(resultResult);  // aby test prošel
        Assert.Equivalent(new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError), resultResult);
        */

        Assert.IsType<ObjectResult>(resultResult); // Expecting 500 Internal Server Error
        var objectResult = result.Result as ObjectResult;
        Assert.Equal(500, objectResult.StatusCode);
    }
}
