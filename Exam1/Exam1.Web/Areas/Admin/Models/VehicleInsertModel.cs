using Autofac;
using Exam1.Application;

namespace Exam1.Web.Areas.Admin.Models;

public class VehicleInsertModel
{
    private ILifetimeScope _scope;
    private IVehicleManagementService _vehicleManagementService;
    public string BrandName { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }


    public VehicleInsertModel() { }


    public VehicleInsertModel(IVehicleManagementService vehicleManagementService)
    {
        _vehicleManagementService = vehicleManagementService;
    }

    public void Resolve(ILifetimeScope scope)
    {
        _scope = scope;
        _vehicleManagementService = _scope.Resolve<IVehicleManagementService>();
    }

    public async Task InsertVehicleAsync()
    {
        await _vehicleManagementService.InsertVehicleAsync(BrandName, Category, Price);
    }


}
