using Microsoft.EntityFrameworkCore;
using Nitaj.Domain.Entities;

namespace Nitaj.Infrastructure.Context
{
    public class NitajDbContext : DbContext
    {
        public NitajDbContext(DbContextOptions<NitajDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
