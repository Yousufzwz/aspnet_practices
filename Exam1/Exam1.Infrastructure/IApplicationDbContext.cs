using Exam1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exam1.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Book> Books { get; set; }
    }
}