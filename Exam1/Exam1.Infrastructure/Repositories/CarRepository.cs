using Exam1.Domain.Entities;
using Exam1.Domain.Entities.Repositories;
using FirstDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure.Repositories;

public class CarRepository : Repository<Car, Guid>, ICarRepository
{
    public CarRepository(IApplicationDbContext context) : base((DbContext) context)
    { 
        
    }
}
