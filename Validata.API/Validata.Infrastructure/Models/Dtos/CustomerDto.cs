namespace Validata.Infrastructure.Models.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AddressDto Address { get; set; }
    }
}
