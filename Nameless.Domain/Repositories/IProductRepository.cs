using Nameless.Domain.Entities;

namespace Nameless.Domain.Repositories;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAll();
    public Task<Product?> Get(int id);
    public Task<Product> Create(Product newProduct);
    public Task<Product> Update(Product updatedProduct);
    public Task<bool> Delete(int id);
}