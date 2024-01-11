using Exam1.Domain;
using Exam1.Domain.Entities.Repositories;
using Exam1.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IBookRepository BookRepository { get; }
    }
}
