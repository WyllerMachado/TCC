using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassLogger.ViewModels
{
    public class RegistrarCursoViewModel
    {
        [Required]
        [Display(Name="Nome do Curso")]
        public string Nome { get; set; }

        public IEnumerable<SelectListItem> Coordenadores { get; set; }

        [Display(Name="Coordenador")]
        public string CoordenadorId { get; set; }
    }
}