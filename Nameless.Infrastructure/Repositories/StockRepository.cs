using Dapper;
using Nameless.Domain.Dto;
using Nameless.Domain.Entities;
using Nameless.Domain.Helpers;
using Nameless.Domain.Repositories;

namespace Nameless.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public StockRepository(IConnectionFactory connectionFactory)
    {
        this._connectionFactory = connectionFactory;
    }
    
    public async Task<IEnumerable<Stock>> GetAll()
    {
        var query = @"SELECT
                        s.id,
                        s.stock_count AS StockCount,
                        ps.size AS ProductSize,
                        c.colour AS ProductColour,
                        ct.target AS ProductTarget
                    FROM stock s
                    JOIN product_size ps on ps.id = s.product_size_id
                    JOIN colour c on s.product_colour_id = c.id
                    JOIN customer_target ct on s.customer_target_id = ct.id";
        
        using (var conn = this._connectionFactory.GetConnection())
        {
            var result = await conn.QueryAsync<Stock>(query);

            return result.ToList();
        }
    }

    public async Task<Stock?> Get(int id)
    {
        var query = @"SELECT
                        s.id,
                        s.stock_count AS StockCount,
                        ps.size AS ProductSize,
                        c.colour AS ProductColour,
                        ct.target AS ProductTarget
                    FROM stock s
                    JOIN product_size ps on ps.id = s.product_size_id
                    JOIN colour c on s.product_colour_id = c.id
                    JOIN customer_target ct on s.customer_target_id = ct.id
                    WHERE s.id = @id";

        using (var conn = this._connectionFactory.GetConnection())
        {
            var result = await conn.QueryAsync<Stock>(query, new { id = id });

            return result.FirstOrDefault();
        }
    }
    
    public async Task<IEnumerable<Stock>> GetByProduct(int productId)
    {
        var query = @"SELECT
                        s.id,
                        s.stock_count AS StockCount,
                        ps.size AS ProductSize,
                        c.colour AS ProductColour,
                        ct.target AS ProductTarget
                    FROM stock s
                    JOIN product_size ps on ps.id = s.product_size_id
                    JOIN colour c on s.product_colour_id = c.id
                    JOIN customer_target ct on s.customer_target_id = ct.id
                    WHERE s.product_id = @productId;";

        using (var conn = this._connectionFactory.GetConnection())
        {
            var result = await conn.QueryAsync<Stock>(query, new { productId = productId });

            return result.ToList();
        }
    }

    public async Task<Stock> Create(Stock newProduct)
    {
        throw new NotImplementedException();
    }

    public async Task<Stock> Update(Stock updatedProduct)
    {
        return await Task.Run(() => updatedProduct);
    }

    public async Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}