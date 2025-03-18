using BrokeButWoke.Dtos;
using BrokeButWoke.Services.MainCategoryService;
using Microsoft.AspNetCore.Mvc;

namespace BrokeButWoke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainCategoryController : ControllerBase
    {
        private readonly IMainCategoryService _service;

        public MainCategoryController(IMainCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MainCategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdCategory = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MainCategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedCategory = await _service.UpdateAsync(id, dto);
            if (updatedCategory == null) return NotFound();

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
