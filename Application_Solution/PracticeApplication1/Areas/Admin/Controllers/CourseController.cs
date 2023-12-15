using Autofac;
using Microsoft.AspNetCore.Mvc;
using PracticeApplication1.Areas.Admin.Models;

namespace PracticeApplication1.Areas.Admin.Controllers;

[Area ("Admin")]
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
    public IActionResult Create(CourseCreateModel model)
    {
        if(ModelState.IsValid)
        {
            model.Resolve(_scope);
            model.CreateCoures();
            return RedirectToAction("Index");
        }
        return View(model); 
    }
}
