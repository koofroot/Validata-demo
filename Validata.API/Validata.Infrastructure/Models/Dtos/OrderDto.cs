namespace Validata.Infrastructure.Models.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public bool IsCompleted { get; set; }

        public IEnumerable<ItemDto> Items { get; set; }
    }
}
