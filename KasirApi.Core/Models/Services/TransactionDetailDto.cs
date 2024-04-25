using System.Text.Json.Serialization;

namespace KasirApi.Core.Models.Services;

public class TransactionDetailDto
{
    public int ProductId { get; set; }
    public int Qty { get; set; }
}

public class TransactionDetailViewDto : TransactionDetailDto
{
    public int Id { get; set; }
    public int TransactionId { get; set; }
    public int Price { get; set; }
    public string ProductName { get; set; } = "";
    public int Total { get; set; }
}

public class TransactionDetailAddDto : TransactionDetailDto
{
    [JsonIgnore]
    public int Total { get; set; }
    
    [JsonIgnore]
    public int Price { get; set; }
    
    [JsonIgnore]
    public int? Discount { get; set; }
    
    [JsonIgnore]
    public int CreatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}

public class TransactionDetailUpdDto : TransactionDetailDto
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}