using BookOrders.Areas.Identity.Data;
using BookOrders.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookOrders.Models
{
    public class BookOrdersContext : IdentityDbContext<BookOrdersUser>
    {
        public BookOrdersContext(DbContextOptions<BookOrdersContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "Admin".ToUpper()
                    },
                    new IdentityRole
                    {
                        Name = "PowerUser",
                        NormalizedName = "PowerUser".ToUpper()
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "User".ToUpper()
                    },
                    new IdentityRole
                    {
                        Name = "Guest",
                        NormalizedName = "Guest".ToUpper()
                    }
                );

            builder.Entity<Data.Models.Category>(e => {
                e.HasKey(x => x.Id);
                e.HasAlternateKey(x => x.Identifier);
                e.Property("CreatedAtUtc").HasDefaultValueSql("SYSUTCDATETIME()").ValueGeneratedOnAdd();
                e.Property("LastModifiedAtUtc").HasDefaultValueSql("SYSUTCDATETIME()").ValueGeneratedOnAddOrUpdate();
                e.Property("Name").HasMaxLength(400).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();
                e.Property("NameNormalized").HasMaxLength(400).IsRequired();
                e.HasIndex(x => x.NameNormalized).IsUnique();
                e.Property("Disabled").HasDefaultValue(false);
                e.HasMany(x => x.Children)
                    .WithOne(x => x.Parent)
                    .HasForeignKey(x => x.ParentId)
                    .HasConstraintName("FK_Category_ParentCategory");
                e.HasOne(x => x.LastModifier)
                    .WithMany(x => x.ModifiedCategories)
                    .HasForeignKey(x => x.LastModifiedId)
                    .HasConstraintName("FK_Category_LastModifiedUser");
            });
        }

        public DbSet<Data.Models.Category> Categories { get; set; }
    }
}
