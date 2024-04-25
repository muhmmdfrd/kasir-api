using System.Text.Json.Serialization;

namespace KasirApi.Core.Models.Services;

public class TransactionDto
{
    public int? MemberId { get; set; }
}

public class TransactionViewDto : TransactionDto
{
    public int Id { get; set; }
    public string ReferenceNumber { get; set; } = null!;
    public string? MemberName { get; set; }
}

public class TransactionViewDetailDto : TransactionDto
{
    public int Id { get; set; }
    public string ReferenceNumber { get; set; } = null!;
    public string? MemberName { get; set; }
    public List<TransactionDetailViewDto> Details { get; set; } = new();
}

public class TransactionUpdDto : TransactionDto
{
    public int Id { get; set; }
    public List<TransactionDetailUpdDto> Details { get; set; } = new();
    
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}

public class TransactionAddDto : TransactionDto
{
    [JsonIgnore]
    public string? ReferenceNumber { get; set; } = null!;
    
    public List<TransactionDetailAddDto> Details { get; set; } = new();
    
    [JsonIgnore]
    public int CreatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}