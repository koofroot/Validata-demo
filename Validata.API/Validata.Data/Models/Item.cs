namespace Validata.Data.Models
{
    public class Item: BaseEntity
    {
        public int Count { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
