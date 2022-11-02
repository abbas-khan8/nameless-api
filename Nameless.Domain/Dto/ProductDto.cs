using Nameless.Domain.Entities;

namespace Nameless.Domain.Dto;

public class ProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string Brand { get; set; }
    public List<Category> Categories { get; set; }
}