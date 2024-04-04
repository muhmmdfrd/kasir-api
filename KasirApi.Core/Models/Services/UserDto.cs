namespace KasirApi.Core.Models.Services;

public class UserDto
{
    public string Nip { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int RoleId { get; set; }
}

public class UserViewDto : UserDto
{
    public int Id { get; set; }
    public string RoleName { get; set; }
}

public class UserAddDto : UserDto
{
    public string Password { get; set; } = null!;
    public int DataStatusId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UserUpdDto : UserDto
{
    public string Password { get; set; } = null!;
    public int DataStatusId { get; set; }
    public int Id { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UserAuthResponse
{
    public string Nip { get; set; } = null!;
    public string Token { get; set; } = "";
}