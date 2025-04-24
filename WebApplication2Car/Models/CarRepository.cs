using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using WebApplication2Car.Data;
using WebApplication2Car.Services;

namespace WebApplication2Car.Models
{
    public class CarRepository : ICarSearchService
    {
        private static ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    

        public (IEnumerable<Car> Cars, int TotalCount) GetCars(int pageNumber)
        {
            int pageSize = 5;
            var cars = _context.cars.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            int totalCount = _context.cars.Count();
            return (cars, totalCount);
        }


        public IEnumerable<Car> GetCarById(int id)
        {

            return _context.cars.Include(c=> c.User).Where(c => c.Id == id);
        }

        public Car GetCarId(int id)
        {
            return _context.cars.Include(c => c.User).FirstOrDefault(c => c.Id == id);
        }

        public void Update(Car car)
        {
            _context.cars.Update(car);
            _context.SaveChanges();


        }

        public void Delete(int id)
        {
            var remove = _context.cars.FirstOrDefault(c => c.Id == id);
            _context.Remove(remove);
            _context.SaveChanges();
        }

        public void CancelRent(int id)
        {
            var cancel = _context.Rents.FirstOrDefault(r=> r.CarId == id);
            var car = _context.cars.FirstOrDefault(c => c.Id == id);
            car.IsForRent = true;
            _context.Remove(cancel);
            _context.SaveChanges();
        }
        public IEnumerable<Car> SearchCars(string manufacturer, string model, int? year, double? engine, int? price,string category)
        {
            var query = _context.cars.AsQueryable();

           


            if (!string.IsNullOrWhiteSpace(manufacturer))
            {
                query = query.Where(c => c.Manufacturer == manufacturer);
            }

            if (!string.IsNullOrWhiteSpace(model))
            {
                query = query.Where(c => c.Model == model);
            }

            if (year.HasValue && year.Value > 0)
            {
                query = query.Where(c => c.Year == year.Value);
            }

            if (engine.HasValue && engine.Value > 0)
            {
                query = query.Where(c => c.Engine == engine.Value);
            }

            if (price.HasValue && price.Value > 0)
            {
                query = query.Where(c => c.Price <= price.Value);
            }


            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(c => c.Category == category);
            }


            return query.ToList();
        }






    }
}
