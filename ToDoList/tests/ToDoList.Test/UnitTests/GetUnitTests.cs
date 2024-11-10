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
    public void Get_ReadAllAndSomeItemIsAvailable_ReturnOK()
    {
        //Arrnge
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        // nastavení repozitáře
        // repositoryMock.When().Do(); //genericke kdyz tak
        // repositoryMock.ReadAll().Returns(); // nastavujeme return value
        // repositoryMock.ReadAll().ReturnsNull(); // nastavujeme return value na null
        // repositoryMock.ReadAll().Throws(); // vyhazujeme výjimku
        // repositoryMock.Received().ReadAll(); // kontrolujeme zavolání metody
        /*
        repositoryMock.Read().Returns(
            [
                new ToDoItem{
                Name = "testName",
                Description = "testDescription",
                IsCompleted = false
                }
            ]);
        */
        var toDoItems = new List<ToDoItem>
        {
            new ToDoItem { ToDoItemId = 1, Name = "Item_1", Description = "Description_1", IsCompleted = false },
            new ToDoItem { ToDoItemId = 2, Name = "Item_2", Description = "Description_2", IsCompleted = true }
        };

        //Mock the Read method to return the list of items
        repositoryMock.Read().Returns(toDoItems);

        //Act
        var result = controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        repositoryMock.Received(1).Read();
    }

    [Fact]
    public void Get_ReadAllNoItemAvailable_ReturnNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        //repositoryMock.Read().ReturnsNull();
        repositoryMock.Read().Returns(null as IEnumerable<ToDoItem>);
        // Act
        var result = controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
        repositoryMock.Received(1).Read();
    }

    [Fact]
    public void Get_ReadAllExceptionOccured_ReturnInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.Read().Throws(new Exception());

        // Act
        var result = controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        repositoryMock.Received(1).Read();
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }
}
