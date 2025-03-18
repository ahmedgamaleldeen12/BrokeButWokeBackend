using Microsoft.AspNetCore.Mvc;
using BrokeButWoke.Services.ExpenseService;
using BrokeButWoke.Dtos;

namespace BrokeButWoke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService) 
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var expenses = await _expenseService.GetAllAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense == null)
                return NotFound();
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseDto dto)
        {
            var result = await _expenseService.CreateAsync(dto);
            if (!result)
                return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ExpenseDto dto)
        {
            var result = await _expenseService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _expenseService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}
