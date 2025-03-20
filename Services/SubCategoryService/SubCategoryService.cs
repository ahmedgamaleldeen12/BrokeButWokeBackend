using BrokeButWoke.Data;
using BrokeButWoke.Dtos;
using BrokeButWoke.Entities;
using Microsoft.EntityFrameworkCore;


namespace BrokeButWoke.Services.SubCategoryService
{
        public class SubCategoryService : ISubCategoryService
        {
            private readonly AppDbContext _context;

            public SubCategoryService(AppDbContext context)
            {
                _context = context;
            }

        public async Task<IEnumerable<SubCategoryDto>> GetAllAsync(Guid mainCategoryId)
        {
            return await _context.SubCategories
                .Where(sc => sc.MainCategoryId == mainCategoryId) 
                .Select(sc => new SubCategoryDto
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    MainCategoryId = sc.MainCategoryId,
                    TotalExpenses = sc.TotalExpenses,
                    CreatedAt = sc.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<SubCategoryDto> GetByIdAsync(Guid id)
            {
                var subCategory = await _context.SubCategories.FindAsync(id);
                if (subCategory == null) return null;

                return new SubCategoryDto
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    MainCategoryId = subCategory.MainCategoryId,
                    TotalExpenses = subCategory.TotalExpenses,
                    CreatedAt = subCategory.CreatedAt
                };
            }

            public async Task<SubCategoryDto> CreateAsync(SubCategoryDto subCategoryDto)
            {
                var subCategory = new SubCategory
                {
                    Id = Guid.NewGuid(),
                    Name = subCategoryDto.Name,
                    MainCategoryId = subCategoryDto.MainCategoryId,
                    TotalExpenses = subCategoryDto.TotalExpenses,
                    CreatedAt = DateTime.UtcNow
                };

                _context.SubCategories.Add(subCategory);
                await _context.SaveChangesAsync();

                subCategoryDto.Id = subCategory.Id;
                subCategoryDto.CreatedAt = subCategory.CreatedAt;
                return subCategoryDto;
            }

            public async Task<SubCategoryDto> UpdateAsync(Guid id, SubCategoryDto subCategoryDto)
            {
                var subCategory = await _context.SubCategories.FindAsync(id);
                if (subCategory == null) return null;

                subCategory.Name = subCategoryDto.Name;
                //subCategory.MainCategoryId = subCategoryDto.MainCategoryId;
                //subCategory.TotalExpenses = subCategoryDto.TotalExpenses;

                _context.SubCategories.Update(subCategory);
                await _context.SaveChangesAsync();

                return subCategoryDto;
            }

            public async Task<bool> DeleteAsync(Guid id)
            {
                var subCategory = await _context.SubCategories.FindAsync(id);
                if (subCategory == null) return false;

                _context.SubCategories.Remove(subCategory);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }


