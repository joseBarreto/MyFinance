using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        private IHttpContextAccessor HttpContextAccessor { get; set; }

        public TransacaoController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }


        public IActionResult Index()
        {
            int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);
            ViewBag.ListaTransacoes = new TransacaoModel().GetTransacoes(id_usuario_logado);
            return View();
        }
        [HttpPost]
        [HttpGet]
        public IActionResult Extrato(TransacaoModel transacao)
        {
            int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);

            ViewBag.ListaTransacao = transacao.GetTransacoes(id_usuario_logado);
            ViewBag.ListaConta = new ContaModel().GetContas(id_usuario_logado);
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);
            var listDashboard = new DashboardModel().GetDadosGraficoPie(id_usuario_logado);

            string valores = "";
            string labels = "";
            string cores = "";
            var random = new Random();
            foreach (var item in listDashboard)
            {
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"

                valores += $"{item.Total},";
                labels += $"'{item.Descricao}',";
                cores += $"'{color}',";
            }
            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;


            return View();
        }

        [HttpGet]
        public IActionResult CriarTransacao(int? id)
        {
            int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);

            ViewBag.ListaContas = new ContaModel().GetContas(id_usuario_logado);
            ViewBag.ListaPlanoContas = new PlanoContaModel().GetPlanoContas(id_usuario_logado);

            if (id != null)
            {
                ViewBag.Registro = new TransacaoModel().CarregarRegistro(id);
            }

            return PartialView();
        }

        [HttpPost]
        public IActionResult CriarTransacao(TransacaoModel transacaoModel)
        {
            if (ModelState.IsValid)
            {
                int.TryParse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"), out int id_usuario_logado);

                if (transacaoModel.Id > 0)
                {
                    transacaoModel.Update();
                }
                else
                {
                    transacaoModel.Insert();
                }
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult ExcluirTransacao(int id)
        {
            ViewBag.Registro = new TransacaoModel().CarregarRegistro(id);
            return PartialView();
        }

        public IActionResult Excluir(int id)
        {
            new TransacaoModel().Excluir(id);
            return RedirectToAction("Index");
        }

    }
}