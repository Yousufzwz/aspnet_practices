using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application;

public interface ILibraryManagementService
{
	Task InsertBookAsync(string name, string category, double price);

	Task<(IList<Book> records, int total, int totalDisplay)>
		GetPagedContentsAsync(int pageIndex, int pageSize, string searchText, string sortBy);
}
