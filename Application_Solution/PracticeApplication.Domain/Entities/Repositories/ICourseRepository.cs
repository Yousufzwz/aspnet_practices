using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeApplication.Domain.Entities.Repositories;

public interface ICourseRepository: IRepositoryBase<Course, Guid>
{
    Task<bool> IsTitleDuplicate(string title, Guid? id = null);

    Task<(IList<Course> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy,
                int pageIndex, int pageSize);
}
