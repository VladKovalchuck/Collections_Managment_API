namespace CollectionsManagmentAPI.Entity;

public class UserEntity
{
    //сомневаюсь в необходимости этой сущности
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }
}