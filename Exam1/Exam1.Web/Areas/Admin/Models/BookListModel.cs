using Exam1.Application;
using Exam1.Infrastructure;
using System.Web;

namespace Exam1.Web.Areas.Admin.Models;

public class BookListModel
{
	private ILibraryManagementService _libraryManagementService;

	public BookListModel()
	{
	}

	public BookListModel(ILibraryManagementService libraryManagementService)
	{
		_libraryManagementService = libraryManagementService;
	}

	public async Task<object> GetPagedContentsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
	{
		var data = await _libraryManagementService.GetPagedContentsAsync(
			dataTablesUtility.PageIndex,
			dataTablesUtility.PageSize,
			dataTablesUtility.SearchText,
			dataTablesUtility.GetSortText(new string[] { "Name", "Category", "Price" }));

		return new
		{
			recordsTotal = data.total,
			recordsFiltered = data.totalDisplay,
			data = (from record in data.records
					select new string[]
					{
								HttpUtility.HtmlEncode(record.Name),
								HttpUtility.HtmlEncode(record.Category),
								record.Price.ToString(),
								record.Id.ToString()
					}
				).ToArray()
		};
	}
}
