using BrokeButWoke.Data;
using BrokeButWoke.Dtos;
using BrokeButWoke.Entities;
using Microsoft.EntityFrameworkCore;


namespace BrokeButWoke.Services.ExpenseService
{

    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;

        public ExpenseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenseDto>> GetAllAsync()
        {
            return await _context.Expenses
                .Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Cost = e.Cost,
                    Date = e.Date,
                    SubCategoryId = e.SubCategoryId
                })
                .ToListAsync();
        }

        public async Task<ExpenseDto?> GetByIdAsync(Guid id)
        {
            return await _context.Expenses
                .Where(e => e.Id == id)
                .Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Cost = e.Cost,
                    Date = e.Date,
                    SubCategoryId = e.SubCategoryId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(ExpenseDto dto)
        {
            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                Cost = dto.Cost,
                Date = dto.Date,
                SubCategoryId = dto.SubCategoryId
            };

            _context.Expenses.Add(expense);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, ExpenseDto dto)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return false;

            expense.Cost = dto.Cost;
            expense.Date = dto.Date;
            expense.SubCategoryId = dto.SubCategoryId;

            _context.Expenses.Update(expense);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return false;

            _context.Expenses.Remove(expense);
            return await _context.SaveChangesAsync() > 0;
        }
    }
        
}
