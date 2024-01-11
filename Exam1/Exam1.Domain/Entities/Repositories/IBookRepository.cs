using Exam1.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Entities.Repositories;

public interface IBookRepository : IRepositoryBase<Book, Guid>
{
	Task<bool> IsNameDuplicateAsync(string title, Guid? id = null);

    Task<(IList<Book> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy,
                int pageIndex, int pageSize);
}
