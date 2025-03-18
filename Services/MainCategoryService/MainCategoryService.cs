using BrokeButWoke.Data;
using BrokeButWoke.Dtos;
using BrokeButWoke.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrokeButWoke.Services.MainCategoryService
{
    public class MainCategoryService : IMainCategoryService
    {
        private readonly AppDbContext _context;

        public MainCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MainCategoryDto>> GetAllAsync()
        {
            return await _context.MainCategories
                .Select(c => new MainCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    TotalExpenses = c.TotalExpenses
                })
                .ToListAsync(); 
        }


        public async Task<MainCategoryDto?> GetByIdAsync(Guid id)
        {
            var category = await _context.MainCategories.FindAsync(id);
            return category == null ? null : new MainCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                TotalExpenses = category.TotalExpenses
            };
        }

        public async Task<MainCategoryDto> CreateAsync(MainCategoryDto dto)
        {
            var category = new MainCategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                TotalExpenses = dto.TotalExpenses
            };

            _context.MainCategories.Add(category);
            await _context.SaveChangesAsync();

            return new MainCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                TotalExpenses = category.TotalExpenses
            };
        }

        public async Task<MainCategoryDto?> UpdateAsync(Guid id, MainCategoryDto dto)
        {
            var category = await _context.MainCategories.FindAsync(id);
            if (category == null) return null;

            category.Name = dto.Name;
            category.TotalExpenses = dto.TotalExpenses;

            await _context.SaveChangesAsync();

            return new MainCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                TotalExpenses = category.TotalExpenses
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _context.MainCategories.FindAsync(id);
            if (category == null) return false;

            _context.MainCategories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
