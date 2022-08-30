namespace Vidly2.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountRate { get; set; }
        public Customer Customer { get; set; }
        public Rental Rental { get; set; }
    }
}