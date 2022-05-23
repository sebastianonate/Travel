using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;
using Travel.Infrastructure.Data.Context;

namespace Travel.Infrastructure.Data.Repositories
{
    public class EditorialRepository: GenericRepository<Editorial>, IEditorialRepository
    {
        public EditorialRepository(IDbContext context) : base(context)
        {
        }
    }
}