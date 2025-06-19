using Finbit.Api.Models;
using Finbit.Api.Repositories.Implementations;
using Finbit.Api.Repositories.Interfaces;
using Finbit.Api.Services.Interfaces;

namespace Finbit.Api.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Transaction>> GetByUserAsync(string username)
    {
        return await _repository.GetByUserAsync(username);

    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        return await _repository.CreateAsync(transaction);
    }

    public async Task<bool> UpdateAsync(int id, Transaction updated)
    {
        return await _repository.UpdateAsync(id, updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
