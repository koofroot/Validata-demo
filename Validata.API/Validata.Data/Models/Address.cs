namespace Validata.Data.Models
{
    public class Address : BaseEntity
    {
        public string StreetAddress { get; set; }

        public string PostalCode { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
