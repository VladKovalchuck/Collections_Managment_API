using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.DataAccess.Interfaces;

interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
}