using SGB_Beta1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGB_Beta1.Controllers
{
    public class FinanceiroController : Controller
    {
        SGBEntities DB = new SGBEntities();

        // GET: Financeiro
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SaldoVendas()
        {
            return View();
        }

        public ActionResult Saidas()
        {
            return View();
        }

        public ActionResult ContasApagar()
        {
            return View();
        }
    }
}