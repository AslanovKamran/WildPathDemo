using System.Data;

namespace Infrastructure.Databases.Abstract;
public interface IDatabase
{
    IDbConnection CreateConnection();
}
