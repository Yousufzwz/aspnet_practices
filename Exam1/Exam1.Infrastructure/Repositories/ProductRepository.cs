using Exam1.Domain.Entities;
using Exam1.Domain.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure.Repositories;

public class ProductRepository : Repository<Product, Guid>, IProductRepository
{
    public ProductRepository(IApplicationDbContext context) : base((DbContext)context)
    {
    }

    public async Task<bool> IsNameDuplicateAsync(string name, Guid? id = null)
    {
        if (id.HasValue)
        {
            return (await GetCountAsync(x => x.Id != id.Value && x.Name == name)) > 0;
        }
        else
        {
            return (await GetCountAsync(x => x.Name == name)) > 0;
        }
    }

    public async Task<(IList<Product> records, int total, int totalDisplay)>
       GetTableDataAsync(string searchText, string orderBy,
           int pageIndex, int pageSize)
    {
        return await GetDynamicAsync(x => x.Name.Contains(searchText),
            orderBy, null, pageIndex, pageSize, true);
    }

}
