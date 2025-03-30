using System.ComponentModel.DataAnnotations;

namespace prototipo_web.Models
{
    public class ProductosModel
    {
            public string codigo { get; set; }
            public string nombre { get; set; }
            public string descripcion { get; set; }
            public string precio { get; set; }
            public string unidades { get; set; }
            public string estado { get; set; }
            public string marca { get; set; }
            public string modelo { get; set; }
            public string accesorios { get; set; }
            public string urlimage { get; set; }
            public IFormFile ImageFile { get; set; }
    }

    
}
