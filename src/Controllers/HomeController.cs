using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TabulatorDynamicQueryBuilder.Extensions;
using TabulatorDynamicQueryBuilder.Infrastructure;
using TabulatorDynamicQueryBuilder.Models;

namespace TabulatorDynamicQueryBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> GetPersons(TabulatorOptionsModel options)
        {
            var results = await _context.BasicData
                .Select(p => new
                {
                    p.PersonId,
                    p.Title,
                    p.FirstName,
                    p.LastName,
                    p.Date,
                    p.Age,
                    p.Email,
                    p.Nationality,
                    Photo = p.Picture!.Thumbnail
                })
                .TabulatorQueryBuilder(options)
                .Build();

            return Ok(results);
        }
    }
}