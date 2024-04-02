using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivateUserWithToken.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("USUARIO_ID")]
        public int IdUser { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("FOTO_PERFIL")]
        public string? FotoPerfil { get; set; }

        [Column("PASSWORD")]
        public byte[] Password { get; set; }

        [Column("SALT")]
        public string? Salt { get; set; }

        [Column("ACTIVO")]
        public bool? Activo { get; set; }

        [Column("TOKENMAIL")]
        public string TokenMail { get; set; }

        [Column("ROL_ID")]
        public int? RolId { get; set; } 
     
    }
}
