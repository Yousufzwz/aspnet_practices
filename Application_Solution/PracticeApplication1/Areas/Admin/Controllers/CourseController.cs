using Microsoft.AspNetCore.Mvc;

namespace PracticeApplication1.Areas.Admin.Controllers;

[Area ("Admin")]
public class CourseController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }
}
