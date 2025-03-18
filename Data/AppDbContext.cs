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
        public override int SaveChanges()
        {
            UpdateSubCategoryExpenses();
            UpdateMainCategoryExpenses();
            HandleExpenseDeletions();
            return base.SaveChanges();
        }

        private void UpdateSubCategoryExpenses()
        {
            var newExpenses = ChangeTracker.Entries<Expense>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            foreach (var expense in newExpenses)
            {
                var subCategory = SubCategories.Find(expense.SubCategoryId);
                if (subCategory != null)
                {
                    subCategory.TotalExpenses += expense.Cost;
                }
            }
        }

        private void UpdateMainCategoryExpenses()
        {
            var newExpenses = ChangeTracker.Entries<Expense>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            foreach (var expense in newExpenses)
            {
                var subCategory = SubCategories.Find(expense.SubCategoryId);
                if (subCategory != null)
                {
                    var mainCategory = MainCategories.Find(subCategory.MainCategoryId);
                    if (mainCategory != null)
                    {
                        mainCategory.TotalExpenses += expense.Cost;
                    }
                }
            }
        }

        private void HandleExpenseDeletions()
        {
            var deletedExpenses = ChangeTracker.Entries<Expense>()
                .Where(e => e.State == EntityState.Deleted)
                .Select(e => e.Entity)
                .ToList();

            foreach (var expense in deletedExpenses)
            {
                var subCategory = SubCategories.Find(expense.SubCategoryId);
                if (subCategory != null)
                {
                    subCategory.TotalExpenses -= expense.Cost;
                    var mainCategory = MainCategories.Find(subCategory.MainCategoryId);
                    if (mainCategory != null)
                    {
                        mainCategory.TotalExpenses -= expense.Cost;
                    }
                }
            }

        }
    }

}
