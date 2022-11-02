using Nameless.Domain.Entities;

namespace Nameless.Domain.Services;

public interface IStockService
{
    public Task<IEnumerable<Stock>> GetAll();
    public Task<Stock?> Get(int id);
    public Task<IEnumerable<Stock>> GetByProduct(int productId);
}