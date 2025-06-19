using System;
using System.Threading.Tasks;
using Finbit.Api.Models;
using Finbit.Api.Services.Implementations;
using Finbit.Api.Repositories.Interfaces;
using Moq;
using Xunit;

namespace Finbit.Tests.Services
{
    public class TransactionServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedTransaction()
        {
            // Arrange
            var mockRepo = new Mock<ITransactionRepository>();
            var transactionToCreate = new Transaction
            {
                Amount = 50,
                Concept = "Test transaction",
                Date = DateTimeOffset.UtcNow,
                Type = TransactionType.Expense,
                Category = TransactionCategory.Food,
                Username = "user"
            };

            mockRepo
                .Setup(repo => repo.CreateAsync(transactionToCreate))
                .ReturnsAsync(transactionToCreate);

            var service = new TransactionService(mockRepo.Object);

            // Act
            var result = await service.CreateAsync(transactionToCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test transaction", result.Concept);
            Assert.Equal("user", result.Username);
            Assert.Equal(50, result.Amount);
        }
    }
}
