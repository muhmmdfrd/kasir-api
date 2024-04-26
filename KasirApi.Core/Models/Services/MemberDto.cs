using System.Text.Json.Serialization;

namespace KasirApi.Core.Models.Services;

public class MemberDto
{
    public string Nik { get; set; } = null!;
    public string Name { get; set; } = null!;
    // public DateOnly BirthDate { get; set; }
    public string BirthPlace { get; set; } = null!;
    public char? Gender { get; set; }
    public string? Address { get; set; }
}

public class MemberViewDto : MemberDto
{
    public int Id { get; set; }
    public string MemberNumber { get; set; } = null!;
    public DateTime JoinDate { get; set; }
    public int Point { get; set; }
}

public class MemberAddDto : MemberDto
{
    [JsonIgnore]
    public string? MemberNumber { get; set; }
    
    [JsonIgnore]
    public int CreatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public int DataStatusId { get; set; } = 1;
    
    [JsonIgnore]
    public DateTime JoinDate { get; set; }
}

public class MemberUpdDto : MemberDto
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}