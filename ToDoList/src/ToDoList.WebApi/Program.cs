using Microsoft.AspNetCore.DataProtection.Repositories;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    // Configure DI Services
    // WebApi services
    builder.Services.AddControllers();  // přidal ToDoItemsController
    builder.Services.AddSwaggerGen();

    // pridal jsem si DBContext (Lekce05)
    // Persistence services
    builder.Services.AddDbContext<ToDoItemsContext>();  // přidal ToDoItemsContext
    // pridame pro Lekce06
    builder.Services.AddScoped<IRepository<ToDoItem>, ToDoItemsRepository>();
    // přidal ToDoItemsRepository
}
var app = builder.Build();
{
    // Configure Middleware (HTTP request pipeline)
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API V1"));
}

//app.MapGet("/", () => "Hello World!");
//app.MapGet("/ahojsvete", () => "Ahoj světe!");

app.Run();
