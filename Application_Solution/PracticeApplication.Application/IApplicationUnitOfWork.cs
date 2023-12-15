using PracticeApplication.Domain;
using PracticeApplication.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeApplication.Application;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    ICourseRepository CourseRepository { get; }
}
