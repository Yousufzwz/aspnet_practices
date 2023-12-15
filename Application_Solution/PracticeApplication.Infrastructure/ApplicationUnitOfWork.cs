using Microsoft.EntityFrameworkCore;
using PracticeApplication.Application;
using PracticeApplication.Domain.Entities.Repositories;
using PracticeApplication.Infrastructure;
using PracticeApplication.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeApplication1.Infrastructure;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public ICourseRepository CourseRepository { get; private set; }

    public ApplicationUnitOfWork(ApplicationDbContext dbContext) : base(dbContext)
    {
        CourseRepository = new CourseRepository(dbContext);
    }

     
    
}
