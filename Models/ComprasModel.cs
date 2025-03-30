namespace prototipo_web.Models
{
    public class ComprasModel
    {
        public int codigo { get; set; }
        public string Nombres { get; set; }
        public int Identificacion { get; set; }
        public DateTimeOffset fecha { get; set; }
        public int codigoProducto { get; set; }
        public int precio { get; set; }
        public string proveedor { get; set; }
        public int valorPorUnidad { get; set; }
        public int Subtotal { get; set; }
        public int cantidad { get; set; }
    }
}
