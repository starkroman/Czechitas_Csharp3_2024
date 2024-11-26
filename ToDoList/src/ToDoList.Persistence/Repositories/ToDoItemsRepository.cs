// implementace rozhraní

using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

// Lekce06 - Repository
public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    // konstruktor
    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

    // implementace metod
    public async Task Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
    }

    /*
    public IEnumerable<ToDoItem> Read()
    {
        return context.ToDoItems.ToList();
    }

    public ToDoItem? ReadById(int id)
    {
        return context.ToDoItems.Find(id);
    }
    */
    // nápověda od profíků

    public async Task<IEnumerable<ToDoItem>> Read() => await context.ToDoItems.ToListAsync();

    public async Task<ToDoItem?> ReadById(int id) => await context.ToDoItems.FindAsync(id);

    /*
    public void Update( ToDoItem updateItem)
    {
        var item = context.ToDoItems.Find(updateItem.ToDoItemId);
        if (item != null)
        {
            context.Entry(item).CurrentValues.SetValues(item);
            context.SaveChanges();
        }
    }

    public void DeleteById(int id)
    {
        var item = context.ToDoItems.Find(id);
        if (item != null)
        {
            context.ToDoItems.Remove(item);
            context.SaveChanges();
        }
    }
    */
    // úprava od profíků
    public async Task Update(ToDoItem item)
    {
        var foundItem = await context.ToDoItems.FindAsync(item.ToDoItemId) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {item.ToDoItemId} not found.");
        context.Entry(foundItem).CurrentValues.SetValues(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var item = await context.ToDoItems.FindAsync(id) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {id} not found.");
        context.ToDoItems.Remove(item);
        await context.SaveChangesAsync();
    }

}
