//https://github.com/czechitas/csharp3-template/blob/Lesson-03/ToDoList/src/ToDoList.Domain/DTOs/ToDoItemUpdateRequestDto.cs

namespace ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

// record = struktura
public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted) //id is generated
{
    public ToDoItem ToDomain() => new() { Name = Name, Description = Description, IsCompleted = IsCompleted };
}
