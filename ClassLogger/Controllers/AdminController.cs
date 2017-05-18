using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClassLogger.Models;
using ClassLogger.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ClassLogger.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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


        // GET: /Admin/RegistrarAdmin
        [AllowAnonymous]
        public ActionResult RegistrarAdmin()
        {
            return View();
        }


        // POST: /Admin/RegistrarAdmin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarAdmin([Bind(Exclude = "Foto")]RegistrarAdminViewModel model)
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
                    var imageBinaryData = PhotoToByteArray(Request.Files["Foto"]);
                    user.Foto = imageBinaryData;
                }

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    
                    // Criar o role "Administrador"
                    // var roleStore = new RoleStore<IdentityRole>(_context);
                    // var roleManager = new RoleManager<IdentityRole>(roleStore);
                    // await roleManager.CreateAsync(new IdentityRole { Name = "Administrador" });
                    ////////////////////////////////////////////////////////////////////////////

                    // Adicionando usuário ao role "Administrador"
                    var userStore = new UserStore<ApplicationUser>(_context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    await userManager.AddToRoleAsync(user.Id, "Administrador");
                    

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
        #region Helpers

        private byte[] PhotoToByteArray(HttpPostedFileBase photo)
        {
            byte[] imageBinaryData = null;

            if (photo != null)
                using (var binaryReader = new BinaryReader(photo.InputStream))
                    imageBinaryData = binaryReader.ReadBytes(photo.ContentLength);

            return imageBinaryData;
        }

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