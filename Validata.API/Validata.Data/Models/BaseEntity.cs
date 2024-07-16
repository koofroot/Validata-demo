namespace Validata.Data.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
