namespace ToDoList.Persistence;

using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

public class ToDoItemsContext: DbContext
{
    //datová položka
    private readonly string connectionString;
    //konstruktor
    public ToDoItemsContext(string connectionString = "Data Source=../../data/localdb.db")
    {
        this.connectionString = connectionString;
        this.Database.Migrate();  //aplikuj všechny migrace (Lekce05)
    }

    // Vlastnost  - vytvoř databázi pomocí ToDoItem
    public DbSet<ToDoItem> ToDoItems { get; set; }

    // přetížení metody OnConfiguring, abych dostal připojení na SQLite
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString);
    }
}
