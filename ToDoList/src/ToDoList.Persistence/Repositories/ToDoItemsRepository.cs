// implementace

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
    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }

    public void Delete(ToDoItem item) => throw new NotImplementedException();
    public void Read(ToDoItem item) => throw new NotImplementedException();
    public void Update(ToDoItem item) => throw new NotImplementedException();
}
