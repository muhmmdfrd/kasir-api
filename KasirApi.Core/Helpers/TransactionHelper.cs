using System.Transactions;
using Flozacode.Exceptions;
using Flozacode.Repository;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Common;
using KasirApi.Core.Models.Services;
using KasirApi.Repository.Contexts;
using KasirApi.Repository.Entities;

namespace KasirApi.Core.Helpers;

public class TransactionHelper
{
    private readonly ITransactionService _service;
    private readonly IFlozaRepo<Product, AppDbContext> _productRepo;

    public TransactionHelper(ITransactionService service, IFlozaRepo<Product, AppDbContext> productRepo)
    {
        _service = service;
        _productRepo = productRepo;
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
            
            // TODO: now using repo
            // then using service
            var products = _productRepo.AsQueryable.ToList();
            
            foreach (var detail in value.Details)
            {
                // TODO: percentage using dummy data first
                // then using appsettings.json

                var product = products.FirstOrDefault(x => x.Id == detail.ProductId);

                if (product == null) 
                    throw new RecordNotFoundException("Product not found.");

                detail.Price = product.Price;
                detail.Discount = value.MemberId == null ? 0 : (int)(detail.Price * 0.4);
                detail.Total = (int)(detail.Qty * detail.Price - detail.Qty * detail.Discount)!;
                detail.CreatedBy = currentUser.Id;
                detail.CreatedAt = now;
                detail.UpdatedBy = currentUser.Id;
                detail.UpdatedAt = now;
            }

            var result = await _service.CreateAsync(value);
            
            transaction.Complete();

            return result;
        }
    }
}