using Finbit.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finbit.Api.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(int id);
    Task<IEnumerable<Transaction>> GetByUserAsync(string username);
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<bool> UpdateAsync(int id, Transaction updated);
    Task<bool> DeleteAsync(int id);
}
