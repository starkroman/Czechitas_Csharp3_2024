namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;


public class GetTests
{
    [Fact]
    public async Task Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../data/localdb.db");
        var controller = new ToDoItemsController(context, null);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false,
            Category = "Osoba"
        };

        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        // Act
        var result = await controller.Read();
        var resultResult = await result.Result;
        var value = await result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        var firstItem = value.First();
        Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);
        Assert.Equal(toDoItem.Category, firstItem.Category);
    }

    [Fact]
    public async Task Get_NoItems_ReturnsNotFound()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../data/localdb.db");
        var controller = new ToDoItemsController(context);

        // Act
        var result = await controller.Read();
        var resultResult = await result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
    }
}
