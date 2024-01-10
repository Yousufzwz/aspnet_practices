namespace Exam1.Domain.Entities;

public class Product : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double Weight { get; set; }
}