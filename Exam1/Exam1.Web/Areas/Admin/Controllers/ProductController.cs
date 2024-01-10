using Autofac;
using Exam1.Domain.Exceptions;
using Exam1.Infrastructure;
using Exam1.Web.Areas.Admin.Models;
using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{

    private readonly ILifetimeScope _scope;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILifetimeScope scope,
   ILogger<ProductController> logger)
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
        var model = _scope.Resolve<ProductInsertModel>();
        return View(model);
    }


    [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInsertModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.IncludeProductAsync();
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Product added successfuly",
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
                        Message = "There was a problem to inserting product",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }

    public async Task<JsonResult> GetProducts()
    {
        var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
        var model = _scope.Resolve<ProductListModel>();

        var data = await model.GetPagedContentsAsync(dataTablesModel);
        return Json(data);
    }


    public async Task<IActionResult> Update(Guid id)
    {
        var model = _scope.Resolve<ProductUpdateModel>();
        await model.LoadAsync(id);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProductUpdateModel model)
    {
        model.Resolve(_scope);

        if (ModelState.IsValid)
        {
            try
            {
             //newly done
				await model.UpdateProductAsync(); TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Product updated successfuly!",
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
                    Message = "There was a problem in updating the product!",
                    Type = ResponseTypes.Danger
                });
            }
        }

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var model = _scope.Resolve<ProductListModel>();

        if (ModelState.IsValid)
        {
            try
            {
                await model.DeleteProductAsync(id); TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Product removed successfuly!",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Server Error");

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "There was a problem in removing your product!",
                    Type = ResponseTypes.Danger
                });
            }
        }
        return RedirectToAction("Index");
    }



}
