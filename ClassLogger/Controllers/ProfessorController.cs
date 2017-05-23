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
    public class ProfessorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public ProfessorController()
        {
            _context = new ApplicationDbContext();
        }

        public ProfessorController(ApplicationUserManager userManager)
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

        // GET: /Professor/Registrar
        public ActionResult Registrar()
        {
            return View();
        }

        // POST: /Professor/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar([Bind(Exclude = "Foto")]RegistrarProfessorViewModel model)
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
                    // Criar o role "Professor"
                    // var roleStore = new RoleStore<IdentityRole>(_context);
                    // var roleManager = new RoleManager<IdentityRole>(roleStore);
                    // await roleManager.CreateAsync(new IdentityRole { Name = "Professor" });
                    //////////////////////////////////////////////////////////////////////////

                    // Adicionando usuário ao role "Professor"
                    var userStore = new UserStore<ApplicationUser>(_context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    await userManager.AddToRoleAsync(user.Id, "Professor");

                    _context.Professores.Add(new Professor(user.Id));
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