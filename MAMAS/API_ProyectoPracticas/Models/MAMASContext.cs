using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_ProyectoPracticas.Models
{
    public class MAMASContext : DbContext
    {
        public MAMASContext()
        {
        }

        public MAMASContext(DbContextOptions<MAMASContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlumnoProfesor> AlumnoProfesor { get; set; }
        public virtual DbSet<Alumnos> Alumnos { get; set; }
        public virtual DbSet<Asignaturas> Asignaturas { get; set; }
        public virtual DbSet<Centros> Centros { get; set; }
        public virtual DbSet<Chats> Chats { get; set; }
        public virtual DbSet<Cursos> Cursos { get; set; }
        public virtual DbSet<Evaluaciones> Evaluaciones { get; set; }
        public virtual DbSet<Mensajes> Mensajes { get; set; }
        public virtual DbSet<Notas> Notas { get; set; }
        public virtual DbSet<Padres> Padres { get; set; }
        public virtual DbSet<Perfiles> Perfiles { get; set; }
        public virtual DbSet<ProfesorAsignatura> ProfesorAsignatura { get; set; }
        public virtual DbSet<ProfesorCurso> ProfesorCurso { get; set; }
        public virtual DbSet<Profesores> Profesores { get; set; }
        public virtual DbSet<TiposChat> TiposChat { get; set; }
        public virtual DbSet<UsuarioChat> UsuarioChat { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MAMAS;Trusted_Connection=True;");
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AlumnoProfesor>(entity =>
            {
                entity.HasKey(e => e.IdAlumnoProfesor);

                entity.Property(e => e.IdAlumnoProfesor).HasColumnName("idAlumnoProfesor");

                entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");

                entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.AlumnoProfesor)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlumnoProfesor_Alumnos");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.AlumnoProfesor)
                    .HasForeignKey(d => d.IdProfesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlumnoProfesor_Profesores");
            });

            modelBuilder.Entity<Alumnos>(entity =>
            {
                entity.HasKey(e => e.IdAlumno);

                entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");

                entity.Property(e => e.ApellidosAlumnos)
                    .IsRequired()
                    .HasColumnName("apellidosAlumnos")
                    .HasMaxLength(50);

                entity.Property(e => e.FechaNacimientoAlumno)
                    .HasColumnName("fechaNacimientoAlumno")
                    .HasColumnType("date");

                entity.Property(e => e.IdPadre).HasColumnName("idPadre");

                entity.Property(e => e.NombreAlumno)
                    .IsRequired()
                    .HasColumnName("nombreAlumno")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPadreNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdPadre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alumnos_Padres");
            });

            modelBuilder.Entity<Asignaturas>(entity =>
            {
                entity.HasKey(e => e.IdAsignatura);

                entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");

                entity.Property(e => e.NombreAsignatura)
                    .IsRequired()
                    .HasColumnName("nombreAsignatura")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Centros>(entity =>
            {
                entity.HasKey(e => e.IdCentro);

                entity.Property(e => e.IdCentro).HasColumnName("idCentro");

                entity.Property(e => e.DireccionCentro)
                    .IsRequired()
                    .HasColumnName("direccionCentro")
                    .HasMaxLength(50);

                entity.Property(e => e.NombreCentro)
                    .IsRequired()
                    .HasColumnName("nombreCentro")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Chats>(entity =>
            {
                entity.HasKey(e => e.IdChat);

                entity.Property(e => e.IdChat).HasColumnName("idChat");

                entity.Property(e => e.IdTipo).HasColumnName("idTipo");

                entity.Property(e => e.NombreChat)
                    .IsRequired()
                    .HasColumnName("nombreChat")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chats_TiposChat");
            });

            modelBuilder.Entity<Cursos>(entity =>
            {
                entity.HasKey(e => e.IdCurso);

                entity.Property(e => e.IdCurso).HasColumnName("idCurso");

                entity.Property(e => e.LetraCurso)
                    .IsRequired()
                    .HasColumnName("letraCurso")
                    .HasMaxLength(1);

                entity.Property(e => e.NumeroCurso).HasColumnName("numeroCurso");
            });

            modelBuilder.Entity<Evaluaciones>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacion);

                entity.Property(e => e.IdEvaluacion).HasColumnName("idEvaluacion");

                entity.Property(e => e.NombreEvaluacion)
                    .IsRequired()
                    .HasColumnName("nombreEvaluacion")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Mensajes>(entity =>
            {
                entity.HasKey(e => e.IdMensaje);

                entity.Property(e => e.IdMensaje).HasColumnName("idMensaje");

                entity.Property(e => e.ContenidoMensaje)
                    .IsRequired()
                    .HasColumnName("contenidoMensaje")
                    .HasMaxLength(1000);

                entity.Property(e => e.FechaMensaje)
                    .HasColumnName("fechaMensaje")
                    .HasColumnType("date");

                entity.Property(e => e.IdChat).HasColumnName("idChat");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdChatNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.IdChat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mensajes_Chats");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mensajes_Usuarios");
            });

            modelBuilder.Entity<Notas>(entity =>
            {
                entity.HasKey(e => e.IdNota);

                entity.Property(e => e.IdNota).HasColumnName("idNota");

                entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");

                entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");

                entity.Property(e => e.IdCurso).HasColumnName("idCurso");

                entity.Property(e => e.IdEvaluacion).HasColumnName("idEvaluacion");

                entity.Property(e => e.Nota)
                    .IsRequired()
                    .HasColumnName("nota")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Notas)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notas_Alumnos");

                entity.HasOne(d => d.IdAsignaturaNavigation)
                    .WithMany(p => p.Notas)
                    .HasForeignKey(d => d.IdAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notas_Asignaturas");

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.Notas)
                    .HasForeignKey(d => d.IdCurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notas_Cursos");

                entity.HasOne(d => d.IdEvaluacionNavigation)
                    .WithMany(p => p.Notas)
                    .HasForeignKey(d => d.IdEvaluacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notas_Evaluaciones");
            });

            modelBuilder.Entity<Padres>(entity =>
            {
                entity.HasKey(e => e.IdPadre);

                entity.Property(e => e.IdPadre).HasColumnName("idPadre");

                entity.Property(e => e.ApellidosPadre)
                    .IsRequired()
                    .HasColumnName("apellidosPadre")
                    .HasMaxLength(50);

                entity.Property(e => e.DireccionPadre)
                    .IsRequired()
                    .HasColumnName("direccionPadre")
                    .HasMaxLength(50);

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.NombrePadre)
                    .IsRequired()
                    .HasColumnName("nombrePadre")
                    .HasMaxLength(50);

                entity.Property(e => e.TelefonoPadre)
                    .IsRequired()
                    .HasColumnName("telefonoPadre")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Padres)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Padres_Usuarios");
            });

            modelBuilder.Entity<Perfiles>(entity =>
            {
                entity.HasKey(e => e.IdPerfil);

                entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");

                entity.Property(e => e.NombrePerfil)
                    .IsRequired()
                    .HasColumnName("nombrePerfil")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProfesorAsignatura>(entity =>
            {
                entity.HasKey(e => e.IdProfesorAsignatura);

                entity.Property(e => e.IdProfesorAsignatura).HasColumnName("idProfesorAsignatura");

                entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");

                entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");

                entity.HasOne(d => d.IdAsignaturaNavigation)
                    .WithMany(p => p.ProfesorAsignatura)
                    .HasForeignKey(d => d.IdAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfesorAsignatura_Asignaturas");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.ProfesorAsignatura)
                    .HasForeignKey(d => d.IdProfesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfesorAsignatura_Profesores");
            });

            modelBuilder.Entity<ProfesorCurso>(entity =>
            {
                entity.HasKey(e => e.IdProfesorCurso);

                entity.Property(e => e.IdProfesorCurso).HasColumnName("idProfesorCurso");

                entity.Property(e => e.IdCurso).HasColumnName("idCurso");

                entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.ProfesorCurso)
                    .HasForeignKey(d => d.IdCurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfesorCurso_Cursos");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.ProfesorCurso)
                    .HasForeignKey(d => d.IdProfesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfesorCurso_Profesores");
            });

            modelBuilder.Entity<Profesores>(entity =>
            {
                entity.HasKey(e => e.IdProfesor);

                entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");

                entity.Property(e => e.ApellidosProfesor)
                    .IsRequired()
                    .HasColumnName("apellidosProfesor")
                    .HasMaxLength(50);

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.NombreProfesor)
                    .IsRequired()
                    .HasColumnName("nombreProfesor")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Profesores)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profesores_Usuarios");
            });

            modelBuilder.Entity<TiposChat>(entity =>
            {
                entity.HasKey(e => e.IdTipo);

                entity.Property(e => e.IdTipo).HasColumnName("idTipo");

                entity.Property(e => e.NombreTipo)
                    .IsRequired()
                    .HasColumnName("nombreTipo")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UsuarioChat>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioChat);

                entity.Property(e => e.IdUsuarioChat).HasColumnName("idUsuarioChat");

                entity.Property(e => e.IdChat).HasColumnName("idChat");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdChatNavigation)
                    .WithMany(p => p.UsuarioChat)
                    .HasForeignKey(d => d.IdChat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioChat_Chats");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioChat)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioChat_Usuarios");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");

                entity.Property(e => e.PassUsuario)
                    .IsRequired()
                    .HasColumnName("passUsuario")
                    .HasMaxLength(50);

                entity.Property(e => e.UsernameUsuario)
                    .IsRequired()
                    .HasColumnName("usernameUsuario")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuarios_Perfiles");
            });
        }
    }
}
