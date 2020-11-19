using Microsoft.EntityFrameworkCore;
using ProvaOA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaOA.Server
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Professor> Professores { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<ProfCurso> ProfessorCurso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Muitos para muitos
            modelBuilder.Entity<ProfCurso>()
                .HasKey(pc => new { pc.ProfessorId, pc.CursoId });

            modelBuilder.Entity<ProfCurso>()
                .HasOne(pc => pc.Curso)
                .WithMany(c => c.Professores)
                .HasForeignKey(pc => pc.CursoId);

            modelBuilder.Entity<ProfCurso>()
                .HasOne(pc => pc.Professor)
                .WithMany(p => p.Cursos)
                .HasForeignKey(pc => pc.ProfessorId);

            //Um para muitos
            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Alunos)
                .HasForeignKey(a => a.CursoId);

            //Um para um
            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Professor)
                .WithOne(p => p.Orientando)
                .HasForeignKey<Aluno>(a => a.ProfessorId);
        }
    }
}
