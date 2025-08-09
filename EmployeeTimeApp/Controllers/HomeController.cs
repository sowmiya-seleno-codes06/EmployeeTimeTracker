// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using EmployeeTimeApp.Models;

// namespace EmployeeTimeApp.Controllers;

// public class HomeController : Controller
// {
//     private readonly ILogger<HomeController> _logger;

//     public HomeController(ILogger<HomeController> logger)
//     {
//         _logger = logger;
//     }

//     public IActionResult Index()
//     {
//         return View();
//     }

//     public IActionResult Privacy()
//     {
//         return View();
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }

using EmployeeTimeApp.Models;
using EmployeeTimeApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTimeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;

        public HomeController()
        {
            _apiService = new ApiService();
        }

        public async Task<IActionResult> Index()
        {
            var data = await _apiService.GetTimeEntriesAsync();

            // Calculate total hours per employee
            var grouped = data
                .Where(e => e.DeletedOn == null) // ignore deleted
                .GroupBy(e => e.EmployeeName)
                .Select(g => new EmployeeTotal
                {
                    EmployeeName = g.Key,
                    TotalHours = g.Sum(x => (x.EndTimeUtc - x.StarTimeUtc).TotalHours)
                })
                .OrderByDescending(x => x.TotalHours)
                .ToList();

            return View(grouped);
        }
    }
}
