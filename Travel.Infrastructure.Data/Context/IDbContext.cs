using Microsoft.EntityFrameworkCore;

namespace Travel.Infrastructure.Data.Context
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();

        void Dispose();
    }
}