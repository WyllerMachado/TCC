using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClassLogger.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        public string Celular { get; set; }

        public byte[] Foto { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Boletim> Boletins { get; set; }
        public DbSet<Coordenador> Coordenadores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<RegistroDeAluno> RegistrosDeAluno { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        public ApplicationDbContext()
            : base("TCC", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}