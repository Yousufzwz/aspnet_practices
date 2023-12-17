using Microsoft.EntityFrameworkCore;
using PracticeApplication.Domain.Entities;

namespace PracticeApplication.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<Course> Courses { get; }
}