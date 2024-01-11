namespace Task20_consumewebapioftask11_.Models
{
    public class State
    {
        public int id { get; set; }
        public string StateName { get; set; }
        public int Countryid { get; set; } 
        public Country Country { get; set; } 
    }
}
