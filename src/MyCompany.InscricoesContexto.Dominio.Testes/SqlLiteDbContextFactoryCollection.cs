using MyCompany.InscricoesContexto.Infraestrutura;
using Xunit;

namespace MyCompany.InscricoesContexto.Dominio.Testes
{
    [CollectionDefinition(nameof(InscricoesDbContext))]
    public class SqlLiteDbContextFactoryCollection : ICollectionFixture<SqlLiteDbContextFactory> { }
}