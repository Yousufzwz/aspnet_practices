using Exam1.Domain.Entities;
using Exam1.Domain.Entities.Repositories;
using FirstDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure.Repositories;

public class CarRepository : Repository<Car, Guid>, ICarRepository
{
    public CarRepository(IApplicationDbContext context) : base((DbContext) context)
    { 
        
    }

    public async Task<(IList<Car> records, int total, int totalDisplay)>
      GetTableDataAsync(string searchText, string orderBy,
          int pageIndex, int pageSize)
    {
        return await GetDynamicAsync(x => x.BrandName.Contains(searchText),
            orderBy, null, pageIndex, pageSize, true);
    }

    public async Task<bool> IsVehicleDuplicateNameAsync(string name, Guid? id = null)
    {
        if (id.HasValue)
        {
            return await IsDuplicateWithIdAsync(name, id.Value);
        }
        else
        {
            return await IsDuplicateWithoutIdAsync(name);
        }
    }

    private async Task<bool> IsDuplicateWithIdAsync(string name, Guid id)
    {
        int count = await GetCountAsync(x => x.Id != id && x.BrandName == name);
        return count > 0;
    }

    private async Task<bool> IsDuplicateWithoutIdAsync(string name)
    {
        int count = await GetCountAsync(x => x.BrandName == name);
        return count > 0;
    }
}
