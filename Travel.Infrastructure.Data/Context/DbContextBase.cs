using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Travel.Infrastructure.Data.Context
{
    public class DbContextBase : DbContext ,IDbContext
    {
        public DbContextBase() 
        {
        }
        public DbContextBase(DbContextOptions options) : base(options)
        {

        }
    }
}