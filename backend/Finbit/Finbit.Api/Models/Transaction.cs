using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Finbit.Api.Models
{
    public enum TransactionType { Income, Expense }
    public enum TransactionCategory { Food, Transport, Salary, Other }

    public class Transaction
    {
        public int Id { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        [Required]
        public string Concept { get; set; } = string.Empty;

        public DateTimeOffset Date { get; set; }

        public TransactionType Type { get; set; }

        public TransactionCategory Category { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
