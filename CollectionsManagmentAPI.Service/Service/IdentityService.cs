using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Enums;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CollectionsManagmentAPI.Service.Service;

public class IdentityService : IIdentityService
{
    private readonly string PasswordSalt = "JSp5ElLOr62P7gSPzaEU0l5sIezuOCTa";
    

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
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("Id", user.Id.ToString()),
            new Claim("Banned", user.IsBlocked.ToString())
        };
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(AuthOptions.Expires),
            signingCredentials: AuthOptions.Credentials,
            audience: AuthOptions.Audience,
            issuer: AuthOptions.Issuer
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}