namespace Nameless.Domain.Entities;

public class Product : BaseModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string Brand { get; set; }    
    public List<Stock> Stocked { get; set; }
    public List<Category> Categories { get; set; }
}


