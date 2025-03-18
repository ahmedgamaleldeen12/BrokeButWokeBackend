using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrokeButWoke.Entities
{
    public class SubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid MainCategoryId { get; set; }

        [ForeignKey("MainCategoryId")]
        public MainCategory? MainCategory { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalExpenses { get; set; }

        public ICollection<Expense> Expenses { get; set; }

        public DateTime CreatedAt { get; set; } 

        public SubCategory()
        {
            TotalExpenses = 0;
            Expenses = new List<Expense>();
        }
    }
}
