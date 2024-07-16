namespace Validata.Data.Models
{
    public class Order: BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCompleted { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}
