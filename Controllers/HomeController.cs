using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using TP2.Models;

namespace TP2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        [Route("/Personne/all")]
        public IActionResult Personne()
        {

                Personal_info personal_Info = new Personal_info();
                Person[] resultTable = personal_Info.GetAllPerson();
                ViewBag.resultTable = resultTable;
                ViewBag.userID = null;
                return View();
        }
        [HttpGet]
        [Route("/Personne/{userID:int}")]
        public IActionResult Personne(int userID)
        {
            Personal_info personal_Info = new Personal_info();
            Person resultUser = personal_Info.GetPerson(userID);
            if(resultUser == null)
            {
                ViewBag.resultTable = null;
            }
            else
            {
                ViewBag.resultTable = resultUser;
            }
            ViewBag.userID = userID;
            return View();
        }

        [HttpGet]
        [Route("/Personne/search")]
        public IActionResult PersonneSearch()
        {
            return View();
        }

        [HttpPost]
        [Route("/Personne/SearchHandler")]
        public IActionResult SearchHandler(FormData formCentent)
        {
            string firstName = formCentent.firstName;
            string country = formCentent.country;

            Personal_info request_data = new Personal_info();
            Person resulted_data = request_data.GetPersonBySearch(firstName, country);

            if( resulted_data == null)
            {
                return Redirect("/Personne/-1");
            }
            else
            {
                return Redirect("/Personne/"+resulted_data.Id);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}