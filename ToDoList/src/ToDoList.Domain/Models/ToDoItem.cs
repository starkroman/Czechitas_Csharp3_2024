namespace ToDoList.Domain.Models;

using System.ComponentModel.DataAnnotations;

public class ToDoItem
{
    [Key]   // klíč
    public int ToDoItemId { get; set; }
    [Length(1,50)]  // pro jmeno=Name
    public string Name { get; set; }
    [StringLength(250)]  // popisek 
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
