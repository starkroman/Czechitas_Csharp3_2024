namespace ToDoList.Test;

using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class GetTest1
{
    [Fact]   // atribut = test

    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        controller.newToDoItems.Add(toDoItem);

        // Act
        var result = controller.Read();
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        var firstItem = value.First();
        Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);
    }
}

/*
public void Get_AllItems_ReturnsAllItems()
{
    // Arrange
    var controller = new ToDoItemsController();
    var newToDoItem = new ToDoItem
    {
        ToDoItemId = 1,
        Name = "Jirka",
        Description = "Úkol z Javy",
        IsCompleted = false
    };

    ToDoItemsController.newToDoItems.Add(newToDoItem);

    // Act
    var result = controller.Read();


    // Assert
    var resultOKObjectResult = Assert.IsType<OkObjectResult>(result.Result);

    var returnItems = (resultOKObjectResult.Value as IEnumerable<ToDoItemGetResponseDto>).ToList();

    Assert.Single(returnItems);
    Assert.Equal(newToDoItem.Name, returnItems[0].Name);
    Assert.Equal(newToDoItem.Description, returnItems[0].Description);
    Assert.Equal(newToDoItem.IsCompleted, returnItems[0].IsCompleted);
}
*/







