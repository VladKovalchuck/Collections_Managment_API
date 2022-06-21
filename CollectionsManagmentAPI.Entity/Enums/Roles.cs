using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;


namespace CollectionsManagmentAPI.Entity.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum Roles
{
    [EnumMember(Value = "user")]
    User,
    [EnumMember(Value = "admin")]
    Admin 
}