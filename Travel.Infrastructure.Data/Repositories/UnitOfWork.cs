using System.Threading;
using System.Threading.Tasks;
using Travel.Core.Domain.Contracts;
using Travel.Infrastructure.Data.Context;

namespace Travel.Infrastructure.Data.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly IDbContext _context;
        public UnitOfWork(IDbContext context) => _context = context;
        
        public int Commit(CancellationToken cancellationToken)
        {
            return _context.SaveChanges();
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        private IAutorRepository _autorRepository;
        public IAutorRepository AutorRepository { get { return _autorRepository ??= new AutorRepository(_context); } }

        private ILibroRepository _libroRepository;
        public ILibroRepository LibroRepository { get { return _libroRepository ??= new LibroRepository(_context); } }

        private IEditorialRepository _editorialRepository;
        public IEditorialRepository EditorialRepository { get { return _editorialRepository ??= new EditorialRepository(_context); } }
    }
}