using Autofac;
using Exam1.Domain.Entities;
using Exam1.Domain.Features.AccessStore;
using System.ComponentModel.DataAnnotations;

namespace Exam1.Web.Areas.Admin.Models;

public class ProductUpdateModel
{

    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required, Range(0, 10000, ErrorMessage = "Product price range should be 0 & 10000")]
    public double Price { get; set; }
    public double Weight { get; set; }

    private IProductManagementService _productService;

    public ProductUpdateModel()
    {

    }

    public ProductUpdateModel(IProductManagementService productService)
    {
        _productService = productService;
    }

    internal void Resolve(ILifetimeScope scope)
    {
        _productService = scope.Resolve<IProductManagementService>();
    }

    internal async Task LoadAsync(Guid id)
    {
        Product content = await _productService.GetProductAsync(id);
        if (content != null)
        {
            Id = content.Id;
            Name = content.Name;
            Price = content.Price;
            Weight = content.Weight;
        }
    }

    internal async Task UpdateProductAsync()
    {
        if (!string.IsNullOrWhiteSpace(Name)
            && Price >= 0)
        {
            await _productService.UpdateProductAsync(Id, Name, Price, Weight);
        }
    }
}