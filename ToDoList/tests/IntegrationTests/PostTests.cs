namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;


public class PostTests
{
    [Fact]
    public async Task Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../data/localdb.db");
        var controller = new ToDoItemsController(context);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = await controller.Create(request);
        var resultResult = await result.Result;
        var value = await result.GetValue();

        // Assert
        Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);
    }
}
