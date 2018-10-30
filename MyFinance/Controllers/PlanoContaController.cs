using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class PlanoContaController : Controller
    {
        private IHttpContextAccessor HttpContextAccessor { get; set; }

        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);

            ViewBag.ListaPlanoConta = new PlanoContaModel().GetPlanoContas(id_usuario_logado);
            return View();
        }

        [HttpGet]
        public IActionResult CriarPlanoConta(int? id)
        {
            if (id != null)
            {
                ViewBag.Registro = new PlanoContaModel().CarregarRegistro(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult CriarPlanoConta(PlanoContaModel planoContaModel)
        {
            if (ModelState.IsValid)
            {
                int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);


                if (planoContaModel.Id > 0)
                {
                    planoContaModel.Update();
                }
                else
                {
                    planoContaModel.Insert(id_usuario_logado);
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult ExcluirPlanoConta(int id)
        {
            new PlanoContaModel().Excluir(id);
            return RedirectToAction("Index");
        }
    }
}