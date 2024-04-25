using KasirApi.Repository.Entities;

namespace KasirApi.Core.Validators;

public class ProductValidator
{
    public static bool IsValid(Product product)
    {
        return product.Stock > 0;
    }

    public static bool IsValid(Product product, int qty)
    {
        var finalQty = product.Stock - qty;
        return finalQty >= 0;
    }
}