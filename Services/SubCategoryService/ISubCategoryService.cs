using BrokeButWoke.Dtos;

namespace BrokeButWoke.Services.SubCategoryService
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryDto>> GetAllAsync(Guid mainCategoryId);        
        Task<SubCategoryDto> GetByIdAsync(Guid id);
        Task<SubCategoryDto> CreateAsync(SubCategoryDto subCategoryDto);
        Task<SubCategoryDto> UpdateAsync(Guid id, SubCategoryDto subCategoryDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
