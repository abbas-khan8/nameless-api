using System.Data;

namespace Nameless.Domain.Helpers;

public interface IConnectionFactory
{
    public IDbConnection GetConnection();
}
