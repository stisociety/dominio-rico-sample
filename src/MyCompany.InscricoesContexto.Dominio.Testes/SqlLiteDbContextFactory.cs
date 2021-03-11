using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCompany.InscricoesContexto.Infraestrutura;

namespace MyCompany.InscricoesContexto.Dominio.Testes
{
    public sealed class SqlLiteDbContextFactory :  IDisposable
    {
        private bool _disposed = false;
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        public SqlLiteDbContextFactory()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
        }

        public async Task<InscricoesDbContext> CriarAsync()
        {
            var options = new DbContextOptionsBuilder<InscricoesDbContext>()
                .UseLoggerFactory(GetLoggerFactory())
                .EnableSensitiveDataLogging()
                .UseSqlite(_connection)
                .Options;
            var contexto = new InscricoesDbContext(options);
            await contexto.Database.EnsureCreatedAsync();
            return contexto;
        }

        private ILoggerFactory GetLoggerFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddConsole()
                .AddDebug()
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _connection.Close();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}