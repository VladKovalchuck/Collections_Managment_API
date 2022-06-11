namespace CollectionsManagmentAPI.Entity;

public class ItemEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Likes { get; set; }
    
    public List<TagEntity> Tags { get; set; }
    public CollectionEntity? Collection { get; set; }
    public List<CommentEntity> Comments { get; set; }
}