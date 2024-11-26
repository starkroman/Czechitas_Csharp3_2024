using ToDoList.Domain.DTOs;
using ToDoList.Frontend.Clients;
using ToDoList.Frontend.Views;

public class ToDoItemsClient : IToDoItemsClient
{
    private readonly HttpClient httpClient;
    public ToDoItemsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    // Lekce 9. = async, Task<zabalit>, await (počkej si na výsledek)
    public async Task<List<ToDoItemView>> ReadItemsAsync()
    {
        var toDoItemsView = new List<ToDoItemView>();
        var response = await httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>("api/ToDoItems");

         toDoItemsView = response.Select(dto => new ToDoItemView
        {
            ToDoItemId = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted
        }).ToList();
        return toDoItemsView;
    }

}
