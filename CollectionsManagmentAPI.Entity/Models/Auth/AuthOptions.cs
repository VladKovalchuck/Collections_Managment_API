using Microsoft.IdentityModel.Tokens;

namespace CollectionsManagmentAPI.Entity;

public class AuthOptions
{
    public const string Issuer = "CollectionsManagmentAPIServer";
    public const string Audience = "CollectionsManagmentAPIClient";
    public const string Token = "RJs3QwIyISQRFT9Atq3Q9NjNRENLMoxM";
    public const int Expires = 1;
    
    private static SymmetricSecurityKey key = new SymmetricSecurityKey(
        System.Text.Encoding.UTF8.GetBytes(AuthOptions.Token));

    public static SigningCredentials Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
}