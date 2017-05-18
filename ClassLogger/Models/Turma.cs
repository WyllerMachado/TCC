using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("Turmas")]
    public class Turma
    {
        /* Uma turma, onde é lecionada uma 
         * disciplina, com professor, alunos, 
         * qual periodo e qual curso faz parte */

        [Required, Key]
        public int TurmaId { get; set; }

        [Required, ForeignKey("Disciplina")]
        public int DisciplinaId { get; set; }
        public virtual Disciplina Disciplina { get; set; }

        [Required, ForeignKey("Professor")]
        public string ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
        
        [Required, ForeignKey("Periodo")]
        public int PeriodoId { get; set; }
        public virtual Periodo Periodo { get; set; }

        public virtual ICollection<Aula> Aulas { get; set; }
        public virtual ICollection<Aluno> Alunos { get; set; }
        public virtual ICollection<Boletim> Boletins { get; set; }
    }
}