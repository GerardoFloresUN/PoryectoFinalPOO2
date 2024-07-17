using Dermatologia.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dermatologia
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Doctor> Doctores { get; set; }

        public DbSet<Cita> Citas { get; set; }

        public DbSet<Producto> Productos { get; set; }

    }
}