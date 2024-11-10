// implementace rozhraní

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
    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
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
    public IEnumerable<ToDoItem> Read() => context.ToDoItems.ToList();
    public ToDoItem? ReadById(int id) => context.ToDoItems.Find(id);

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
    public void Update(ToDoItem item)
    {
        var foundItem = context.ToDoItems.Find(item.ToDoItemId) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {item.ToDoItemId} not found.");
        context.Entry(foundItem).CurrentValues.SetValues(item);
        context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var item = context.ToDoItems.Find(id) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {id} not found.");
        context.ToDoItems.Remove(item);
        context.SaveChanges();
    }
    
}
