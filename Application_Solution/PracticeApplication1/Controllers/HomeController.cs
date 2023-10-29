using Microsoft.AspNetCore.Mvc;
using PracticeApplication1.Models;
using Serilog;
using System.Diagnostics;

namespace PracticeApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ISMsSender _smsSender;

        public HomeController(ILogger<HomeController> logger,
            IEmailSender emailSender, ISMsSender smsSender)
        {
            _logger = logger; 
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        public IActionResult Index()
        {
            var model = new IndexModel();
            model.Message = "Assalamu Alaikum";
            _logger.LogInformation("I am in index");
            Log.Error("This is an error message");
            Log.Fatal("This is fatal error message");
            Log.Information("This is information message");
            return View(model);
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