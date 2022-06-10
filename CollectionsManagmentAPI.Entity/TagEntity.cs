namespace CollectionsManagmentAPI.Entity;

public class TagEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    //связь многие ко многим между тегами и айтемами
    public List<ItemEntity> Items { get; set; }
}