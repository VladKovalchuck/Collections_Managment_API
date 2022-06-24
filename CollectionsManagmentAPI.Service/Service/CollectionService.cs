using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.DataAccess.Repositories;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Extensions;
using CollectionsManagmentAPI.Entity.Models.Collection;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.VisualBasic;

namespace CollectionsManagmentAPI.Service.Service;

public class CollectionService : ICollectionService
{
    private readonly IRepository<CollectionEntity> _collectionRepository;

    public CollectionService(IRepository<CollectionEntity> collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public List<CollectionModel> GetAll()
    {
        var collections = _collectionRepository.GetAll();
        
        List<CollectionModel> resultCollections = new List<CollectionModel>();
        foreach (var collection in collections)
        {
            resultCollections.Add(collection.ConvertToCollectionModel());
        }
        return resultCollections;
    }

    public List<CollectionModel> GetRange(int skip, int take)
    {
        var collections = _collectionRepository.GetAll().OrderBy(c => c.Id).Skip(skip).Take(take);
        
        List<CollectionModel> resultCollections = new List<CollectionModel>();
        foreach (var collection in collections)
        {
            resultCollections.Add(collection.ConvertToCollectionModel());
        }
        return resultCollections;
    }

    public async Task<CollectionEntity> GetById(int id)
    {
        return await _collectionRepository.GetById(id);
    }

    public async Task Create(CollectionEntity collection)
    {
        await _collectionRepository.Create(collection);
    }

    public async Task Update(CollectionEntity collection, CollectionModel updateModel)
    {
        collection.Id = updateModel.Id;
        collection.Name = updateModel.Name;
        collection.Description = updateModel.Description;
        collection.Topic = updateModel.Topic;
        
        await _collectionRepository.Update(collection);
    }

    public async Task<bool> Delete(int id)
    {
        return await _collectionRepository.Delete(id);
    }
    public CollectionEntity SearchByName(string name)
    {
        return _collectionRepository.GetAll().FirstOrDefault(c => c.Name == name);
    }
}