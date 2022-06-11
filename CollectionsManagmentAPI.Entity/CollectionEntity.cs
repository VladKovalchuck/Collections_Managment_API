namespace CollectionsManagmentAPI.Entity;

public class CollectionEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Topic { get; set; }

    public List<ItemEntity> Items { get; set; }
}