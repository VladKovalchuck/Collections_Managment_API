namespace CollectionsManagmentAPI.Entity;

public class UserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    //public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[]? PasswordHash { get; set; }

    public ICollection<CommentEntity> Comments { get; set; }
}