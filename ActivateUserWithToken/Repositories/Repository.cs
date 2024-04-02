using ActivateUserWithToken.Data;
using ActivateUserWithToken.Helpers;
using ActivateUserWithToken.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivateUserWithToken.Repositories
{
    public class Repository
    {
        private UserContext context;
        public Repository(UserContext context)
        {
            this.context = context;
        }
        
        public async Task<Usuario> RegistrarUsuarioAsync(string nombre, string apellido, string email, string password, string imagen)
        {
            // Verificar si ya existe un usuario con el mismo correo electrónico
            var existingUser = await this.context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                throw new Exception("Ya existe una cuenta con este correo electrónico.");
            }

            Usuario user = new Usuario();

            user.IdUser = await this.GetMaxIdUsuarioAsync();
            user.Nombre = nombre;
            user.Apellido = apellido;
            user.Email = email;

            // Si no se proporciona una imagen, asignar la imagen por defecto
            if (imagen == null)
            {
                user.FotoPerfil = "default.png"; // EN wwwroot/perfiles/default.png
            }
            else
            {
                user.FotoPerfil = imagen;
            }


            int idRol;
            if (user.Email == "bookifyapp@outlook.com")
            {
                idRol = 1; //ADMIN
                user.RolId = idRol;
            }
            else
            {
                idRol = 2; //USER
                user.RolId = idRol;
            }

            //CADA USUARIO TENDRÁ UN SALT DISTINTO
            user.Salt = HelperTools.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            //ACTIVAMOS USUARIO EN false y GENERAMOS TOKEN
            user.Activo = false;
            user.TokenMail = HelperTools.GenerateTokenMail();

            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
            return user;
        }

        private async Task<int> GetMaxIdUsuarioAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Usuarios.MaxAsync(z => z.IdUser) + 1;
            }
        }

        public async Task ActivarUserAsync(string token)
        {
            ////BUSCAMOS EL USUARIO POR SU TOKEN
            Usuario usuario = await this.context.Usuarios.FirstOrDefaultAsync(t => t.TokenMail == token);
            usuario.Activo = true;
            usuario.TokenMail = "";
            await this.context.SaveChangesAsync();
        }

        public async Task<Usuario> BuscarUsuarioPorTokenAsync(string token)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(u => u.TokenMail == token);

        }
    }
}
