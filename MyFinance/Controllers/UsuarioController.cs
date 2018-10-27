using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Login(int? id)
        {
            if (id != null && id == 0)
            {
                HttpContext.Session.SetString("NomeUsuarioLogado", string.Empty);
                HttpContext.Session.SetString("IdUsuarioLogado", string.Empty);
            }
            return View();
        }

        [HttpPost]
        public IActionResult ValidarLogin(UsuarioModel usuario)
        {
            if (usuario.ValidarLogin() == true)
            {
                HttpContext.Session.SetString("NomeUsuarioLogado", usuario.Nome);
                HttpContext.Session.SetString("IdUsuarioLogado", usuario.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["MessagemLoginInvalido"] = "Dados de login inválidos";
                return RedirectToAction("Login");
            }
        }
    }
}