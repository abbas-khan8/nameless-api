using Nameless.Domain.Entities;
using Nameless.Domain.Repositories;
using Nameless.Domain.Services;

namespace Nameless.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    private readonly IStockService _stockService;

    public ProductService(IProductRepository productRepository, IStockService stockService)
    {
        this._productRepository = productRepository;
        this._stockService = stockService;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = await _productRepository.GetAll();

        if (products.Any())
        {
            foreach (var product in products)
            {
                var stock = await _stockService.GetByProduct(product.Id);
                product.Stocked = stock.ToList();
            }
        }
        
        return products;
    }
    
    public async Task<Product?> Get(int id)
    {
        var product = await _productRepository.Get(id);
        
        if (product is not null)
        {
            var stock = await _stockService.GetByProduct(product.Id);
            product.Stocked = stock.ToList();
        }
        
        return product;
    }
}