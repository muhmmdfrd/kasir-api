namespace KasirApi.Core.Models.Services;

public class ProductDto
{
    public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Stock { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
}

public class ProductViewDto : ProductDto
{
    
}

public class ProductAddDto : ProductDto
{
    
}

public class ProductUpdDto : ProductDto
{
    
}
        