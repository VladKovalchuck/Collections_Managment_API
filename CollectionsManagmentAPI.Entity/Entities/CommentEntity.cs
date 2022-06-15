namespace CollectionsManagmentAPI.Entity;

public class CommentEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; }
    public ItemEntity Item { get; set; }
    public UserEntity User { get; set; }
}