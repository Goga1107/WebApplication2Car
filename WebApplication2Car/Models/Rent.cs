namespace WebApplication2Car.Models
{
    public class Rent
    {
        public int RentId { get; set; }
        public string UserId { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentPrice { get; set; }
        public Car Car { get; set; }
    }
}
