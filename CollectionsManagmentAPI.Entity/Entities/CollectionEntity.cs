namespace CollectionsManagmentAPI.Entity;

public class CollectionEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Topic { get; set; }
    public int UserId { get; set; }
    

    public UserEntity User { get; set; }
    public ICollection<ItemEntity> Items { get; set; }
    public ICollection<TagEntity> Tags { get; set; }
}