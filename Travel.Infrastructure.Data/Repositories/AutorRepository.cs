using System.Linq;
using Microsoft.EntityFrameworkCore;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;
using Travel.Infrastructure.Data.Context;

namespace Travel.Infrastructure.Data.Repositories
{
    public class AutorRepository: GenericRepository<Autor>, IAutorRepository
    {
        public AutorRepository(IDbContext context) : base(context)
        {
        }


        public Autor FindAutorWithLibros(int id)
        {
            return _db.Set<Autor>().Include(t=>t.Libros).FirstOrDefault(t=>t.Id==id);
        }
    }
}