using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("Periodos")]
    public class Periodo
    {
        [Required, Key]
        public int PeriodoId { get; set; }

        [Required, ForeignKey("Curso")]
        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        public virtual ICollection<Turma> Turmas { get; set; }
        public virtual ICollection<Aluno> Alunos { get; set; }
    }
}