using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClassLogger.Models;
using ClassLogger.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClassLogger.Controllers
{
    public class CursoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursoController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Curso/Registrar
        public ActionResult Registrar()
        {
            var users = new List<ApplicationUser>();
            var coordenadores = _context.Coordenadores.ToList();

            using (var userStore = new UserStore<ApplicationUser>(_context))
                using (var userManager = new ApplicationUserManager(userStore))
                    users.AddRange(coordenadores.Select(coordenador => userManager.FindById(coordenador.UserId)));

            var model = new RegistrarCursoViewModel
            {
                Coordenadores = new SelectList(users, "Id", "Nome")
            };
            
            return View(model);
        }

        // POST: Curso/Registrar
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar(RegistrarCursoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var curso = new Curso
            {
                Nome = model.Nome,
                CoordenadorId = model.CoordenadorId
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // GET: Curso/Listar
        public ActionResult Listar()
        {
            var cursos = _context.Cursos.ToList();

            var model = new ListarCursoViewModel
            {
                Cursos = new List<Curso>()
            };

            // Convertendo os UserId's para os nomes dos Coordenadores
            foreach (var curso in cursos)
            {
                if (curso.CoordenadorId != null)
                    using (var userStore = new UserStore<ApplicationUser>(_context))
                    using (var userManager = new ApplicationUserManager(userStore))
                        curso.CoordenadorId = userManager.FindById(curso.CoordenadorId).Nome;
                else
                    curso.CoordenadorId = "Não atribuído";

                model.Cursos.Add(curso);
            }

            return View(model);
        }
    }
}
