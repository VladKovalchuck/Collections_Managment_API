using CollectionsManagmentAPI.DataAccess;
using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface ICollectionService
{
    IQueryable<CollectionEntity> GetAll();
    IQueryable<CollectionEntity> GetRange(int skip, int take);
    Task<CollectionEntity> GetById(int id);
    Task Create(CollectionEntity collection);
    Task Update(CollectionEntity collection);
    Task<bool> Delete(int id);
    CollectionEntity SearchByName(string name);
}