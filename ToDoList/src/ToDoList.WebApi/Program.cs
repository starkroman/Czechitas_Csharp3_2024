using Microsoft.AspNetCore.DataProtection.Repositories;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    // Configure DI
    builder.Services.AddControllers();
    // pridal jsem si DBContext (Lekce05)
    builder.Services.AddDbContext<ToDoItemsContext>();
    // pridame pro Lekce06
    builder.Services.AddScoped<IRepository<ToDoItem>, ToDoItemsRepository>();
}
var app = builder.Build();
{
    // Configure Middleware (HTTP request pipeline)
    app.MapControllers();
}

//app.MapGet("/", () => "Hello World!");
//app.MapGet("/ahojsvete", () => "Ahoj světe!");

app.Run();
