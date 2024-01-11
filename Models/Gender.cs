using System.ComponentModel.DataAnnotations;

namespace Task20_consumewebapioftask11_.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string GenderName { get; set; }
        public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
    }
}
