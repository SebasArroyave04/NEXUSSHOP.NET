using Microsoft.AspNetCore.Mvc;
using prototipo_web.Models;
using prototipo_web.Repositorio;

namespace prototipo_web.Controllers
{
    public class comprasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IRepositorioCompras repositoriocompras;
        public comprasController(RepositorioCompras repositoriocompras)
        {
            this.repositoriocompras = repositoriocompras;
        }
        public IActionResult compras()
        {
            return View("~/Views/Home/Compras.cshtml");
        }
        public IActionResult ObtenerCompra(ComprasModel codigo)
        {
            var producto = repositoriocompras.ObtenerCompra(codigo);
            if (producto == null)
            {
                return NotFound();
            }
            return Json(producto);
        }
    }
}
