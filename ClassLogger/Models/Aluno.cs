using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassLogger.Models
{
    [Table("Alunos")]
    public class Aluno
    {
        [Required]
        [Key, ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Curso")]
        public int? CursoId { get; set; }
        public virtual Curso Curso { get; set; }

        [ForeignKey("Periodo")]
        public int? PeriodoId { get; set; }
        public virtual Periodo Periodo { get; set; }

        public virtual ICollection<Turma> Turmas { get; set; }
        public virtual ICollection<RegistroDeAluno> RegistrosDeAluno { get; set; }
        public virtual ICollection<Boletim> Boletins { get; set; }
    }
}