using System.Transactions;
using Flozacode.Exceptions;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Common;
using KasirApi.Core.Models.Services;
using KasirApi.Core.Validators;

namespace KasirApi.Core.Helpers;

public class TransactionHelper
{
    private readonly ITransactionService _service;
    private readonly IMemberService _memberService;
    private readonly IProductService _productService;

    public TransactionHelper(ITransactionService service, IMemberService memberService, IProductService productService)
    {
        _service = service;
        _memberService = memberService;
        _productService = productService;
    }

    public async Task<List<TransactionViewDto>> GetListAsync()
    {
        return await _service.GetListAsync();
    }

    public async Task<TransactionViewDetailDto> FindAsync(int id)
    {
        return await _service.FindAsync(id);
    }

    public async Task<int> CreateAsync(TransactionAddDto value, CurrentUser currentUser)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            var now = DateTime.UtcNow;
            value.ReferenceNumber = Guid.NewGuid().ToString();
            value.CreatedBy = currentUser.Id;
            value.CreatedAt = now;
            value.UpdatedBy = currentUser.Id;
            value.UpdatedAt = now;

            await InsertDetailAsync(value, currentUser);
            await UpdatePointIfMemberAsync(value, currentUser);
            var result = await _service.CreateAsync(value);
            
            transaction.Complete();
            
            return result;
        }
    }

    private async Task InsertDetailAsync(TransactionAddDto value, CurrentUser currentUser)
    {
        var products = await _productService.GetListAsync();
            
        foreach (var detail in value.Details)
        {
            var product = products.FirstOrDefault(x => x.Id == detail.ProductId);

            if (product == null) 
                throw new RecordNotFoundException("Product not found.");

            var isProductValid = ProductValidator.IsValid(product, detail.Qty);
            if (!isProductValid)
                throw new ApplicationException("Final stock's product is less than zero");
            
            var now = DateTime.UtcNow;
            detail.Price = product.Price;
            detail.Discount = 0;
            detail.Total = (int)(detail.Qty * detail.Price - detail.Discount)!;
            detail.CreatedBy = currentUser.Id;
            detail.CreatedAt = now;
            detail.UpdatedBy = currentUser.Id;
            detail.UpdatedAt = now;
        }
    }

    private async Task UpdatePointIfMemberAsync(TransactionAddDto value, CurrentUser currentUser)
    {
        if (value.MemberId != null)
        {
            var totalPayment = value.Details.Sum(x => x.Total);
            await _memberService.UpdatePointAsync(value.MemberId ?? 0, totalPayment, currentUser.Id);
        }
    }
}