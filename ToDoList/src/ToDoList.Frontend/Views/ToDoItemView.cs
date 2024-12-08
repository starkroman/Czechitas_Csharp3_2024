namespace ToDoList.Frontend.Views;

using System.ComponentModel.DataAnnotations;

public class ToDoItemView
{
    /*
    public int ToDoItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string? Category { get; set;}
    */


    public int ToDoItemId { get; set; }

    [Required(ErrorMessage = "Name is mandatory.")]
    [Length(3, 50)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Description is mandatory.")]
    [StringLength(250)]
    public required string Description { get; set; }

    public bool IsCompleted { get; set; }

    [Required(ErrorMessage = "Catefory is mandatory.")]
    [StringLength(350)]
    public required string? Category { get; set; }
}
