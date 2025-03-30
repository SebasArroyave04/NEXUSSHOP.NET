using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prototipo_web.Models
{
    public class ProveedoresModel
    {
        public string Nombres { get; set; }
        public int Identificacion { get; set; }
        public string Empresa { get; set; }
        public string CorreoEmpresa { get; set; }
        public int RUT { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correoprov { get; set; }
        public string Comentarios { get; set; }
    }
}
