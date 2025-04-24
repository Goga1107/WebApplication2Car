using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication2Car.Data;
using WebApplication2Car.Models;
using WebApplication2Car.Services;


namespace WebApplication2Car.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarSearchService _carSearchService;

        private static CarRepository CarRepository;
        private static ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, ICarSearchService service)
        {
            CarRepository = new CarRepository(context);
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _carSearchService = service;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShowCars(int pageNumber = 1)
        {
            var result = CarRepository.GetCars(pageNumber);
            ViewBag.TotalCount = result.TotalCount;
            ViewBag.CurrentPage = pageNumber;
            return View("ShowCars", result.Cars);
        }

        public IActionResult SearchCars()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Description(int id)
        {
            var car = _carSearchService.GetCarById(id);

            return View("Description", car);

        }


        public IActionResult SearchResult(string manufacturer, string model, int year, double engine, int price,string category)
        {
            var searchResults = _carSearchService.SearchCars(manufacturer, model, year, engine, price,category);

           
            return View("SearchResult", searchResults);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddCar(Car car, IFormFile ImagePath)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.GetUserId();
                car.UserId = userId;
            }
            else
            {
                return RedirectToAction("ShowCars");
            }

            if (ImagePath != null && ImagePath.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImagePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImagePath.CopyTo(fileStream);
                }
                car.ImagePath = uniqueFileName;  
            }

            _context.Add(car);
            _context.SaveChanges();
            return RedirectToAction("ShowCars");
        }

        [HttpGet]

        public IActionResult EditCar(Car car,int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.GetUserId();
                car.UserId = userId;
            }

            var getcarbyid = _carSearchService.GetCarId(id);
            return View(getcarbyid);
        }
        [HttpPost]
        public IActionResult EditCar(Car car)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.GetUserId();
                car.UserId = userId;
            }
            _carSearchService.Update(car);

            return RedirectToAction("ShowCars");
        }

        public IActionResult Delete(int id)
        {
            _carSearchService.Delete(id);
            return RedirectToAction("ShowCars");
        }

        //forrent 
        [HttpGet]
        
        public IActionResult RentCar(int Id)
        {
            var car = _carSearchService.GetCarId(Id);

            if (car != null)
            {
                Rent viewModel = new Rent
                {
                    CarId = car.Id
                };
                return View(viewModel);
            }
            else
            {
                
                return NotFound();
            }

        }



        [HttpPost]
        public IActionResult RentCar(int Id, DateTime startDate, DateTime endDate)
        {
            
            
            var car = _context.cars.FirstOrDefault(c => c.Id == Id && c.IsForRent);
            if (car == null)
            {
                return RedirectToAction("ShowCars");
            }

            
            var rentPrice = CalculateRentPrice(Id,startDate, endDate);
            var userid = GetcurrentUserID();
        
            var rent = new Rent
            {
                UserId = userid,
                CarId = Id,
                StartDate = startDate,
                EndDate = endDate,
                RentPrice = rentPrice
            };

         
            car.IsForRent = false;

   
            _context.Rents.Add(rent);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public IActionResult RentedCar(string userId)
        {

            var rent = _context.Rents.Include(c=> c.Car).Where(r=> r.UserId == userId).ToList();
            return View(rent);

        }
        public string GetcurrentUserID()
        {
            return HttpContext.User.Identity.Name;
        }
        private decimal CalculateRentPrice(int carId,DateTime startDate, DateTime endDate)
        {

            var duration = (endDate - startDate).Days;
            var car = _context.cars.FirstOrDefault(c => c.Id == carId);
            decimal dailyPrice = car.Price;
            return duration * dailyPrice;
        }

        public IActionResult CancelRent(int id)
        {
            _carSearchService.CancelRent(id);
      
            return RedirectToAction("ShowCars");
        }


    }

  
}