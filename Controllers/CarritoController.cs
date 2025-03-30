using Microsoft.AspNetCore.Mvc;
using prototipo_web.Repositorio;
using prototipo_web.Models;

namespace prototipo_web.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IcarritoServicio _carritoServicio;

        public CarritoController(IcarritoServicio carritoServicio)
        {
            _carritoServicio = carritoServicio;
        }

        public IActionResult Agregar(Producto ProductoId, int Cantidad)
        {
            if (ProductoId != null)
            {
                _carritoServicio.agregar(ProductoId, Cantidad);
            }
            var carroitem = _carritoServicio.ListarItemsCarro();
            return View("~/Views/Home/Carrito.cshtml", carroitem);
        }

        public IActionResult eliminar(int ProductoId)
        {
            _carritoServicio.eliminarItemcarro(ProductoId);

            var carritoItems = _carritoServicio.ListarItemsCarro();
            return View("~/Views/Home/Carrito.cshtml", carritoItems);
        }

        public IActionResult actualizarItem(int ProductId, int Cantidad)
        {
            if (Cantidad < 1)
            {
                return BadRequest("La cantidad debe ser al menos 1.");
            }

            _carritoServicio.actualizarItemCarro(ProductId, Cantidad);
            var carritoItems = _carritoServicio.ListarItemsCarro();
            return View("~/Views/Home/Carrito.cshtml", carritoItems);
        }
        public IActionResult carrito(int ProductoId, int Cantidad)
        {
            var detalle = _carritoServicio.save(ProductoId, Cantidad);
            if (detalle != null)
            {
                _carritoServicio.agregar(detalle, Cantidad);
            }
            var carroitem = _carritoServicio.ListarItemsCarro();
            return View("~/Views/Home/Carrito.cshtml", carroitem);
        }


    }
}

    



                                                