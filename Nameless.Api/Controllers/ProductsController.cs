using Microsoft.AspNetCore.Mvc;
using Nameless.Domain.Services;

namespace Nameless.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductsController : ControllerBase
{
    private IProductService _productService;

    public ProductsController(IProductService productService)
    {
        this._productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAll();
        
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productService.Get(id);
        
        if (product is null)
        {
            return NotFound();
        }
        
        return Ok(product);
    }
}


