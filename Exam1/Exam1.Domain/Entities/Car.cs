using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Entities;

public class Car : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string BrandName { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
}
