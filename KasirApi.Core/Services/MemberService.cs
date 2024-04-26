using System.Transactions;
using AutoMapper;
using  AutoMapper.QueryableExtensions;
using Flozacode.Exceptions;
using Flozacode.Models.Paginations;
using Flozacode.Repository;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Filters;
using KasirApi.Core.Models.Services;
using KasirApi.Core.Models.Contexts;
using KasirApi.Core.Models.Entities;

namespace KasirApi.Core.Services;

public class MemberService : IMemberService
{
    private readonly IMapper _mapper;
    private readonly IFlozaRepo<Member, AppDbContext> _repo;

    public Task<Pagination<MemberViewDto>> GetPagedAsync(MemberFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MemberViewDto>> GetListAsync()
    {
        var result = _repo.AsQueryable.ProjectTo<ProductViewDto>(_mapper.ConfigurationProvider).ToList();
        return await Task.FromResult(result);
    }

    public async Task<MemberViewDto> FindAsync(long id)
    {
        var result = _repo.AsQueryable.ProjectTo<MemberViewDto>(_mapper.ConfigurationProvider).FirstOrDefault(u => u.Id == id);

        if (result == null)
        {
            throw new RecordNotFoundException("Member not found.");
        }
        
        return await Task.FromResult(result);
    }

    public async Task<int> CreateAsync(MemberUpdDto value)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            var entity = _mapper.Map<Member>(value);
            var result = await _repo.AddAsync(entity);
            transaction.Complete();
            return result;
        }
    }

    public async Task<int> UpdateAsync(MemberUpdDto value)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            var entity = _mapper.Map<Member>(value);
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
                throw new RecordNotFoundException("Member not found.");
            }
            
            var result = await _repo.DeleteAsync(entity);
            transaction.Complete();
            return result;
        }
    }
}