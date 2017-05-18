using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("Boletins")]
    public class Boletim
    {
        [Required]
        [Key, ForeignKey("Aluno")]
        [Column(Order = 1)]
        public string AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }

        [Required]
        [Key, ForeignKey("Turma")]
        [Column(Order = 2)]
        public int TurmaId { get; set; }
        public virtual Turma Turma { get; set; }

        public decimal NotaAv1 { get; set; }
        public decimal NotaAv2 { get; set; }
        public decimal NotaAv3 { get; set; }
        public decimal NotaFinal { get; set; }

        public int Presencas { get; set; }

        public bool Aprovado { get; set; }
    }
}