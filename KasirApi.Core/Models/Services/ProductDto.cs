using System.Text.Json.Serialization;

namespace KasirApi.Core.Models.Services;

public class ProductDto
{
    public string Name { get; set; } = null!;
    public int Price { get; set; }
    public int Stock { get; set; }
}

public class ProductViewDto : ProductDto
{
    public int Id { get; set; }
}

public class ProductAddDto : ProductDto
{
    public string? Code { get; set; }
    
    [JsonIgnore]
    public int CreatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}

public class ProductUpdDto : ProductDto
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}
        