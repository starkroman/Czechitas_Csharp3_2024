namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class DeleteUnitTests
{
    [Fact]

    public void Delete_ValidItems_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).Returns(new ToDoItem());

        int testId = 1;

        // Act
        var result = controller.DeleteById(testId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(1).DeleteById(testId);
    }

     [Fact]
    public void Delete_ItemDoesNotExist_Returns404()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        // Mock the ReadById method to return null for a non-existent ID
        repositoryMock.ReadById(999).Returns((ToDoItem)null); // Simulate that item with ID 999 does not exist

        // Act
        var result = controller.DeleteById(999); // Non-existent ID

        // Assert
        Assert.IsType<NotFoundResult>(result); // Expecting 404 Not Found
    }
}
