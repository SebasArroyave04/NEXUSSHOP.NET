using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prototipo_web.Controllers
{
    public class empleadosController : Controller
    {
        public ActionResult guardar(IFormCollection Save)
        {
            string Identificacion = Save["Identificacion"];
            string Nombre = Save["Nombre"];
            string Sexo = Save["Sexo"];
            string Correo = Save["Correo"];
            string Fecha_de_nacimiento = Save["Fecha_de_nacimiento"];
            string Cargo = Save["Cargo"];
            string Apellido = Save["Apellido"];
            string Telefono = Save["Telefono"];
            string Oficina = Save["Oficina"];
            string Funcion = Save["Funcion"];

            if (Identificacion != null && Nombre != null && Sexo != null && Correo != null && Fecha_de_nacimiento != null && Cargo != null && Apellido != null && Telefono != null && Oficina != null && Funcion != null)
                return View();
            else
                TempData["error"] = "Debes llenar todos los campos";
            return RedirectToAction("Index", "empleados");
        }

        // GET: empleadosController
        public IActionResult Index()
        {
            return View("empleados");
        }


        // GET: empleadosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: empleadosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: empleadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: empleadosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: empleadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: empleadosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: empleadosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
