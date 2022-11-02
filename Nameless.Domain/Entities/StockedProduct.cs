namespace Nameless.Domain.Entities;

public class StockedProduct
{
    public Product Product { get; set; }
    public List<Stock> Stocked { get; set; }
}