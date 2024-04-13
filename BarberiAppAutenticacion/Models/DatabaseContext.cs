using BarberiApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberiApp.WebApi.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario>? Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Usuario");
                entity.Property(e => e.UsuarioID).HasColumnName("Id");
                entity.Property(e => e.Email).HasMaxLength(180).IsUnicode(false);
                entity.Property(e => e.Alias).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.Contraseña).HasMaxLength(30).IsUnicode(false);
                entity.Property(e => e.RolId).HasColumnName("rol_id").HasMaxLength(1).IsUnicode(false);
            });
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}