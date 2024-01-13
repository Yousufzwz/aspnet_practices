using Autofac;
using Exam1.Domain;
using Exam1.Infrastructure;
using Exam1.Web.Areas.Admin.Models;
using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
            var model = _scope.Resolve<VehicleInsertModel>();
            return View(model);
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
				catch (DuplicateNameException de)
				{
					TempData.Put("ResponseMessage", new ResponseModel
					{
						Message = de.Message,
						Type = ResponseTypes.Danger
					});
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


        public async Task<JsonResult> GetCars()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<VehiclesListModel>();

            var data = await model.GetDataOfVehiclesAsync(dataTablesModel);
            return Json(data);
        }


		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
            var vehicleModel = _scope.Resolve<VehiclesListModel>();

			if (ModelState.IsValid)
			{
				try
				{
					await vehicleModel.RemoveVehicleAsync(id);
					TempData.Put("ResponseMessage", new ResponseModel
					{
						Message = "Vehicle successfully removed from the workshop!",
						Type = ResponseTypes.Success
					});

					return RedirectToAction("Index");

				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Server Error");

					TempData.Put("ResponseMessage", new ResponseModel
					{
						Message = "Failed to removed vehicle from the workshop!",
						Type = ResponseTypes.Danger
					});
				}
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Update(Guid id)
		{
			var vehicleModel = _scope.Resolve<VehicleUpdateModel>();
			await vehicleModel.OldToNewAsync(id);
			return View(vehicleModel);
		}


		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(VehicleUpdateModel vehicleModel)
		{
			vehicleModel.Resolve(_scope);

			if (ModelState.IsValid)
			{
				try
				{
					await vehicleModel.UpdateVehicleAsync();
					TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Vehicle features updated successfuly!",
					    Type = ResponseTypes.Success
                });
                    return RedirectToAction("Index");
				}
				catch (DuplicateNameException de)
				{
					TempData.Put("ResponseMessage", new ResponseModel
					{
						Message = de.Message,
						Type = ResponseTypes.Danger
					});
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Server Error");

					TempData.Put("ResponseMessage", new ResponseModel
					{
						Message = "Failed to updated data of a vehicle!",
						Type = ResponseTypes.Danger
					});
				}
			}

			return View(vehicleModel);
		}

	}
}
