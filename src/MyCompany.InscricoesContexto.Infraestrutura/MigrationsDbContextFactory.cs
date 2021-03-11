using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyCompany.InscricoesContexto.Infraestrutura
{
    public sealed class MigrationsDbContextFactory : IDesignTimeDbContextFactory<InscricoesDbContext>
    {
        public InscricoesDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("2851e5bd-c99f-4603-bd3b-294fac96e899")
                .AddEnvironmentVariables()
                .Build();
            
            var connstr = config.GetConnectionString("Inscricoes");
            return Create(connstr);
        }

        private InscricoesDbContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                    $"{nameof(connectionString)} is null or empty.",
                    nameof(connectionString));

            var optionsBuilder =
                new DbContextOptionsBuilder<InscricoesDbContext>();
            optionsBuilder.UseSqlServer(connectionString, options => {
                options.MigrationsHistoryTable($"{nameof(InscricoesDbContext)}Migrations");
                options.EnableRetryOnFailure();
            });
            return new InscricoesDbContext(optionsBuilder.Options);
        }
    }
}