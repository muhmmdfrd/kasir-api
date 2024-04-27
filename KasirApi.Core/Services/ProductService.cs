using System.Transactions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Flozacode.Exceptions;
using Flozacode.Models.Paginations;
using Flozacode.Repository;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Filters;
using KasirApi.Core.Models.Services;
using KasirApi.Repository.Contexts;
using KasirApi.Repository.Entities;

namespace KasirApi.Core.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IFlozaRepo<Product, AppDbContext> _repo;

    public Task<Pagination<ProductViewDto>> GetPagedAsync(ProductFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductViewDto>> GetListAsync()
    {
        var result = _repo.AsQueryable.ProjectTo<ProductViewDto>(_mapper.ConfigurationProvider).ToList();
        return await Task.FromResult(result);
    }

    public async Task<ProductViewDto> FindAsync(long id)
    {
        var result = _repo.AsQueryable.ProjectTo<ProductViewDto>(_mapper.ConfigurationProvider).FirstOrDefault(u => u.Id == id);

        if (result == null)
        {
            throw new RecordNotFoundException("Product not found.");
        }
        
        return await Task.FromResult(result);
    }

    public async Task<int> CreateAsync(ProductAddDto value)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            var entity = _mapper.Map<Product>(value);
            var result = await _repo.AddAsync(entity);
            transaction.Complete();
            return result;
        }
    }

    public async Task<int> UpdateAsync(ProductUpdDto value)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            var entity = _mapper.Map<Product>(value);
            var result = await _repo.UpdateAsync(entity);
            transaction.Complete();
            return result;
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            var entity = _repo.AsQueryable.FirstOrDefault(u => u.Id == id);

            if (entity == null)
            {
                throw new RecordNotFoundException("Product not found.");
            }
            
            var result = await _repo.DeleteAsync(entity);
            transaction.Complete();
            return result;
        }
    }
}