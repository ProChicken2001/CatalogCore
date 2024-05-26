using Google.Protobuf.WellKnownTypes;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcCatalogCoreServer.Models
{
    public class CatalogCoreDbContext : DbContext
    {
        public CatalogCoreDbContext(DbContextOptions<CatalogCoreDbContext> options) : base(options) { }

        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Materiales> Materiales { get; set; }
        public DbSet<TipoUsuarios> TipoUsuarios { get; set; }
        public DbSet<RolEmpleados> RolEmpleados { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proveedores>().HasKey(p => p.ProveedorId);
            modelBuilder.Entity<Categorias>().HasKey(c => c.CategoriaId);
            modelBuilder.Entity<Materiales>().HasKey(m => m.MaterialId); 
            modelBuilder.Entity<TipoUsuarios>().HasKey(tu => tu.TipoUsuarioId);
            modelBuilder.Entity<RolEmpleados>().HasKey(r => r.RolId);
            modelBuilder.Entity<Usuarios>().HasKey(u => u.UsuarioId);
            modelBuilder.Entity<Empleados>().HasKey(u => u.EmpleadoId);
            modelBuilder.Entity<Prestamos>().HasKey(p => p.PrestamoId);


            modelBuilder.Entity<Usuarios>().Property(u => u.FechaInscripcion)
                .HasConversion(
                    u => u.ToDateTime(),
                    u => Timestamp.FromDateTime(DateTime.SpecifyKind(u,DateTimeKind.Utc))
                );

            modelBuilder.Entity<Empleados>().Property(e => e.FechaContratacion)
                .HasConversion(
                    e => e.ToDateTime(),
                    e => Timestamp.FromDateTime(DateTime.SpecifyKind(e, DateTimeKind.Utc))
                );

            modelBuilder.Entity<Empleados>().Property(e => e.Salario)
                .HasConversion(
                    e => Convert.ToDecimal(e),
                    e => Convert.ToDouble(e)
                );


            modelBuilder.Entity<Prestamos>().Property(p => p.FechaPrestamo)
                .HasConversion(
                    p => p.ToDateTime(),
                    p => Timestamp.FromDateTime(DateTime.SpecifyKind(p, DateTimeKind.Utc))
                );

            modelBuilder.Entity<Prestamos>().Property(p => p.FechaDevolucionEsperada)
                .HasConversion(
                    p => p.ToDateTime(),
                    p => Timestamp.FromDateTime(DateTime.SpecifyKind(p, DateTimeKind.Utc))
                );

            modelBuilder.Entity<Prestamos>().Property(p => p.FechaDevolucionReal)
                .HasConversion(
                    p => p.ToDateTime(),
                    p => Timestamp.FromDateTime(DateTime.SpecifyKind(p, DateTimeKind.Utc))
                );


            modelBuilder.Entity<Prestamos>().Property(p => p.Penalizaciones)
                .HasConversion(
                    p => Convert.ToDecimal(p),
                    p => Convert.ToDouble(p)
                );

            //modelBuilder.Entity<Usuarios>().Property(u => u.FechaInscripcion)
            //    .HasColumnName("FechaInscripcion")
            //    .HasColumnType("datetime");
            //modelBuilder.Entity<Usuarios>(us =>
            //{
            //    us.HasKey(u => u.UsuarioId);
            //    us.Property(u => u.FechaInscripcion).IsRequired();

            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}
