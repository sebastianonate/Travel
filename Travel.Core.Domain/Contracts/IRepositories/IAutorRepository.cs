namespace Travel.Core.Domain.Contracts
{
    public interface IAutorRepository : IGenericRepository<Autor>
    {
        Autor FindAutorWithLibros(int id);
    }
}