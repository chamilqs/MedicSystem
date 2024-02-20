using Microsoft.EntityFrameworkCore;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Core.Domain.Common;
using MedicSystem.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace MedicSystem.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<PruebaDeLaboratorio> PruebasDeLaboratorio { get; set; }
        public DbSet<ResultadoDeLaboratorio> ResultadosDeLaboratorio { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "Default";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "Default";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        
            #region Configuración de la tabla 'Usuario'
            modelBuilder.Entity<Usuario>(entity => {

                // Llave primaria y nombre de la tabla
                entity.ToTable("Usuarios");
                entity.HasKey(fk => fk.Id);

                // Propiedades
                entity.Property(u => u.Nombre)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(u => u.Apellido)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(u => u.NombreUsuario)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.Correo)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.Password)
                      .IsRequired();
                entity.Property(u => u.TipoUsuario)
                      .IsRequired();

                // Relaciones
                entity.HasMany(u => u.Pacientes)
                      .WithOne(u => u.Usuario)
                      .HasForeignKey(g => g.UsuarioId);
                entity.HasMany(u => u.Medicos)
                      .WithOne(u => u.Usuario)
                      .HasForeignKey(u => u.UsuarioId);
                entity.HasMany(u => u.Citas)
                      .WithOne(u => u.Usuario)
                      .HasForeignKey(u => u.UsuarioId);
            });

            #endregion

            #region Configuración de la tabla 'Paciente'
            modelBuilder.Entity<Paciente>(entity =>
            {

                // Llave primaria y nombre de la tabla
                entity.ToTable("Pacientes");
                entity.HasKey(fk => fk.Id);

                // Propiedades
                entity.Property(p => p.Nombre)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(p => p.Apellido)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(p => p.Direccion)
                      .IsRequired()
                      .HasMaxLength(250);
                entity.Property(p => p.Telefono)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(p => p.Cedula)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(p => p.FechaNacimiento)
                      .IsRequired();
                entity.Property(p => p.Fumador)
                      .IsRequired();
                entity.Property(p => p.Alergias)
                      .IsRequired();
                //entity.Property(p => p.Foto)
                //      .IsRequired();

                // Relaciones
                entity.HasOne(p => p.Usuario)
                      .WithMany(p => p.Pacientes)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(p => p.Citas)
                      .WithOne(p => p.Paciente)
                      .HasForeignKey(p => p.PacienteId);
            });

            #endregion

            #region Configuración de la tabla 'Medico'
            modelBuilder.Entity<Medico>(entity =>
            {

                // Llave primaria y nombre de la tabla
                entity.ToTable("Medicos");
                entity.HasKey(fk => fk.Id);

                // Propiedades
                entity.Property(m => m.Nombre)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(m => m.Apellido)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(m => m.Correo)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(m => m.Telefono)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(m => m.Cedula)
                      .IsRequired()
                      .HasMaxLength(50);
                //entity.Property(m => m.Foto)
                //      .IsRequired();

                // Relaciones
                entity.HasOne(m => m.Usuario)
                      .WithMany(m => m.Medicos)
                      .HasForeignKey(m => m.UsuarioId);
                entity.HasMany(g => g.Pacientes)
                      .WithMany(g => g.Medicos)
                      .UsingEntity(j => j.ToTable("MedicosPacientes"));
                entity.HasMany(m => m.Citas)
                      .WithOne(m => m.Medico)
                      .HasForeignKey(m => m.MedicoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Configuración de la tabla 'Cita'

            modelBuilder.Entity<Cita>(entity =>
            {

                // Llave primaria y nombre de la tabla
                entity.ToTable("Citas");
                entity.HasKey(fk => fk.Id);

                // Propiedades
                entity.Property(c => c.Fecha)
                      .IsRequired();
                entity.Property(c => c.Hora)
                      .IsRequired();
                entity.Property(c => c.Causa)
                      .IsRequired();
                entity.Property(c => c.EstadoCita)
                      .IsRequired();

                // Relaciones
                entity.HasOne(c => c.Medico)
                      .WithMany(c => c.Citas)
                      .HasForeignKey(c => c.MedicoId)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(c => c.Usuario)
                      .WithMany(c => c.Citas)
                      .HasForeignKey(c => c.UsuarioId);
                entity.HasMany(c => c.ResultadosDeLaboratorio)
                      .WithMany(c => c.Citas)
                      .UsingEntity(j => j.ToTable("CitasResultadosDeLaboratorio"));
            });
            #endregion

            #region Configuración de la tabla 'PruebaDeLaboratorio'

            modelBuilder.Entity<PruebaDeLaboratorio>(entity =>
            {

                // Llave primaria y nombre de la tabla
                entity.ToTable("PruebasDeLaboratorio");
                entity.HasKey(fk => fk.Id);

                // Propiedades
                entity.Property(p => p.Nombre)
                      .IsRequired()
                      .HasMaxLength(150);

                // Relaciones
                entity.HasMany(p => p.ResultadosDeLaboratorio)
                      .WithOne(p => p.PruebaDeLaboratorio)
                      .HasForeignKey(p => p.PruebaDeLaboratorioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            #endregion

            #region Configuración de la tabla 'ResultadoDeLaboratorio'
            modelBuilder.Entity<ResultadoDeLaboratorio>(entity =>
            {

                // Llave primaria y nombre de la tabla
                entity.ToTable("ResultadosDeLaboratorio");
                entity.HasKey(fk => fk.Id);

                // Propiedades
                entity.Property(r => r.Resultado)
                      .IsRequired()
                      .HasMaxLength(500);
                entity.Property(r => r.EstadoResultado)
                      .IsRequired();

                // Relaciones
                entity.HasOne(r => r.Paciente)
                      .WithMany(r => r.ResultadosDeLaboratorio)
                      .HasForeignKey(r => r.PacienteId)
                      .OnDelete(DeleteBehavior.NoAction);
                //entity.HasMany(r => r.PruebasDeLaboratorio)
                //      .WithOne(r => r.ResultadoDeLaboratorio)
                //      .HasForeignKey(r => r.ResultadoDeLaboratorioId)
                //      .OnDelete(DeleteBehavior.NoAction);
            });


            #endregion
        }
    }
}
