
using Microsoft.AspNetCore.Identity;

namespace WebApplication2Car.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public int Year { get; set; }

        public int Price { get; set; }

        public double Engine { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public bool IsForRent { get; set; }
        public ICollection<Rent> Rents { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public string Category { get; set; }








    }
}
