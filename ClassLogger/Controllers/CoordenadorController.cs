using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    [Authorize(Roles = "Administrador")]
    public class CoordenadorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public CoordenadorController()
        {
            _context = new ApplicationDbContext();
        }

        public CoordenadorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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


        // GET: /Coordenador/Registrar
        public ActionResult Registrar()
        {
            return View();
        }
        
        // POST: /Coordenador/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar([Bind(Exclude = "Foto")]RegistrarCoordenadorViewModel model)
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
                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // Criar o role "Coordenador"
                    // var roleStore = new RoleStore<IdentityRole>(_context);
                    // var roleManager = new RoleManager<IdentityRole>(roleStore);
                    // await roleManager.CreateAsync(new IdentityRole { Name = "Coordenador" });
                    ////////////////////////////////////////////////////////////////////////////

                    // Adicionando usuário ao role "Coordenador"
                    var userStore = new UserStore<ApplicationUser>(_context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    await userManager.AddToRoleAsync(user.Id, "Coordenador");

                    _context.Coordenadores.Add(new Coordenador(user.Id));
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Coordenador/Atribuir
        public ActionResult Atribuir()
        {
            var users = new List<ApplicationUser>();
            var coordenadores = _context.Coordenadores.ToList();

            using (var userStore = new UserStore<ApplicationUser>(_context))
                using (var userManager = new ApplicationUserManager(userStore))
                    users.AddRange(coordenadores.Select(coordenador => userManager.FindById(coordenador.UserId)));

            var cursos = _context.Cursos.ToList();

            var model = new AtribuirCoordenadorViewModel
            {
                Cursos = new SelectList(cursos, "CursoId", "Nome"),
                Coordenadores = new SelectList(users, "Id", "Nome")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Atribuir(AtribuirCoordenadorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var curso = _context.Cursos.Single(c => c.CursoId == model.CursoId);

            curso.CoordenadorId = model.CoordenadorId;

            _context.Cursos.AddOrUpdate(curso);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
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