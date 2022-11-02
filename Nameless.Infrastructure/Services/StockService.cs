using Nameless.Domain.Entities;
using Nameless.Domain.Repositories;
using Nameless.Domain.Services;

namespace Nameless.Infrastructure.Services;

public class StockService : IStockService
{
    private IStockRepository _stockRepository;

    public StockService(IStockRepository stockRepository)
    {
        this._stockRepository = stockRepository;
    }

    public async Task<IEnumerable<Stock>> GetAll()
    {
        var stocked = await _stockRepository.GetAll();
        
        return stocked;
    }
    
    public async Task<Stock?> Get(int id)
    {
        var stock = await _stockRepository.Get(id);
        
        return stock;
    }
    
    public async Task<IEnumerable<Stock>> GetByProduct(int productId)
    {
        var stocked = await _stockRepository.GetByProduct(productId);
        
        return stocked;
    }
}