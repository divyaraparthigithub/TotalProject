using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task20_consumewebapioftask11_.Models
{
    
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int Quantity { get; set; }
        //public Customer customer { get; set; }
       // public virtual ICollection<Customer> Customer{ get; } = new List<Customer>();
    }

}
