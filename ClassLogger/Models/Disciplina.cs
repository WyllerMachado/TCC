using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("Disciplinas")]
    public class Disciplina
    {
        /* A disciplina propriamente dita, 
         * com sua ementa e cursos os quais 
         * faz parte. Não possui professores 
         * nem alunos */
        
        [Required, Key]
        public int DisciplinaId { get; set; }

        [Required]
        public string Nome { get; set; }
        
        public string Ementa { get; set; }

        public virtual ICollection<Turma> Turmas { get; set; }
        public virtual ICollection<Curso> Cursos { get; set; }
    }
}