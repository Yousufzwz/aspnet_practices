using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exam1.Application.Features.AccessStore;

public class VehicleManagementService : IVehicleManagementService
{
    private readonly IApplicationUnitOfWork _unitOfWork;

    public VehicleManagementService(IApplicationUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task InsertVehicleAsync(string brandName, string category, double price)
    {
        bool isDuplicateTitle = await _unitOfWork.CarRepository.IsVehicleDuplicateNameAsync(brandName);
        if (isDuplicateTitle)
            throw new DuplicateNameException(); ;

        Car car = new Car()
        {
            BrandName = brandName,
            Category = category,
            Price = price
        };

        _unitOfWork.CarRepository.Add(car);
        await _unitOfWork.SaveAsync();

    }

	public async Task RemoveVehicleAsync(Guid id)
	{
		await _unitOfWork.CarRepository.RemoveAsync(id);
		await _unitOfWork.SaveAsync();
	}

	public async Task<(IList<Car> records, int total, int totalDisplay)> GetDataOfVehiclesAsync(int pageIndex, int pageSize, string searchText, string sortBy)
    {
        return await _unitOfWork.CarRepository.GetTableDataAsync(searchText, sortBy,
            pageIndex, pageSize);
    }


}
