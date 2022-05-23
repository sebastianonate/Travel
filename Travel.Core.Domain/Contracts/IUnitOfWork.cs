using System;
using System.Threading;
using System.Threading.Tasks;

namespace Travel.Core.Domain.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        int Commit(CancellationToken cancellationToken);
        
        Task Rollback();

        IAutorRepository AutorRepository { get; }
        ILibroRepository LibroRepository { get; }
        IEditorialRepository EditorialRepository { get; }
    }
}