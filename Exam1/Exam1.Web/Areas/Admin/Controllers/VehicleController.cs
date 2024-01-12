using Autofac;
using Exam1.Infrastructure;
using Exam1.Web.Areas.Admin.Models;
using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILifetimeScope scope,

        ILogger<VehicleController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(VehicleInsertModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    vehicleModel.Resolve(_scope);
                    await vehicleModel.InsertVehicleAsync();
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "New vehicle added in the workshop!",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");   

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to added a new car in the workshop!",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(vehicleModel);
        }


    }
}
