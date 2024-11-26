namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;

public class GetByIdTests
{
    [Fact]
    public async Task GetById_ValidId_ReturnsItem()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../data/localdb.db");
        var controller = new ToDoItemsController(context, null); // Docasny hack, nez z controlleru odstranime context.
        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        // Act
        var result =  await controller.ReadById(toDoItem.ToDoItemId);
        var resultResult = await result.Result;
        var value = await result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(toDoItem.ToDoItemId, value.Id);
        Assert.Equal(toDoItem.Description, value.Description);
        Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);
        Assert.Equal(toDoItem.Name, value.Name);
    }

    [Fact]
    public async Task GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../data/localdb.db");
        var controller = new ToDoItemsController(context, null); // Docasny hack, nez z controlleru odstranime context.

        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        // Act
        var invalidId = -1;
        var result = await controller.ReadById(invalidId);
        var resultResult = await result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
    }
}
