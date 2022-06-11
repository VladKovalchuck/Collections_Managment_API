using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.DataAccess.Interfaces;

interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
}