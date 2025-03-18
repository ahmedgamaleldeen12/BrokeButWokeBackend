using BrokeButWoke.Dtos;

namespace BrokeButWoke.Services.MainCategoryService
{
    public interface IMainCategoryService
    {
        Task<IEnumerable<MainCategoryDto>> GetAllAsync();
        Task<MainCategoryDto?> GetByIdAsync(Guid id);
        Task<MainCategoryDto> CreateAsync(MainCategoryDto dto);
        Task<MainCategoryDto?> UpdateAsync(Guid id, MainCategoryDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
