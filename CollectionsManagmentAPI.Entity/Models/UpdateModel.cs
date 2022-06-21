using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CollectionsManagmentAPI.Entity.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;

namespace CollectionsManagmentAPI.Entity;

public class UpdateModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    [EnumDataType(typeof(Roles))]
    [JsonConverter(typeof(StringEnumConverter))]
    public Roles Role { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsBlocked { get; set; }
}