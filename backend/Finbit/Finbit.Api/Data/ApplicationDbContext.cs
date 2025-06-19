using Finbit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Finbit.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Transaction> Transactions => Set<Transaction>();
}
