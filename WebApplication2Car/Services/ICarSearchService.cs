using WebApplication2Car.Models;

namespace WebApplication2Car.Services
{
    public interface ICarSearchService
    {
        IEnumerable<Car> SearchCars(string manufacturer, string model, int? year, double? engine, int? price, string category);
      //  IEnumerable<Car> GetCars(int pageNumber);
        IEnumerable<Car> GetCarById(int id);
        Car GetCarId(int id);




        void Update(Car car);


        void Delete(int id);
        void CancelRent(int id);
    }
}
