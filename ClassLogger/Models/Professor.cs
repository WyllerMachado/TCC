using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("Professores")]
    public class Professor
    {
        [Required]
        [Key, ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Turma> Turmas { get; set; }
    }
}