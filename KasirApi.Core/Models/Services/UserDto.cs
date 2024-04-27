using System.Text.Json.Serialization;

namespace KasirApi.Core.Models.Services;

public class UserDto
{
    public string Email { get; set; } = null!;
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
}

public class UserViewDto : UserDto
{
    public int Id { get; set; }
    public string RoleName { get; set; } = "";
    public string Nip { get; set; } = null!;
}

public class UserAddDto : UserDto
{
    public string Password { get; set; } = null!;
    
    [JsonIgnore]
    public string? Nip { get; set; }
    
    [JsonIgnore]
    public int DataStatusId { get; set; } = 1;
    [JsonIgnore]
    public int CreatedBy { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}

public class UserUpdDto : UserDto
{
    public string? Password { get; set; }
    public int Id { get; set; }
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}

public class UserAuthResponse
{
    public string Nip { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Id { get; set; }
}