using Exam1.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Entities.Repositories;

public interface ICarRepository : IRepositoryBase<Car, Guid>
{

}
