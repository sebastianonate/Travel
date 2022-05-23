using System.Collections.Generic;

namespace Travel.Core.Domain.Contracts
{
    public interface ILibroRepository : IGenericRepository<Libro>
    {
        Libro FindWithIncludes(int id);
    }
}