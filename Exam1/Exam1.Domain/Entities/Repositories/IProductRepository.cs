using Exam1.Domain.Repositories;

namespace Exam1.Domain.Entities.Repositories;

public interface IProductRepository : IRepositoryBase<Product, Guid>
{
    Task<bool> IsNameDuplicateAsync(string title, Guid? id = null);

    Task<(IList<Product> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy,
                int pageIndex, int pageSize);
}