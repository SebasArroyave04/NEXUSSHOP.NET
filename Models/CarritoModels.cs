using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prototipo_web.Models
{
    public class Producto
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public string descripcion { get; set; }
    }

    public class carroitem
    {
        public Producto producto { get; set; }
        public int cantidad { get; set; }
    }

    public class productoSeleccionado
    {
        public List<carroitem> Items { get; set; } = new List<carroitem>();
        public decimal TotalPrecio => Items.Sum(item => item.producto.precio * item.cantidad);
    }
}
