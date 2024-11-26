
namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

// generické rozhraní
public interface IRepository<T> where T : class
{
    //void Create(ToDoItem item);  // bez generické vazby, obecná vazba
    //void Create<T>(T item);  // s generikou
        public Task Create(T item);  // zjednodušená generika, CRUD doplnit...

        public Task<IEnumerable<T>> Read();

        public Task<T?> ReadById(int id);  //nulovatelný typ

        public Task Update(T item);

        public Task DeleteById(int id);

}
