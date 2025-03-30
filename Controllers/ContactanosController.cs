using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prototipo_web.Models;
using prototipo_web.Repositorio;

namespace prototipo_web.Controllers
{
    public class ContactanosController : Controller
    { 
    private readonly IrepositorioContacto repocontacto;

    public ContactanosController(IrepositorioContacto repocontacto)
    {
         this.repocontacto = repocontacto;
    }

      public IActionResult Contactanos()
      {
          return View();
      }

        public IActionResult enviarcontacto(ContactanosModel contacto)
        {
            if (!ModelState.IsValid)
                return View("~/views/Contactanos/Contactanos.cshtml");

            repocontacto.Contacto(contacto);
            TempData["SuccessMessage"] = "El Mensaje fue mandado con éxito.";
            return View("~/views/Contactanos/Contactanos.cshtml");
        }
    }
}
