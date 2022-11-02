namespace Nameless.Domain.Entities;

public class Stock : BaseModel
{
    public int StockCount { get; set; }
    public string ProductSize { get; set; }
    public string ProductColour { get; set; }
    public string ProductTarget { get; set; }
}