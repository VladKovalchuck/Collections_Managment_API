namespace CollectionsManagmentAPI.Entity;

public class CollectionEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Topic { get; set; }
    
    //мой гениальный вариант выбора дополнительных полей для айтемов
    public int? IntName1 { get; set; }
    public int? IntName2 { get; set; }
    public int? IntName3 { get; set; }
    public string? StringName1 { get; set; }
    public string? StringName2 { get; set; }
    public string? StringName3 { get; set; }
    
    public List<ItemEntity> Items { get; set; }
}