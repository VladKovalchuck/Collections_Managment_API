namespace CollectionsManagmentAPI.Entity.Extensions;

public static class UserEntityExtension
{
    public static UserModel ConvertToUserModel(this UserEntity user)
    {
        return new UserModel()
        {
            Id = user.Id,
            Username = user.Username,
            EmailAddress = user.EmailAddress,
            Role = user.Role,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash,
            IsBlocked = user.IsBlocked
        };
    }
}