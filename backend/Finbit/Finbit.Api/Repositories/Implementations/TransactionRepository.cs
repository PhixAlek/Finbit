using Finbit.Api.Data;
using Finbit.Api.Models;
using Finbit.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finbit.Api.Repositories.Implementations;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _context.Transactions.FindAsync(id);
    }

    public async Task<IEnumerable<Transaction>> GetByUserAsync(string username)
    {
        return await _context.Transactions
            .Where(t => t.Username == username)
            .ToListAsync();
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<bool> UpdateAsync(int id, Transaction updated)
    {
        var existing = await _context.Transactions.FindAsync(id);
        if (existing == null) return false;

        existing.Amount = updated.Amount;
        existing.Concept = updated.Concept;
        existing.Date = updated.Date;
        existing.Type = updated.Type;
        existing.Category = updated.Category;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) return false;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }
}
