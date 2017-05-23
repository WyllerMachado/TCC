using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClassLogger.Helpers;
using ClassLogger.Models;
using ClassLogger.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ClassLogger.Controllers
{
    public class AlunoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public AlunoController()
        {
            _context = new ApplicationDbContext();
        }

        public AlunoController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Aluno/Registrar
        public ActionResult Registrar()
        {
            var cursos = _context.Cursos.ToList();

            var model = new RegistrarAlunoViewModel()
            {
                Cursos = new SelectList(cursos, "CursoId", "Nome")
            };

            return View(model);
        }

        // POST: /Aluno/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar([Bind(Exclude = "Foto")]RegistrarAlunoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,

                    Nome = model.Nome,
                    Cpf = model.Cpf,
                    DataNascimento = model.DataNascimento,
                    Celular = model.Celular
                };

                // Adicionando imagem ao user
                if (Request.Files.Count > 0)
                {
                    var imageBinaryData = Photo.ToByteArray(Request.Files["Foto"]);
                    user.Foto = imageBinaryData;
                }

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Criar o role "Aluno"
                    var roleStore = new RoleStore<IdentityRole>(_context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole { Name = "Aluno" });
                    //////////////////////////////////////////////////////////////////////////

                    // Adicionando usuário ao role "Aluno"
                    var userStore = new UserStore<ApplicationUser>(_context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    await userManager.AddToRoleAsync(user.Id, "Aluno");

                    _context.Alunos.Add(new Aluno(user.Id, model.CursoId));
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion
    }
}