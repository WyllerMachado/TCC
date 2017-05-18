using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("Aulas")]
    public class Aula
    {
        /* Uma aula de uma turma, com data 
         * e duração definidas, que possui 
         * uma lista de registros de aluno */

        [Required, Key]
        public int AulaId { get; set; }

        [Required, ForeignKey("Turma")]
        public int TurmaId { get; set; }
        public virtual Turma Turma { get; set; }

        public virtual ICollection<RegistroDeAluno> RegistrosDeAluno { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public TimeSpan Duracao { get; set; }

        public int Codigo { get; private set; }
        public bool Encerrada { get; set; }
        

        public void GerarCodigo()
        {
            Random generator = new Random();
            Codigo = generator.Next(100000, 999999);
        }
    }
}