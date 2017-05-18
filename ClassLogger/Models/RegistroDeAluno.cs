using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("RegistrosDeAluno")]
    public class RegistroDeAluno
    {
        /* O registro de um aluno em uma aula 
         * espec�fica, com seu hor�rio de entrada,
         * sa�da e tempo de perman�ncia. */

        [Required]
        [Key, ForeignKey("Aula")]
        [Column(Order = 1)]
        public int AulaId { get; set; }
        public virtual Aula Aula { get; set; }

        [Required]
        [Key, ForeignKey("Aluno")]
        [Column(Order = 2)]
        public string AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }

        public DateTime HorarioEntrada { get; set; }
        public DateTime HorarioSaida { get; set; }
        public TimeSpan Permanencia { get; set; }
        
        // Invalidar presen�a se [permanencia < 30 minutos]
    }
}