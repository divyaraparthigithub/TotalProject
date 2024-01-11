using Microsoft.AspNetCore.Mvc.Rendering;

namespace Task20_consumewebapioftask11_.Models
{
    public class Registration
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
        public IEnumerable<SelectListItem> Country { get; set; }
        public Dictionary<string, IEnumerable<string>> State { get; set; }
        public Registration()
        {
            Country = new List<SelectListItem>();
            State = new Dictionary<string, IEnumerable<string>>();

        }
        //public IEnumerable<string> Country { get; set; }
        //public Dictionary<string, IEnumerable<string>> State { get; set; }
    }
}
