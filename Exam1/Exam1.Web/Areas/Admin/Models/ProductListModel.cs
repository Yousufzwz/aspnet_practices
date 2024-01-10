using Exam1.Domain.Features.AccessStore;
using Exam1.Infrastructure;
using System.Web;

namespace Exam1.Web.Areas.Admin.Models;

public class ProductListModel
{
    private IProductManagementService _productManagementService;

    public ProductListModel()
    {
    }

    public ProductListModel(IProductManagementService productManagementService)
    {
        _productManagementService = productManagementService;
    }

    public async Task<object> GetPagedContentsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
    {
        var data = await _productManagementService.GetPagedContentsAsync(
            dataTablesUtility.PageIndex,
            dataTablesUtility.PageSize,
            dataTablesUtility.SearchText,
            dataTablesUtility.GetSortText(new string[] { "Name", "Price", "Weight" }));

        return new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = (from record in data.records
                    select new string[]
                    {
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.Price),
                                record.Weight.ToString(),
                                record.Id.ToString()
                    }
                ).ToArray()
        };
    }

    internal async Task DeleteProductAsync(Guid id)
    {
        await _productManagementService.DeleteProductAsync(id);
    }


}