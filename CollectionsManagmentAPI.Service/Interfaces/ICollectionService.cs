using CollectionsManagmentAPI.DataAccess;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Models.Collection;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface ICollectionService
{
    List<CollectionModel> GetAll();
    List<CollectionModel> GetRange(int skip, int take);
    Task<CollectionEntity> GetById(int id);
    Task Create(CollectionEntity collection);
    Task Update(CollectionEntity collection, CollectionModel updateModel);
    Task<bool> Delete(int id);
    CollectionEntity SearchByName(string name);
}