using System.ComponentModel.DataAnnotations;

namespace BrokeButWoke.Dtos
{
    public class SubCategoryDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public Guid MainCategoryId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Expenses must be a positive value.")]
        public decimal TotalExpenses { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
