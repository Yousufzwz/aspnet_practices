using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application;

public interface IVehicleManagementService
{
    Task InsertVehicleAsync(string brandName, string category, double price);
}
