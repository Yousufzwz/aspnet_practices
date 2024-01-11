using Autofac;
using Exam1.Infrastructure;
using Exam1.Web.Areas.Admin.Models;
using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Exam1.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class LibraryController : Controller
{
	private readonly ILifetimeScope _scope;
	private readonly ILogger<LibraryController> _logger;

	public LibraryController(ILifetimeScope scope,
	
		ILogger<LibraryController> logger)
	{
		_scope = scope;
		_logger = logger;
	}
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Create()
	{
		var model = _scope.Resolve<BookInsertModel>();
		return View(model);
	}


	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(BookInsertModel model)
	{
		if (ModelState.IsValid)
		{
			try
			{
				model.Resolve(_scope);
				await model.InsertBookAsync();
				TempData.Put("ResponseMessage", new ResponseModel
				{
					Message = "Book inserted successfully!",
					Type = ResponseTypes.Success
				});

				return RedirectToAction("Index");
			}
			catch (DuplicateNameException de)
			{
				TempData.Put("ResponseMessage", new ResponseModel
				{
					Message = de.Message,
					Type = ResponseTypes.Danger
				});
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Server Error");

				TempData.Put("ResponseMessage", new ResponseModel
				{
					Message = "There was a problem to inserting book",
					Type = ResponseTypes.Danger
				});
			}
		}
		return View(model);
	}

	public async Task<JsonResult> GetBooks()
	{
		var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
		var model = _scope.Resolve<BookListModel>();

		var data = await model.GetPagedContentsAsync(dataTablesModel);
		return Json(data);
	}


}
