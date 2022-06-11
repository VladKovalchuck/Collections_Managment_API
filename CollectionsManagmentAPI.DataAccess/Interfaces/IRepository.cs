using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.DataAccess.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IQueryable<T>> GetAll();
    Task<T> GetById(int id);
    Task Create(T item);
    Task Update(T item);
    Task Delete(int id);
}