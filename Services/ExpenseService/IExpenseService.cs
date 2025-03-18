using BrokeButWoke.Dtos;

namespace BrokeButWoke.Services.ExpenseService
{
    public interface IExpenseService
    {
        Task<List<ExpenseDto>> GetAllAsync();
        Task<ExpenseDto?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(ExpenseDto dto);
        Task<bool> UpdateAsync(Guid id, ExpenseDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
