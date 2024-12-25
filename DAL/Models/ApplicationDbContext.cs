using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for models
        public DbSet<User> Users { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User model configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Email).IsUnique(); // Ensure unique usernames
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Role).HasDefaultValue("User");
            });

            // InventoryItem model configuration
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.HasKey(e => e.ItemId);
                entity.Property(e => e.ItemName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(250);
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.StockQuantity).IsRequired();
                entity.Property(e => e.CreatedDate).IsRequired();

                // Define relationship
                entity.HasOne(i => i.User)
                      .WithMany(u => u.InventoryItems)
                      .HasForeignKey(i => i.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete inventory items when user is deleted
            });
        }
    }
}
