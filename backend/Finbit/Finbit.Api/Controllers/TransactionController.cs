using Finbit.Api.Models;
using Finbit.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finbit.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    // View my transactions
    [HttpGet("mine")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyTransactions()
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
            return Unauthorized();

        var transactions = await _transactionService.GetByUserAsync(username);
        return Ok(transactions);
    }

    // Create a new personal transaction
    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
            return Unauthorized();

        transaction.Username = username;
        await _transactionService.CreateAsync(transaction);
        return Ok(transaction);
    }

    // View all (admins only)
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllTransactions()
    {
        var all = await _transactionService.GetAllAsync();
        return Ok(all);
    }

    // Update transaction (only admins for now)
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction updated)
    {
        var success = await _transactionService.UpdateAsync(id, updated);
        if (!success)
            return NotFound($"Transaction with id {id} not found.");

        return Ok($"Transaction {id} updated.");
    }


    // Delete transaction (admins only)
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        await _transactionService.DeleteAsync(id);
        return Ok($"Transaction {id} deleted.");
    }
}
