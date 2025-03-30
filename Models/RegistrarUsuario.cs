using System.ComponentModel.DataAnnotations;

namespace prototipo_web.Models
{
    public class RegistrarUsuario
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string Identificacion {  get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string FNacimiento { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Tiposexo SSexo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string CContrasena { get; set; }
    }
}

