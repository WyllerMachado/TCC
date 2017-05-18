using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLogger.Models
{
    [Table("Coordenadores")]
    public class Coordenador
    {
        [Required]
        [Key, ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }

        public Coordenador()
        {
            
        }

        public Coordenador(string userId)
        {
            UserId = userId;
        }
    }
}