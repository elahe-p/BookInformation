using BookInformation.Application.Abstraction;
using BookInformation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookInformation.Infrastructure.Database;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    #region Domain Model
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithOne()
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}
