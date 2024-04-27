using KasirApi.Core.Models.Services;

namespace KasirApi.Core.Validators;

public abstract class ProductValidator
{
    public static bool IsValid(ProductViewDto product)
    {
        return product.Stock > 0;
    }

    public static bool IsValid(ProductViewDto product, int qty)
    {
        var finalQty = product.Stock - qty;
        return finalQty >= 0;
    }
}