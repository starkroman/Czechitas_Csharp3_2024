
namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;


public interface IRepository<T> where T : class
{
    //void Create(ToDoItem item);  // bez generické vazby, obecná vazba
    //void Create<T>(T item);  // s generikou
        public void Create(T item);  // zjednodušená generika, CRUD doplnit...

        public void Read(T item);

        public void Update(T item);

        public void Delete(T item);

}
