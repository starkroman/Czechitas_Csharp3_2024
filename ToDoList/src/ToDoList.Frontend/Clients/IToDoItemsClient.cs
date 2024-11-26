namespace ToDoList.Frontend.Clients;
using ToDoList.Frontend.Views;
public interface IToDoItemsClient
{
    // doplnit Task<>
    public Task<List<ToDoItemView>> ReadItemsAsync();
}
