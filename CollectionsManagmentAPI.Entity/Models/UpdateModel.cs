namespace CollectionsManagmentAPI.Entity;

public class UpdateModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}