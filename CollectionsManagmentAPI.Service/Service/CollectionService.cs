using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.DataAccess.Repositories;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.VisualBasic;

namespace CollectionsManagmentAPI.Service.Service;

public class CollectionService : ICollectionService
{
    private readonly IRepository<CollectionEntity> _collectionRepository;

    public CollectionService(IRepository<CollectionEntity> collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public IQueryable<CollectionEntity> GetAll()
    {
        return _collectionRepository.GetAll();
    }

    public IQueryable<CollectionEntity> GetRange(int skip, int take)
    {
        return _collectionRepository.GetAll().OrderBy(c => c.Id).Skip(skip).Take(take);
    }

    public async Task<CollectionEntity> GetById(int id)
    {
        return await _collectionRepository.GetById(id);
    }

    public async Task Create(CollectionEntity collection)
    {
        await _collectionRepository.Create(collection);
    }

    public async Task Update(CollectionEntity collection)
    {
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