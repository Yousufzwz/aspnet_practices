using Exam1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Book> Books { get; set; }
    }
}