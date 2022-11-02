using Microsoft.AspNetCore.Mvc;
using Nameless.Domain.Services;

namespace Nameless.Api.Controllers;

[ApiController]
[Route("api/stock")]
public class StockController : ControllerBase
{
    private IStockService _stockService;
    
    private IProductService _productService;

    public StockController(IStockService stockService, IProductService productService)
    {
        this._stockService = stockService;
        this._productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocked = await _stockService.GetAll();
        
        return Ok(stocked);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var stock = await _stockService.Get(id);
        
        if (stock is null)
        {
            return NotFound();
        }
        
        return Ok(stock);
    }
    
    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        var product = await _productService.Get(productId);

        if (product is null)
        {
            return NotFound();
        }
        
        var stocked = await _stockService.GetByProduct(product.Id);
        
        return Ok(stocked);
    }
}


