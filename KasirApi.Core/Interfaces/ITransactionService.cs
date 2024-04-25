using KasirApi.Core.Models.Services;

namespace KasirApi.Core.Interfaces;

public interface ITransactionService
{
    public Task<int> CreateAsync(TransactionAddDto value);
    public Task<List<TransactionViewDto>> GetListAsync();
    public Task<TransactionViewDetailDto> FindAsync(int id);
}