using Exam1.Domain.Entities;
using FirstDemo.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features.AccessStore;

public class LibraryManagementService : ILibraryManagementService
{
	private readonly IApplicationUnitOfWork _unitOfWork;
	public LibraryManagementService(IApplicationUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task InsertBookAsync(string name, string category, double price)
	{

		bool isDuplicateTitle = await _unitOfWork.BookRepository.
		   IsNameDuplicateAsync(name);

		if (isDuplicateTitle)
			throw new DuplicateNameException(); ;

		Book product = new Book
		{
			Name = name,
			Category= category,
			Price= price
		};

		_unitOfWork.BookRepository.Add(product);
		await _unitOfWork.SaveAsync();
	}

    public async Task<(IList<Book> records, int total, int totalDisplay)> GetPagedContentsAsync(int pageIndex, int pageSize, string searchText, string sortBy)
    {
        return await _unitOfWork.BookRepository.GetTableDataAsync(searchText, sortBy,
            pageIndex, pageSize);
    }

}
