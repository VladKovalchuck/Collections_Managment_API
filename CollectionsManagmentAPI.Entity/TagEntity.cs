namespace CollectionsManagmentAPI.Entity;

public class TagEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ItemEntity Item { get; set; }
    public CollectionEntity Collection { get; set; }
}