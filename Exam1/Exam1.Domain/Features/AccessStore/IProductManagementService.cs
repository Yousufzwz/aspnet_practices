using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Features.AccessStore;

public interface IProductManagementService
{
    Task IncludeProductAsync(string name, double price, double weight);

    Task UpdateProductAsync(Guid id, string name, double price, double weight);
    Task DeleteProductAsync(Guid id);
    Task<Product> GetProductAsync(Guid id);
    Task<(IList<Product> records, int total, int totalDisplay)>
        GetPagedContentsAsync(int pageIndex, int pageSize, string searchText, string sortBy);
}
