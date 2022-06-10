namespace CollectionsManagmentAPI.Entity;

public class ItemEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Likes { get; set; }
    
    //мой гениальный вариант выбора дополнительных полей для айтемов
    public int? Int1 { get; set; }
    public int? Int2 { get; set; }
    public int? Int3 { get; set; }
    public string? String1 { get; set; }
    public string? String2 { get; set; }
    public string? String3 { get; set; }
    
    //связь многие ко многим между тегами и айтемами
    public List<TagEntity> Tags { get; set; }
    public CollectionEntity? Collection { get; set; }
    public List<CommentEntity> Comments { get; set; }
}