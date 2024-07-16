namespace Validata.Data.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
