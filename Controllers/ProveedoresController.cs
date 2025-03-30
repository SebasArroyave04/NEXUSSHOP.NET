using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prototipo_web.Models;
using prototipo_web.Repositorio;

namespace prototipo_web.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly IRepositorioProveedor repositorioProveedor;

        public ProveedoresController(IRepositorioProveedor repositorioProveedor)
        {
            this.repositorioProveedor = repositorioProveedor;
        }

        public IActionResult Proveedores(ProveedoresModel proveedores)
        {
           if (!ModelState.IsValid)
           return View(proveedores);

            repositorioProveedor.Proveedores(proveedores);
            TempData["SuccessMessage"] = "El registro de proveedor fue exitoso.";
            return View();
        }

        public IActionResult Index() 
        {
            return View("~/Views/Proveedores/Proveedores.cshtml");
        }
    }
}
