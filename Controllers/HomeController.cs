using System.Web.Mvc;
using System.Linq;
using SGB_Beta1.Models;
using System;
using System.Collections.Generic;
using PagedList;
using System.Configuration;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SGB_Beta1.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        SGBEntities DB = new SGBEntities();

        public ActionResult Index(int? pagina)
        {
            var diasHome = ConfigurationManager.AppSettings.Get("DiasHome");
            var datInclusao = DateTime.Now.AddDays(Convert.ToDouble(diasHome));
            string administrador = ConfigurationManager.AppSettings.Get("Administrador");
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            var usuario = DB.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

            if (usuario.UserName == User.Identity.GetUserName())
                TempData["Autorizacao"] = User.Identity.GetUserName();
            else
                TempData["Autorizacao"] = "Não autorizado";

            var model = DB.Agendamentos.Where(x => x.Chegada >= datInclusao).ToList();

            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Clientes(int? pagina)
        {
            var model = DB.Clientes.ToList();
            string administrador = ConfigurationManager.AppSettings.Get("Administrador");
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            var usuario = DB.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

            if (usuario.UserName == User.Identity.GetUserName())
                TempData["Autorizacao"] = User.Identity.GetUserName();
            else
                TempData["Autorizacao"] = "Não autorizado";
            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Agendamentos(int? pagina)
        {
            var model = DB.Agendamentos.ToList();
            string administrador = ConfigurationManager.AppSettings.Get("Administrador");
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            var usuario = DB.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

            if (usuario.UserName == User.Identity.GetUserName())
                TempData["Autorizacao"] = User.Identity.GetUserName();
            else
                TempData["Autorizacao"] = "Não autorizado";
            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Contratos(int? pagina)
        {
            var model = DB.Contratos.ToList();
            string administrador = ConfigurationManager.AppSettings.Get("Administrador");
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            var usuario = DB.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

            if (usuario.UserName == User.Identity.GetUserName())
                TempData["Autorizacao"] = User.Identity.GetUserName();
            else
                TempData["Autorizacao"] = "Não autorizado";
            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Vacinas(int? pagina)
        {
            var model = DB.Vacinas.ToList();
            string administrador = ConfigurationManager.AppSettings.Get("Administrador");
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            var usuario = DB.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

            if (usuario.UserName == User.Identity.GetUserName())
                TempData["Autorizacao"] = User.Identity.GetUserName();
            else
                TempData["Autorizacao"] = "Não autorizado";
            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Produtos(int? pagina)
        {
            var model = DB.Produtos.ToList();
            string administrador = ConfigurationManager.AppSettings.Get("Administrador");
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            var usuario = DB.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

            if (usuario.UserName == User.Identity.GetUserName())
                TempData["Autorizacao"] = User.Identity.GetUserName();
            else
                TempData["Autorizacao"] = "Não autorizado";
            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Vendas(int? pagina)
        {
            var model = DB.Vendas.OrderByDescending(x => x.codigoVenda);

            string administrador = ConfigurationManager.AppSettings.Get("Administrador");
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            var usuario = DB.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

            if (usuario.UserName == User.Identity.GetUserName())
            {
                TempData["Autorizacao"] = User.Identity.GetUserName();
            }
            else
                TempData["Autorizacao"] = "Não autorizado";

            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
            //return View(model);
        }

        public ActionResult Financeiro(int? pagina)
        {
            var model = DB.Vendas.OrderByDescending(x => x.codigoVenda);
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            string administrador = ConfigurationManager.AppSettings.Get("Administrador");
            int paginaNumero = (pagina ?? 1);
            ViewData["VendasPorData"] = DB.VendasPorData.ToList();
            ViewData["Saidas"] = DB.Saidas.ToList();
            ViewData["ContasApagar"] = DB.ContasApagar.ToList();

            var usuario = DB.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

            if (usuario.UserName == User.Identity.GetUserName())
                TempData["Autorizacao"] = User.Identity.GetUserName();
            else
                TempData["Autorizacao"] = "Não autorizado";

            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
            //return View(model);
        }

    }
}