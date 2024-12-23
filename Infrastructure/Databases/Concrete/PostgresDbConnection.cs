using Microsoft.Extensions.Configuration;
using Infrastructure.Databases.Abstract;
using System.Data;
using Dapper;
using Npgsql;


namespace Infrastructure.Databases.Concrete;

public class PostgresDbConnection : IDatabase
{
    private readonly Lazy<IDbConnection> _connection;
    public PostgresDbConnection(IConfiguration configuration, string optionsSection = "NpgSqlConnection")
    {
        // Enable snake_case to PascalCase mapping
        DefaultTypeMap.MatchNamesWithUnderscores = true;


        //Where to catch this Exception? 
        var connectionString = configuration.GetConnectionString(optionsSection)
                    ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null. Check your connection string");
        _connection = new Lazy<IDbConnection>(() => new NpgsqlConnection(connectionString));
    }

    public IDbConnection CreateConnection()
    {
        return _connection.Value;
    }
}
