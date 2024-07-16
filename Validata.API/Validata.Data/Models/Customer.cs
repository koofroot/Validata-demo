namespace Validata.Data.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
