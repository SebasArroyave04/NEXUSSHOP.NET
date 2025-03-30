using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Razor.Hosting;
using prototipo_web.Models;
using prototipo_web.Repositorio;
using System.Diagnostics;

namespace prototipo_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorioHome repositorioHome;
        private readonly IRepositorioCompras repositoriocompras;
        private readonly IRepositorioProveedor repositorioProveedor;
        public HomeController(IRepositorioHome repositorioHome, IRepositorioProveedor repoitorioProveedor, IRepositorioCompras repositoriocompras)
        {
            this.repositorioHome = repositorioHome;
            this.repositoriocompras = repositoriocompras;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductosModel> productos = repositorioHome.Listarproductos();
            return View(productos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login(RegistrarUsuario guardar)
        {
            return View();
        }

        public IActionResult Slider()
        {
            return View();
        }

        public IActionResult MenuUsuario()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult empleados()
        {
            return View();
        }

        public async Task<IActionResult> Productos(ProductosModel model)
        {
            try
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var extension = Path.GetExtension(model.ImageFile.FileName);
                    var NuevoNombre = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Elementos", NuevoNombre);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    model.urlimage = "./Elementos/" + NuevoNombre;
                    repositorioHome.Productos(model);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            TempData["SuccessMessage"] = "El registro de compras fue exitoso.";
            return View();
        }

        [HttpGet]
        public String Mensaje()
        {
            return "Mensaje Back sino crees";
        }

        [HttpGet]
        public JsonResult DetalleProducto(int id)
        {
            ProductosModel detalle = repositorioHome.DetalleProducto(id);
            detalle.accesorios = "Si";
            if (detalle.accesorios == "0")
            {
                detalle.accesorios = "No";
            }
            return Json(detalle);
        }
        public IActionResult Compras()
        {
            return View("~/Views/Home/Compras.cshtml");
        }
        public async Task<IActionResult> ObtenerCompra(string codigo)
        {
            var producto = await repositoriocompras.ObtenerProductoPorCodigo(codigo);
            if (producto == null)
            {
                return NotFound();
            }
            return Json(producto);
        }

        [HttpGet]
        public JsonResult ObtenerProveedor(int Identificacion) 
        {
            ComprasModel detalle = repositoriocompras.ObtenerProveedor(Identificacion);
            return Json(detalle.Nombres);
        }
    }
}
