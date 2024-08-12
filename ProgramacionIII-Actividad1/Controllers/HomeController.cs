using Microsoft.AspNetCore.Mvc;
using ProgramacionIII_Actividad1.Models;
using System.Diagnostics;
using System.IO;

namespace ProgramacionIII_Actividad1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static int visitasTotales = 0;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            visitasTotales++;

            if (HttpContext.Session.GetInt32("RecargasPag") == null)
            {
                HttpContext.Session.SetInt32("RecargasPag", 0);
            }
            HttpContext.Session.SetInt32("RecargasPag", HttpContext.Session.GetInt32("RecargasPag").Value + 1);

            ViewData["VisitasTotales"] = visitasTotales;
            ViewData["RecargasPag"] = HttpContext.Session.GetInt32("RecargasPag").Value;

            var EsPrimeraCarga = HttpContext.Session.GetString("EsPrimeraCarga");

            if (EsPrimeraCarga == null)
            {
                HttpContext.Session.SetString("EsPrimeraCarga", "No");
                ViewData["Message"] = "Hola Mundo - Es la primera vez que se ha cargado la página";
            }
            else
            {
                ViewData["Message"] = "Esta página ya ha sido visitada";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Reload()
        {
            return RedirectToAction("Index");
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
    }
}
