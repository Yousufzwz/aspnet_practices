using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
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
        Car car = new Car()
        {
            BrandName = brandName,
            Category = category,
            Price = price
        };

        _unitOfWork.CarRepository.Add(car);
        await _unitOfWork.SaveAsync();

    }


}
