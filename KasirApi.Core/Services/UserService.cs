using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Flozacode.Exceptions;
using Flozacode.Models.Paginations;
using Flozacode.Repository;
using KasirApi.Core.Configs;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Customs.Requests;
using KasirApi.Core.Models.Enums;
using KasirApi.Core.Models.Filters;
using KasirApi.Core.Models.Services;
using KasirApi.Repository.Contexts;
using KasirApi.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KasirApi.Core.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IFlozaRepo<User, AppDbContext> _repo;
    private readonly JwtConfigs _jwtConfigs;

    public UserService(IMapper mapper, IFlozaRepo<User, AppDbContext> repo, IOptions<JwtConfigs> jwtConfigs)
    {
        _mapper = mapper;
        _repo = repo;
        _jwtConfigs = jwtConfigs.Value;
    }

    public Task<Pagination<UserViewDto>> GetPagedAsync(UserFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserViewDto>> GetListAsync()
    {
        var result = _repo.AsQueryable.ProjectTo<UserViewDto>(_mapper.ConfigurationProvider).ToList();
        return await Task.FromResult(result);
    }

    public async Task<UserViewDto> FindAsync(long id)
    {
        var result = _repo.AsQueryable.ProjectTo<UserViewDto>(_mapper.ConfigurationProvider).FirstOrDefault(u => u.Id == id);

        if (result == null)
        {
            throw new RecordNotFoundException("User not found.");
        }
        
        return await Task.FromResult(result);
    }

    public async Task<int> CreateAsync(UserAddDto value)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            var entity = _mapper.Map<User>(value);
            var result = await _repo.AddAsync(entity);
            transaction.Complete();
            return result;
        }
    }

    public async Task<int> UpdateAsync(UserUpdDto value)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            var entity = _mapper.Map<User>(value);
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
                throw new RecordNotFoundException("User not found.");
            }
            
            var result = await _repo.DeleteAsync(entity);
            transaction.Complete();
            return result;
        }
    }

    public async Task<UserAuthResponse> Auth(AuthRequest request)
    {
        var exist = _repo
            .AsQueryable
            .AsNoTracking()
            .FirstOrDefault(u => 
                u.Email == request.Email && 
                u.Password == request.Password &&
                u.DataStatusId == (int)DataStatusEnum.Active);

        if (exist == null)
        {
            throw new RecordNotFoundException("User not found.");
        }

        var token = await GenerateToken(exist);

        return  new UserAuthResponse
        {
            Nip = exist.Nip,
            Token = token,
        };
    }
    
    private async Task<string> GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfigs.TokenSecret);

        var claims = new List<Claim> 
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Email", user.Email),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddSeconds(_jwtConfigs.TokenLifeTimes),
            Issuer = _jwtConfigs.Issuer,
            Audience = _jwtConfigs.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return await Task.FromResult(tokenHandler.WriteToken(token));
    }
}