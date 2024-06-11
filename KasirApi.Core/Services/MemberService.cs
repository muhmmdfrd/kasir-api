using System.Transactions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Flozacode.Exceptions;
using Flozacode.Models.Paginations;
using Flozacode.Repository;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Customs.Response;
using KasirApi.Core.Models.Filters;
using KasirApi.Core.Models.Services;
using KasirApi.Repository.Contexts;
using KasirApi.Repository.Entities;

namespace KasirApi.Core.Services;

public class MemberService : IMemberService
{
    private const int EXP = 5000;
    private readonly IMapper _mapper;
    private readonly IFlozaRepo<Member, AppDbContext> _repo;

    public MemberService(IMapper mapper, IFlozaRepo<Member, AppDbContext> repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public Task<Pagination<MemberViewDto>> GetPagedAsync(MemberFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MemberViewDto>> GetListAsync()
    {
        var result = _repo.AsQueryable.ProjectTo<MemberViewDto>(_mapper.ConfigurationProvider).ToList();
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
    
    public async Task<int> CreateAsync(MemberAddDto value)
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
            var exist = _repo.AsQueryable.FirstOrDefault(x => x.Id == value.Id);

            if (exist == null)
                throw new RecordNotFoundException("Member not found.");
            
            var entity = _mapper.Map(value, exist);
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

   public async Task UpdatePointAsync(int id, int totalPayment, int creator)
    {
        var member = _repo.AsQueryable.FirstOrDefault(x => x.Id == id);
        if (member == null)
            throw new RecordNotFoundException("member not found.");
        
        member.Point += member.Point + totalPayment / EXP;
        member.UpdatedAt = DateTime.UtcNow;
        
        var result = await _repo.UpdateAsync(member);
        if (result <= 0)
            throw new ApplicationException("Failed to update point.");
    }

    public async Task<MemberValidateResponse> ValidateAsync(string memberNumber)
    {
        var member = _repo.AsQueryable.FirstOrDefault(x => x.MemberNumber == memberNumber);

        if (member != null)
        {
            throw new RecordNotFoundException("Member not found");
        }

        var result = new MemberValidateResponse
        {
            Name = member?.Name ?? "",
            Point = member?.Point ?? 0,
        };

        return await Task.FromResult(result);
    }
}