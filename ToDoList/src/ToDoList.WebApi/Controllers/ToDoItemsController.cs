namespace ToDoList.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    //private static readonly List<ToDoItem> items = [];
    // simulace databáze
    private static List<ToDoItem> newToDoItems = new List<ToDoItem>
    {
        new ToDoItem { ToDoItemId = 1, Name = "Nakoupit", Description = "Kaufland", IsCompleted = false },
        new ToDoItem { ToDoItemId = 2, Name = "Uklidit",  Description = "Dětský pokoj", IsCompleted = true }
    };

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = newToDoItems.Count == 0 ? 1 : newToDoItems.Max(o => o.ToDoItemId) + 1;
            newToDoItems.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
        // odpověď klientovi
        return Created();  // 201

    }
    [HttpGet]
    public IActionResult Read()   // rozhraní IActionResult
    {
        return Ok(newToDoItems);
    }

    [HttpGet("{id:int}")]
    public IActionResult ReadById(int id)
    {
        var item = newToDoItems.FirstOrDefault(i => i.ToDoItemId == id);

        if(item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateById(int id, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var item = newToDoItems.FirstOrDefault(i => i.ToDoItemId == id);

        if(item == null)
            return NotFound();

        // aktualizace hodnot
        item.Name = request.Name;
        item.Description = request.Description;
        item.IsCompleted = request.IsCompleted;

        return NoContent();  //204
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteById(int id)
    {
        var item = newToDoItems.FirstOrDefault(i => i.ToDoItemId == id);

        if(item == null)
            return NotFound();

        newToDoItems.Remove(item);
        return NoContent();
    }
}
