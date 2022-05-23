using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;
using Travel.Infrastructure.Data.Context;

namespace Travel.Infrastructure.Data.Repositories
{
    public class LibroRepository: GenericRepository<Libro>, ILibroRepository
    {
        public LibroRepository(IDbContext context) : base(context)
        {
        }

        public IEnumerable<Libro> GetAll()
        {
            return _dbset.Include(t => t.Autores).Include(t => t.Editorial).ToList();
        }

        public Libro FindWithIncludes(int id)
        {
            return _dbset.Include(t => t.Autores).Include(t => t.Editorial).FirstOrDefault(t => t.Id == id);
        }
    }
}