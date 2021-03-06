using CollectionsManagmentAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManagmentAPI.DataAccess;

public class CollectionsDbContext : DbContext
{
    public CollectionsDbContext(DbContextOptions<CollectionsDbContext> options)
        : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .Property(u => u.Role)
            .HasConversion<string>();
        modelBuilder.Entity<CollectionEntity>()
            .HasOne(c => c.User)
            .WithMany(u => u.Collections)
            .HasForeignKey(c => c.UserId);
    }
    public DbSet<CollectionEntity> Collections { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
}