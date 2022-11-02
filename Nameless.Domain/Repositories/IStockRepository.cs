using Nameless.Domain.Entities;

namespace Nameless.Domain.Repositories;

public interface IStockRepository
{
    public Task<IEnumerable<Stock>> GetAll();
    public Task<Stock?> Get(int productId);
    public Task<IEnumerable<Stock>> GetByProduct(int productId);
    public Task<Stock> Create(Stock newProduct);
    public Task<Stock> Update(Stock updatedProduct);
    public Task<bool> Delete(int id);
}