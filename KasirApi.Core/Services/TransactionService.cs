using AutoMapper;
using AutoMapper.QueryableExtensions;
using Flozacode.Exceptions;
using Flozacode.Repository;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Services;
using KasirApi.Repository.Contexts;
using KasirApi.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace KasirApi.Core.Services;

public class TransactionService : ITransactionService
{
    private readonly IFlozaRepo<Transaction, AppDbContext> _repo;
    private readonly IMapper _mapper;

    public TransactionService(IFlozaRepo<Transaction, AppDbContext> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(TransactionAddDto value)
    {
        var entity = _mapper.Map<Transaction>(value);
        return await _repo.AddAsync(entity);
    }

    public async Task<List<TransactionViewDto>> GetListAsync()
    {
        var result = _repo.AsQueryable
            .ProjectTo<TransactionViewDto>(_mapper.ConfigurationProvider)
            .ToList();
        
        return await Task.FromResult(result);
    }

    public async Task<TransactionViewDetailDto> FindAsync(int id)
    {
        var result = _repo.AsQueryable
            .Include(q => q.TransactionDetails)
            .AsSplitQuery()
            .ProjectTo<TransactionViewDetailDto>(_mapper.ConfigurationProvider).
            FirstOrDefault(u => u.Id == id);

        if (result == null)
        {
            throw new RecordNotFoundException("Transaction not found.");
        }
        
        return await Task.FromResult(result);
    }
}