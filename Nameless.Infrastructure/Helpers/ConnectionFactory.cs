using System.Data;
using Microsoft.Extensions.Configuration;
using Nameless.Domain.Helpers;
using Npgsql;

namespace Nameless.Infrastructure.Helpers;

public class ConnectionFactory : IConnectionFactory
{     
    private readonly string? _connectionString;        

    public ConnectionFactory(IConfiguration configuration)
    {
        this._connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(this._connectionString);            
    }
}

