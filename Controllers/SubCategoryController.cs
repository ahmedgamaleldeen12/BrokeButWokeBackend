using BrokeButWoke.Dtos;
using BrokeButWoke.Services.SubCategoryService;
using Microsoft.AspNetCore.Mvc;

namespace BrokeButWoke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet("by-main-category/{mainCategoryId}")]
        public async Task<IActionResult> GetAllSubCatgeoriesByMainCategoryId(Guid mainCategoryId)
        {
            var subCategories = await _subCategoryService.GetAllAsync(mainCategoryId);

            return Ok(subCategories ?? new List<SubCategoryDto>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subCategory = await _subCategoryService.GetByIdAsync(id);
            if (subCategory == null) return NotFound("Sub-category not found.");
            return Ok(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubCategoryDto subCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdSubCategory = await _subCategoryService.CreateAsync(subCategoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdSubCategory.Id }, createdSubCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SubCategoryDto subCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedSubCategory = await _subCategoryService.UpdateAsync(id, subCategoryDto);
            if (updatedSubCategory == null) return NotFound("Sub-category not found.");

            return Ok(updatedSubCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _subCategoryService.DeleteAsync(id);
            if (!deleted) return NotFound("Sub-category not found.");

            return NoContent();
        }
    }
}
