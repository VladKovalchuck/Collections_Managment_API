using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CollectionsManagmentAPI.Service.Service;

public class IdentityService : IIdentityService
{
    private readonly string PasswordSalt = "JSp5ElLOr62P7gSPzaEU0l5sIezuOCTa";
    private readonly string Token = "RJs3QwIyISQRFT9Atq3Q9NjNRENLMoxM";
    
    public void CreatePasswordHash(string password, out byte[] passwordHash)
    {
        using (var hmac = new HMACSHA512(System.Text.Encoding.UTF8.GetBytes(PasswordSalt)))
        {
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash)
    {
        using (var hmac = new HMACSHA512(System.Text.Encoding.UTF8.GetBytes(PasswordSalt)))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    public string CreateToken(UserEntity user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username)
        };
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(Token));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials,
            audience: "CollectionsManagmentAPIClient",
            issuer: "CollectionsManagmentAPIServer"

        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}