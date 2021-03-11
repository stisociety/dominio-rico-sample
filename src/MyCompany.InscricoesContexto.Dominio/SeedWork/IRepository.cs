namespace MyCompany.InscricoesContexto.Dominio.SeedWork
{
    public interface IRepository<T> where T : IAgreggateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}