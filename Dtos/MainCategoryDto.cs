namespace BrokeButWoke.Dtos
{
    public class MainCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TotalExpenses { get; set; }
    }
}
