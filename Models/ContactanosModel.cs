using System.ComponentModel.DataAnnotations;

namespace prototipo_web.Models
{
    public class ContactanosModel
    {
        [Required(ErrorMessage = "EL campo {0} es requerido")]
        [MaxLength(10)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "EL campo {0} es requerido")]
        [MaxLength(10)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "EL campo {0} es requerido")]
        [MaxLength(10)]
        public string Mensaje { get; set; }
    }
}
