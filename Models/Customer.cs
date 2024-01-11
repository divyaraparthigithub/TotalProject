using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task20_consumewebapioftask11_.Models
{
    public class Customer
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

           
            public string Name { get; set; }

            
           // public int ProductId { get; set; }

        

           //public int GenderId { get; set; }
        
            public string Address { get; set; }
            //public string ProductName { get; set; }

           
            public string Phone { get; set; }

           
            public string Email { get; set; }
        [ForeignKey("GenderId")]
        public virtual Gender? Gender { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        public int ProductId { get;  set; }
        public int GenderId { get;  set; }
        
        //public string GenderName { get; set; }
        //public string ProductName { get; set; }
        //public List<SelectListItem> ProductList { get; set; }


        //public Customer()
        //{
        //    ProductList = new List<SelectListItem>();
        //}
        //public Product Product { get; set;}
    }
    
}
