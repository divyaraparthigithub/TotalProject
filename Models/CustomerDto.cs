using System.ComponentModel.DataAnnotations;

namespace Task20_consumewebapioftask11_.Models
{
    public class CustomerDto
    {
        [Key]
           public int Id { get; set; }
            public string Name { get; set; }

          
            public int ProductId { get; set; }

            public int GenderId { get; set; }

           
            public string Address { get; set; }

            
            public string Phone { get; set; }

            
            public string Email { get; set; }
        

    }
}
