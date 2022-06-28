using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.DataAccess.Repositories;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Enums;
using CollectionsManagmentAPI.Entity.Extensions;
using CollectionsManagmentAPI.Entity.Models.Collection;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;


namespace CollectionsManagmentAPI.Service.Service;

public class CollectionService : ICollectionService
{
    private readonly IRepository<CollectionEntity> _collectionRepository;
    private readonly ITokenService _tokenService;

    public CollectionService(IRepository<CollectionEntity> collectionRepository, ITokenService tokenService)
    {
        _collectionRepository = collectionRepository;
        _tokenService = tokenService;
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

    public async Task<CollectionModel> GetById(int id)
    {
        return (await _collectionRepository.GetById(id)).ConvertToCollectionModel();
    }

    public async Task<CollectionModel> Create(CollectionModel createModel)
    {
        var user = await _tokenService.GetUserFromToken();
        var collection = new CollectionEntity()
        {
            Name = createModel.Name,
            Description = createModel.Description,
            Topic = createModel.Topic,
            UserId = user.Id
        };
        await _collectionRepository.Create(collection);
        return collection.ConvertToCollectionModel();
    }

    public async Task Update(CollectionModel updateModel)
    {
        var collection = await _collectionRepository.GetById(updateModel.Id);
        
        collection.Name = updateModel.Name;
        collection.Description = updateModel.Description;
        collection.Topic = updateModel.Topic;
        
        await _collectionRepository.Update(collection);
    }

    public async Task<bool> Delete(int id)
    {
        return await _collectionRepository.Delete(id);
    }
    public CollectionModel SearchByName(string name)
    {
        return _collectionRepository.GetAll().FirstOrDefault(c => c.Name == name).ConvertToCollectionModel();
    }
}