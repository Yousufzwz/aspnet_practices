using Autofac;
using Microsoft.AspNetCore.Mvc;
using PracticeApplication.Infrastructure;
using PracticeApplication1.Areas.Admin.Models;

namespace PracticeApplication1.Areas.Admin.Controllers;

[Area("Admin")]
public class CourseController : Controller
{
    private readonly ILifetimeScope _scope;
    private readonly ILogger<CourseController> _logger;

    public CourseController(ILifetimeScope scope,
        ILogger<CourseController> logger)
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
        var model = _scope.Resolve<CourseCreateModel>();
        return View(model);
    }


    [HttpPost, ValidateAntiForgeryToken]
    public async Task <IActionResult> Create(CourseCreateModel model)
    {
        if(ModelState.IsValid)
        {
            try
            {
                model.Resolve(_scope);
                await model.CreateCourseAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create course");
            }
        }
        return View(model); 
    }


    public async Task<JsonResult> GetCourses()
    {
        var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
        var model = _scope.Resolve<CourseListModel>();

        var data = await model.GetPagedCoursesAsync(dataTablesModel);
        return Json(data);
    }


    public async Task<IActionResult> Update(Guid id)
    {
        var model = _scope.Resolve<CourseUpdateModel>();
        await model.LoadAsync(id);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(CourseUpdateModel model)
    {
        model.Resolve(_scope);

        if (ModelState.IsValid)
        {
            try
            {
                await model.UpdateCourseAsync();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Server Error");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var model = _scope.Resolve<CourseListModel>();

        if (ModelState.IsValid)
        {
            try
            {
                await model.DeleteCourseAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Server Error");
            }
        }

        return RedirectToAction("Index");
    }
}
