using Exam1.Application;
using Exam1.Domain.Exceptions;
using Exam1.Domain.Features.AccessStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Entities;

public class ProductManagementService : IProductManagementService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    public ProductManagementService(IApplicationUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task IncludeProductAsync(string name, double price, double weight)
    {

        bool isDuplicateTitle = await _unitOfWork.ProductRepository.
           IsNameDuplicateAsync(name);

        if (isDuplicateTitle)
            throw new DuplicateNameException(); ;

        Product product = new Product
        {
            Name = name,
            Price = price,
            Weight = weight
        };

        _unitOfWork.ProductRepository.Add(product);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _unitOfWork.ProductRepository.RemoveAsync(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Product> GetProductAsync(Guid id)
    {
        return await _unitOfWork.ProductRepository.GetByIdAsync(id);
    }


    public async Task UpdateProductAsync(Guid id, string name, double price,
           double weight)
    {
        bool isDuplicatTitle = await _unitOfWork.ProductRepository.
                IsNameDuplicateAsync(name, id);

        if (isDuplicatTitle)
            throw new DuplicateNameException();

        var product = await GetProductAsync(id);
        if (product is not null)
        {
            product.Name = name;
            product.Price = price;
            product.Weight = weight;
        }
        await _unitOfWork.SaveAsync();
    }

    public async Task<(IList<Product> records, int total, int totalDisplay)> GetPagedContentsAsync(int pageIndex, int pageSize, string searchText, string sortBy)
    {
        return await _unitOfWork.ProductRepository.GetTableDataAsync(searchText, sortBy,
            pageIndex, pageSize);
    }

}
