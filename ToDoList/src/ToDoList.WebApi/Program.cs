using ToDoList.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    // Configure DI
    builder.Services.AddControllers();
    // pridal jsem si DBContext (Lekce05)
    builder.Services.AddDbContext<ToDoItemsContext>();
}
var app = builder.Build();
{
    // Configure Middleware (HTTP request pipeline)
    app.MapControllers();
}

//app.MapGet("/", () => "Hello World!");
//app.MapGet("/ahojsvete", () => "Ahoj světe!");

app.Run();
