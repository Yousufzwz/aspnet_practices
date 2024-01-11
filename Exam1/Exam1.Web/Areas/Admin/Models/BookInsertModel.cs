using Autofac;
using Exam1.Application;

namespace Exam1.Web.Areas.Admin.Models;

public class BookInsertModel
{
	private ILifetimeScope _scope;
	private ILibraryManagementService _libraryManagementService;
	public string Name { get; set; }
	public string Category { get; set; }
	public double Price { get; set; }


	public BookInsertModel() { }


	public BookInsertModel(ILibraryManagementService libraryManagementService)
	{
		_libraryManagementService = libraryManagementService;
	}

	internal void Resolve(ILifetimeScope scope)
	{
		_scope = scope;
		_libraryManagementService = _scope.Resolve<ILibraryManagementService>();
	}

	internal async Task InsertBookAsync()
	{
		await _libraryManagementService.InsertBookAsync(Name, Category, Price);
	}
}
