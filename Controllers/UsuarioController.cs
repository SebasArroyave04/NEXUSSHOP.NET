using Microsoft.AspNetCore.Mvc;
using prototipo_web.Controllers;
using prototipo_web.Models;
using prototipo_web;
using prototipo_web.Repositorio;

namespace prototipo_web.Controllers
{
    public class UsuarioController
    {

    }
}
public class UsuarioController : Controller
{
    private readonly IRepositorioUsuario Repousuario;

    public UsuarioController(IRepositorioUsuario Repousuario)
    {
        this.Repousuario = Repousuario;
    }

    public IActionResult Inicio(RegistrarUsuario guardar)
    {
        return View();
    }

    public async Task<IActionResult> Login(LoginUsuario login)

    {
        if (!ModelState.IsValid)
        {
            return View("~/views/Home/Login.cshtml", login);
        }

        else
        {
            ErrorViewModel errorModel = new ErrorViewModel();
            try
            {
                EncryptMDS EncryptMD5 = new EncryptMDS();
                login.Contrasena = EncryptMD5.Encriptar(login.Contrasena);

                bool rsp = await Repousuario.ValidarUsuario(login);
                if (rsp)
                {
                    return View("~/views/Home/MenuUsuario.cshtml");
                }
                else
                {
                    return View("~/views/Home/Login.cshtml");
                }
            }

            catch (Exception Error)
            {
                errorModel.RequestId = Error.HResult.ToString();
                errorModel.Message = Error.Message;
            }
            return View("Error", errorModel);
        }
    }

    public IActionResult Registro(RegistrarUsuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View("~/Views/Home/Registro.cshtml", usuario);
        }
        EncryptMDS EncryptMD5 = new EncryptMDS();
        usuario.Contrasena = EncryptMD5.Encriptar(usuario.Contrasena);
        TempData["SuccessMessage"] = "El registro de usuario fue exitoso.";
        Repousuario.Registro(usuario);

        return View("~/Views/Home/Registro.cshtml");
    }
}   