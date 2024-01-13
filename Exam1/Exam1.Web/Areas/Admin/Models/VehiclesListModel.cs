using Exam1.Application;
using Exam1.Infrastructure;
using System.Web;

namespace Exam1.Web.Areas.Admin.Models;

public class VehiclesListModel
{
    private IVehicleManagementService _vehicleManagementService;

    public VehiclesListModel()
    {
    }

    public VehiclesListModel(IVehicleManagementService vehicleManagementService)
    {
        _vehicleManagementService = vehicleManagementService;
    }

    public async Task<object> GetDataOfVehiclesAsync(DataTablesAjaxRequestUtility dataTablesInfo)
    {
        var data = await _vehicleManagementService.GetDataOfVehiclesAsync(
            dataTablesInfo.PageIndex,
            dataTablesInfo.PageSize,
            dataTablesInfo.SearchText,
            dataTablesInfo.GetSortText(new string[] { "BrandName", "Category", "Price" }));

        return new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = (from record in data.records
                    select new string[]
                    {
                                HttpUtility.HtmlEncode(record.BrandName),
                                HttpUtility.HtmlEncode(record.Category),
                                record.Price.ToString(),
                                record.Id.ToString()
                    }
                ).ToArray()
        };
    }

	public async Task RemoveVehicleAsync(Guid id)
	{
		await _vehicleManagementService.RemoveVehicleAsync(id);
	}
}
