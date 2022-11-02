using Nameless.Domain.Entities;

namespace Nameless.Domain.Services;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetAll();
    public Task<Product?> Get(int id);
}