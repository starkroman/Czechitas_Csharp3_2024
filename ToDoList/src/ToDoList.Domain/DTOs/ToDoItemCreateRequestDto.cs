namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted, string? Category) //id is generated
{
    public ToDoItem GetToDomain() => new() { Name = Name, Description = Description, IsCompleted = IsCompleted, Category = Category};
}
