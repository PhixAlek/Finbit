using Finbit.Api.Models;

namespace Finbit.Api.Services.Interfaces;
public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<IEnumerable<Transaction>> GetByUserAsync(string username);

    Task<Transaction?> GetByIdAsync(int id);
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<bool> UpdateAsync(int id, Transaction updated);
    Task<bool> DeleteAsync(int id);
}
