using System.ComponentModel.DataAnnotations;
using CollectionsManagmentAPI.Entity.Enums;

namespace CollectionsManagmentAPI.Entity;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    public int RoleId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public byte[]? PasswordHash { get; set; }
}