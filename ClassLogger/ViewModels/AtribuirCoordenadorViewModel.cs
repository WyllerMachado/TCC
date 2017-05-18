using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClassLogger.ViewModels
{
    public class AtribuirCoordenadorViewModel
    {
        [Required]
        [Display(Name = "Curso")]
        public int CursoId { get; set; }

        public IEnumerable<SelectListItem> Cursos { get; set; }

        [Display(Name = "Coordenador")]
        public string CoordenadorId { get; set; }

        public IEnumerable<SelectListItem> Coordenadores { get; set; }
    }
}