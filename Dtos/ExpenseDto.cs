namespace BrokeButWoke.Dtos
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public Guid SubCategoryId { get; set; }
    }
}
