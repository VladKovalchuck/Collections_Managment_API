namespace CollectionsManagmentAPI.Entity;

public class TagEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<ItemEntity> Items { get; set; }
}