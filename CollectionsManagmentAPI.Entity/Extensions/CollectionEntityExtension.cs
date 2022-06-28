using CollectionsManagmentAPI.Entity.Models.Collection;

namespace CollectionsManagmentAPI.Entity.Extensions;

public static class CollectionEntityExtension
{
    public static CollectionModel ConvertToCollectionModel(this CollectionEntity collectionEntity)
    {
        return new CollectionModel()
        {
            Id = collectionEntity.Id,
            Name = collectionEntity.Name,
            Description = collectionEntity.Description,
            Topic = collectionEntity.Topic
        };
    }
}