using Autofac;
using Exam1.Domain.Features.AccessStore;

namespace Exam1.Web.Areas.Admin.Models;

public class ProductInsertModel
{
    private ILifetimeScope _scope;
    private IProductManagementService _productManagementService;
    public string Name { get; set; }
    public double Price { get; set; }
    public double Weight { get; set; }


    public ProductInsertModel() { }


    public ProductInsertModel(IProductManagementService productManagementService)
    {
        _productManagementService = productManagementService;
    }

    internal void Resolve(ILifetimeScope scope)
    {
        _scope = scope;
        _productManagementService = _scope.Resolve<IProductManagementService>();
    }

    internal async Task IncludeProductAsync()
    {
        await _productManagementService.IncludeProductAsync(Name, Price, Weight);
    }
}
