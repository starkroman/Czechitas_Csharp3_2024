namespace ToDoList.WebApi.Controllers;

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    //private static readonly List<ToDoItem> items = [];
    // simulace databáze
    /*
    private static readonly List<ToDoItem> newToDoItems = new List<ToDoItem>
    {
        new ToDoItem { ToDoItemId = 1, Name = "Nakoupit", Description = "Kaufland", IsCompleted = false },
        new ToDoItem { ToDoItemId = 2, Name = "Uklidit",  Description = "Dětský pokoj", IsCompleted = true }
    };
    */
    //public readonly List<ToDoItem> newToDoItems = [];   //lekce02

    private readonly ToDoItemsContext context;  // odstraníme pro mockování - Lekce06
    private readonly IRepository<ToDoItem> repository;   // doplnění Lekce06

    public ToDoItemsController()
    {
        //bezparametrický konstruktor
    }

    //přidám (Lekce05) konstruktor
    public ToDoItemsController(ToDoItemsContext context)
    {
        this.context = context;
    }

    //přidám (Lekce06) konstruktor, modifikace konstruktoru (Lekce06)
    //Lekce06
    public ToDoItemsController(IRepository<ToDoItem> repository)
    {
        this.repository = repository;
    }

    // Lekce 05
    public ToDoItemsController(ToDoItemsContext context, IRepository<ToDoItem> repository)
    {
        this.context = context;
        this.repository = repository;
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            //Lekce02
            //item.ToDoItemId = newToDoItems.Count == 0 ? 1 : newToDoItems.Max(o => o.ToDoItemId) + 1;
            //newToDoItems.Add(item);
            // Lekce05
            //context.ToDoItems.Add(item);
            //context.SaveChanges();
            // Lekce06
            repository.Create(item);   // tady to upravím...

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
        // odpověď klientovi
        //return Created();  // 201
        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item));  // 201
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()   // rozhraní IActionResult, vrátí JSON soubor...
    {
        List<ToDoItem> itemsToGet;
        try
        {
            //itemsToGet = newToDoItems;
            // Lekce05
            //itemsToGet = context.ToDoItems.ToList();
            // Lekce06
            itemsToGet = repository.Read().ToList();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
        //return Ok(newToDoItems);  //moje
        //respond to client
        return (itemsToGet is null || itemsToGet.Count is 0)
        ? NotFound()  // 404
        : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain));  //200
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        //try to retrieve the item by id
        ToDoItem? itemToGet;

        //Lekce05
        try
        {
            //itemToGet = context.ToDoItems.Find(toDoItemId);
            itemToGet = repository.ReadById(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemToGet is null)
            ? NotFound() // 404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet)); // 200

        //Lekce02
        /*
        try
        {
            itemToGet = items.Find(i => i.ToDoItemId == toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemToGet is null)
            ? NotFound() //404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet)); //200
        */
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
         //map to Domain object as soon as possible
        var updatedItem = request.ToDomain();
        updatedItem.ToDoItemId = toDoItemId;
        //Lekce05
        try
        {
            //retrieve the item
            //var itemToUpdate = context.ToDoItems.Find(toDoItemId);
            /*
            var itemToUpdate = repository.ReadById(toDoItemId);
            if (itemToUpdate is null)
            {
                return NotFound(); //404
            }
            */
            repository.UpdateById(toDoItemId, updatedItem);

            // Aktualizace hodnot (např. názvu, popisu atd.)
            //itemToUpdate.Name = updatedItem.Name;
            //itemToUpdate.Description = updatedItem.Description;
            //itemToUpdate.IsCompleted = updatedItem.IsCompleted;

            // podle expertů...
            //context.Entry(itemToUpdate).CurrentValues.SetValues(updatedItem);
            //context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
        //respond to client
        return NoContent(); //204

        //Lekce02
        /*
        //map to Domain object as soon as possible
        var updatedItem = request.ToDomain();

        //try to update the item by retrieving it with given id
        try
        {
            //retrieve the item
            var itemIndexToUpdate = items.FindIndex(i => i.ToDoItemId == toDoItemId);
            if (itemIndexToUpdate == -1)
            {
                return NotFound(); //404
            }
            updatedItem.ToDoItemId = toDoItemId;
            items[itemIndexToUpdate] = updatedItem;
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return NoContent(); //204
        */
    }


    [HttpDelete("{itoDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        //try to delete the item
        try
        {
            //var itemToDelete = context.ToDoItems.Find(toDoItemId);

            var itemToDelete = repository.ReadById(toDoItemId);

            if (itemToDelete is null)
            {
                return NotFound(); //404
            }

            //context.ToDoItems.Remove(itemToDelete);
            //context.SaveChanges();

            repository.DeleteById(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        //respond to client
        return NoContent(); //204

        //Lekce02
        /*
        try
        {
            var itemToDelete = items.Find(i => i.ToDoItemId == toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound(); //404
            }
            items.Remove(itemToDelete);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        //respond to client
        return NoContent(); //204
        */
    }
}
