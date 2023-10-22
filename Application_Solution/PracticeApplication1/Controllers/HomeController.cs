using Microsoft.AspNetCore.Mvc;
using PracticeApplication1.Models;
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