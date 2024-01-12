using Exam1.Application;
using Exam1.Domain.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public ICarRepository CarRepository { get; private set; }

    public ApplicationUnitOfWork(ICarRepository carRepository,
        IApplicationDbContext dbContext) : base((DbContext)dbContext)
    {
        CarRepository = carRepository;
    }

}