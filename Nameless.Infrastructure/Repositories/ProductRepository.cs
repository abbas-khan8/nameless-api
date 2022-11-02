using Dapper;
using Nameless.Domain.Entities;
using Nameless.Domain.Helpers;
using Nameless.Domain.Repositories;

namespace Nameless.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public ProductRepository(IConnectionFactory connectionFactory)
    {
        this._connectionFactory = connectionFactory;
    }
    
    public async Task<IEnumerable<Product>> GetAll()
    {
        var query = @"SELECT
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Description,
                        b.brand_name as Brand,
                        c.Id,
                        c.Name
                    FROM product p
                    JOIN brand b on p.brand_id = b.id
                    JOIN product_category pc on P.Id = pc.product_id
                    JOIN category c on c.id = pc.category_id";

        using (var conn = this._connectionFactory.GetConnection())
        {
            var productDict = new Dictionary<int, Product>();

            var result = await conn.QueryAsync<Product, Category, Product>(
                query,
                (product, category) =>
                {
                    Product? productEntry;

                    if (!productDict.TryGetValue(product.Id, out productEntry))
                    {
                        productEntry = product;
                        productEntry.Categories = new List<Category>();
                        productDict.Add(productEntry.Id, productEntry);
                    }

                    if (category is not null)
                    {
                        productEntry.Categories.Add(category);
                    }

                    return productEntry;
                },
                splitOn: "Id");

            return result.Distinct().ToList();
        }
    }

    public async Task<Product?> Get(int id)
    {
        var query = @"SELECT
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Description,
                        b.brand_name as Brand,
                        c.Id,
                        c.Name
                    FROM product p
                    JOIN brand b on p.brand_id = b.id
                    JOIN product_category pc on P.Id = pc.product_id
                    JOIN category c on c.id = pc.category_id
                    WHERE p.id = @id";
        
        using (var conn = this._connectionFactory.GetConnection())
        {
            var productDict = new Dictionary<int, Product>();   
            
            var result = await conn.QueryAsync<Product, Category, Product>(
                query,
                (product, category) =>
                {
                    Product? productEntry;
                        
                    if (!productDict.TryGetValue(product.Id, out productEntry))
                    {
                        productEntry = product;
                        productEntry.Categories = new List<Category>();
                        productDict.Add(productEntry.Id, productEntry);
                    }
                    if (category is not null)
                    {
                        productEntry.Categories.Add(category);
                    }

                    return productEntry;
                },
                splitOn: "Id",
                param: new {Id = id});
                
            return result.FirstOrDefault();
        }
    }

    public async Task<Product> Create(Product newProduct)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> Update(Product updatedProduct)
    {
        return await Task.Run(() => updatedProduct);
    }

    public async Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}