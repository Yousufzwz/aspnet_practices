using Autofac;
using Exam1.Application;
using Exam1.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace Exam1.Web.Areas.Admin.Models;

public class VehicleUpdateModel
{
	private IVehicleManagementService _vehicleService;
	
	[Required]
	public Guid Id { get; set; }

	[Required]
	public string BrandName { get; set; }
	public string Category { get; set; }

	[Required, Range(10000, 100000, ErrorMessage = "Vehicle price range should be in 10000 to 100000")]
	public double Price { get; set; }

	public VehicleUpdateModel() { }

	public VehicleUpdateModel(IVehicleManagementService vehicleService)
	{
		_vehicleService = vehicleService;
	}

	public void Resolve(ILifetimeScope scope)
	{
		_vehicleService = scope.Resolve<IVehicleManagementService>();
	}

	public async Task OldToNewAsync(Guid id)
	{
		Car product = await _vehicleService.GetVehicleAsync(id);
		if (product != null)
		{
			Id = product.Id;
			BrandName = product.BrandName;
			Category = product.Category;
			Price = product.Price;

		}
	}

	public async Task UpdateVehicleAsync()
	{
		if (!string.IsNullOrWhiteSpace(BrandName)
			&& Price >= 0)
		{
			await _vehicleService.UpdateVehicleAsync(Id, BrandName, Category, Price);
		}
	}
}
