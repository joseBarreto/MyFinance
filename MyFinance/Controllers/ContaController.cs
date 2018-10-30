using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class ContaController : Controller
    {
        private IHttpContextAccessor HttpContextAccessor { get; set; }

        public ContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);

            ViewBag.ListaConta = new ContaModel().GetContas(id_usuario_logado);
            return View();
        }

        [HttpGet]
        public IActionResult CriarConta(int? id)
        {
            if (id != null)
            {
                ViewBag.Registro = new ContaModel().CarregarRegistro(id);
            }

            return View();
        }

        [HttpPost]
        public IActionResult CriarConta(ContaModel contaModel)
        {
            if (ModelState.IsValid)
            {
                int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);


                if (contaModel.Id > 0)
                {
                    contaModel.Update();
                }
                else
                {
                    contaModel.Insert(id_usuario_logado);
                }
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult ExcluirConta(int id)
        {
            new ContaModel().Excluir(id);
            return RedirectToAction("Index");
        }
    }
}