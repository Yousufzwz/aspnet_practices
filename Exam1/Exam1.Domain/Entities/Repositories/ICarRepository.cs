using Exam1.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Entities.Repositories;

public interface ICarRepository : IRepositoryBase<Car, Guid>
{
    Task<(IList<Car> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy,
                int pageIndex, int pageSize);
    Task<bool> IsVehicleDuplicateNameAsync(string title, Guid? id = null);
}
