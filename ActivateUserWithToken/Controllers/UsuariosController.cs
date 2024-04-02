using ActivateUserWithToken.Helpers;
using ActivateUserWithToken.Models;
using ActivateUserWithToken.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ActivateUserWithToken.Controllers
{
    public class UsuariosController : Controller
    {
        private HelperMails helperMails;
        private HelperUploadFiles helperUploadFiles;
        private HelperPathProvider helperPathProvider;
        private Repository repo;

        public UsuariosController(HelperMails helperMails, Repository repo, HelperUploadFiles helperUploadFiles, HelperPathProvider helperPathProvider)
        {
            this.helperMails = helperMails;
            this.repo = repo;
            this.helperUploadFiles = helperUploadFiles;
            this.helperPathProvider = helperPathProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(string nombre, string apellido, string email, string password, IFormFile imagen)
        {
            try
            {
                string? fileName = null;
                if (imagen != null)
                {
                    fileName = await this.helperUploadFiles.UploadFilesAsync(imagen, Folders.Perfiles);
                }

                Usuario user = await this.repo.RegistrarUsuarioAsync(nombre, apellido, email, password, fileName);

                string serverUrl = this.helperPathProvider.MapPathURLProvider();
                serverUrl = serverUrl + "/Usuarios/ActivateUser?token=" + user.TokenMail;

                string mensaje = "<h3 style='color: dark; font-weight: bolder'>Usuario Registrado</h3>";
                mensaje += "<strong>Debe activar su cuenta con nosotros pulsando el siguiente enlace </strong>";
                mensaje += "<br/>";
                mensaje += "<a href='" + serverUrl + "'>" + serverUrl + "</a>";
                mensaje += "<p style='color: dark; font-weight: bolder'>Muchas gracias</p>";

                await this.helperMails.SendMailAsync(email, " Registro Usuario ", mensaje);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> ActivateUser(string token)
        {
            // BUSCAMOS EL USUARIO POR SU TOKEN
            Usuario usuario = await this.repo.BuscarUsuarioPorTokenAsync(token);

            if (usuario != null)
            {
                // Si encontramos al usuario, lo activamos y eliminamos el token
                await this.repo.ActivarUserAsync(token);

                ViewData["MENSAJE"] = "Cuenta activada correctamente";
                return View();
            }
            else
            {
                ViewData["MENSAJE"] = "Esta cuenta ya está activa o no existe en la DB";
                return View();
            }
        }
    }
}
