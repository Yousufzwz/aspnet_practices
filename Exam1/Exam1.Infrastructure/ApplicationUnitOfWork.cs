using Exam1.Domain.Entities.Repositories;
using FirstDemo.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
	public IBookRepository BookRepository { get; private set; }

	public ApplicationUnitOfWork(IBookRepository bookRepository,
		IApplicationDbContext dbContext) : base((DbContext)dbContext)
	{
		BookRepository = bookRepository;
	}

}
