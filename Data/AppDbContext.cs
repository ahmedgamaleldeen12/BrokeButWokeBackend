using BrokeButWoke.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System.Xml;

namespace BrokeButWoke.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainCategory>()
                .Property(m => m.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()"); 

            modelBuilder.Entity<SubCategory>()
                .Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()"); 
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSubCategoryExpenses();
            UpdateMainCategoryExpenses();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateSubCategoryExpenses()
        {
            var affectedSubCategories = ChangeTracker.Entries<Expense>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .Select(e => e.Entity.SubCategoryId)
                .Distinct()
                .ToList();

            var subCategories = SubCategories
                .Where(sc => affectedSubCategories.Contains(sc.Id))
                .Include(sc => sc.Expenses)
                .ToList();

            foreach (var subCategory in subCategories)
            {
                subCategory.TotalExpenses = subCategory.Expenses.Sum(e => e.Cost);
            }
        }

        private void UpdateMainCategoryExpenses()
        {
            var affectedMainCategories = ChangeTracker.Entries<Expense>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .Select(e => e.Entity.SubCategoryId)
                .Distinct()
                .ToList();

            var mainCategories = MainCategories
                .Where(mc => SubCategories.Where(sc => affectedMainCategories.Contains(sc.Id)).Select(sc => sc.MainCategoryId).Contains(mc.Id))
                .Include(mc => mc.SubCategories)
                .ThenInclude(sc => sc.Expenses)
                .ToList();

            foreach (var mainCategory in mainCategories)
            {
                mainCategory.TotalExpenses = mainCategory.SubCategories.Sum(sc => sc.Expenses.Sum(e => e.Cost));
            }
        }

    }

}
