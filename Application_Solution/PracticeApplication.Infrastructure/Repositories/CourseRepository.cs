using FirstDemo.Application;
using Microsoft.EntityFrameworkCore;
using PracticeApplication.Domain.Entities;
using PracticeApplication.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeApplication.Infrastructure.Repositories;

public class CourseRepository : Repository<Course, Guid>, ICourseRepository
{
    public CourseRepository(DbContext context) : base(context)
    {
    }
}
