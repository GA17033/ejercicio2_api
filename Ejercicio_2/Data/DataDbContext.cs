using Ejercicio_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio_2.Data
{
    public class DataDbContext :DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Producto> producto { get; set; }

        public DbSet<Venta> venta { get; set; }
    }
}
