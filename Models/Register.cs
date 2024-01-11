using System.ComponentModel.DataAnnotations;

namespace Task20_consumewebapioftask11_.Models
{
    public class Register
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public int SelectedCountry { get; set; }
        public int SelectedState { get; set; }
        public bool EmailConfirmed { get;set; }


    }
}
