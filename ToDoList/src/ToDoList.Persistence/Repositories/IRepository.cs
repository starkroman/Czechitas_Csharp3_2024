
namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

// generické rozhraní
public interface IRepository<T> where T : class
{
    //void Create(ToDoItem item);  // bez generické vazby, obecná vazba
    //void Create<T>(T item);  // s generikou
        public void Create(T item);  // zjednodušená generika, CRUD doplnit...

        public IEnumerable<T> Read();

        public T? ReadById(int id);  //nulovatelný typ

        public void Update(T item);

        public void DeleteById(int id);

}
