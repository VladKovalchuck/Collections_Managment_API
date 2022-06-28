using CollectionsManagmentAPI.DataAccess;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Models.Collection;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface ICollectionService
{
    List<CollectionModel> GetAll();
    List<CollectionModel> GetRange(int skip, int take);
    Task<CollectionModel> GetById(int id);
    Task<CollectionModel> Create(CollectionModel collection);
    Task Update(CollectionModel updateModel);
    Task<bool> Delete(int id);
    CollectionModel SearchByName(string name);
}