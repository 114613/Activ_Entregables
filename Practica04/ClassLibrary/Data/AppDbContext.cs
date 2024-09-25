using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<DetalleTurno> DetalleTurnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Turno>()
                .HasMany(t => t.Detalles)
                .WithOne(d => d.Turno)
                .HasForeignKey(d => d.Turno.Id);
        }
    }
}
