using System.ComponentModel.DataAnnotations;

namespace prototipo_web.Models
{
    public class LoginUsuario
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Contrasena { get; set; }
    }
}
