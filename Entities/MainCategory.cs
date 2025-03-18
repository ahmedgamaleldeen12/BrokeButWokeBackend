using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrokeButWoke.Entities
{
    public class MainCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal TotalExpenses { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }

        public DateTime CreatedAt { get; set; } // EF Core will assign a default

        public MainCategory()
        {
            TotalExpenses = 0;
            SubCategories = new List<SubCategory>();
        }
    }
}
