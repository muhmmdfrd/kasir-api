using Flozacode.Repository;
using KasirApi.Core.Models.Enums;
using KasirApi.Repository.Contexts;
using KasirApi.Repository.Entities;

namespace KasirApi.Core.Helpers;

public class DashboardHelper
{
    private readonly IFlozaRepo<Transaction, AppDbContext> _transactionRepo;
    private readonly IFlozaRepo<Product, AppDbContext> _productRepo;
    private readonly IFlozaRepo<Member, AppDbContext> _memberRepo;
    private readonly IFlozaRepo<TransactionDetail, AppDbContext> _transactionDetailRepo;
    private readonly IFlozaRepo<User, AppDbContext> _userRepo;

    public DashboardHelper(
        IFlozaRepo<Transaction, AppDbContext> transactionRepo, 
        IFlozaRepo<Product, AppDbContext> productRepo, 
        IFlozaRepo<Member, AppDbContext> memberRepo, 
        IFlozaRepo<TransactionDetail, AppDbContext> transactionDetailRepo, 
        IFlozaRepo<User, AppDbContext> userRepo)
    {
        _transactionRepo = transactionRepo;
        _productRepo = productRepo;
        _memberRepo = memberRepo;
        _transactionDetailRepo = transactionDetailRepo;
        _userRepo = userRepo;
    }

    public async Task<object> GetSummaryAsync()
    {
        var products = _productRepo.AsQueryable.Count(q => q.Stock > 0);
        var members = _memberRepo.AsQueryable.Count(q => q.DataStatusId == (int)DataStatusEnum.Active);
        var allTransactions = _transactionRepo.AsQueryable.Count();
        var revenue = _transactionDetailRepo.AsQueryable.Sum(q => q.Total);
        var employees = _userRepo.AsQueryable.Count(q => q.RoleId == 1);

        var result = new
        {
            ProductCount = products,
            MemberCount = members,
            TransactionCount = allTransactions,
            Revenue = revenue,
            EmployeeCount = employees,
        };

        return await Task.FromResult(result);
    }
}