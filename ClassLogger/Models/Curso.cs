using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("Cursos")]
    public class Curso
    {
        [Required, Key]
        public int CursoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [ForeignKey("Coordenador")]
        public string CoordenadorId { get; set; }
        public virtual Coordenador Coordenador { get; set; }

        public virtual ICollection<Periodo> Periodos { get; set; }
        public virtual ICollection<Disciplina> Disciplinas { get; set; }
        public virtual ICollection<Turma> Turmas { get; set; }
    }
}