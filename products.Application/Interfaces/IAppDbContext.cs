using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using products.Domain.Entities;

namespace products.Application.Interfaces;

public interface IAppDbContext
{
    ChangeTracker ChangeTracker { get; }

    DbSet<Product> Products { get; set; }






    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
