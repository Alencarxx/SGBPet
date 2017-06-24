using System.Web.Mvc;
using SGB_Beta1.Models;
using System.Data.Entity.Infrastructure;

namespace SGB_Beta1.Controllers
{
     [OutputCache(NoStore = true, Duration = 0)]
    public class AgendamentosController : Controller
    {
        SGBEntities db = new SGBEntities();

        public ActionResult Criar()
        {
            return View(new Models.Agendamentos());
        }

        [HttpPost]
        public ActionResult Criar(Agendamentos model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Agendamentos.Add(model);
                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Agendamentos", "Home");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Editar(int id)
        {
            Agendamentos clientes = db.Agendamentos.Find(id);

            clientes.Total = clientes.Total;

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost]
        public ActionResult Editar(int id, Agendamentos model)
        {
            try
            {
                db.Entry<Agendamentos>(model).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                TempData["mensagem"] = "Cadastro alterado com sucesso.";
                return RedirectToAction("Agendamentos", "Home");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Deletar(int id)
        {
            Agendamentos clientes = db.Agendamentos.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost, ActionName("Deletar")]
        public ActionResult ConfirmarExclusao(int id)
        {
            Agendamentos clientes = db.Agendamentos.Find(id);

            if (clientes == null)
                return HttpNotFound();

            try
            {
                db.Agendamentos.Remove(clientes);
                db.SaveChanges();
                ViewData["mensagem"] = "Cadastro foi excluido com sucesso.";
                return RedirectToAction("Agendamentos", "Home");
            }
            catch (DbUpdateException)
            {
                db.Entry<Agendamentos>(clientes).State = System.Data.Entity.EntityState.Unchanged;

                ViewData["mensagem"] = "Não foi possível excluir o registro.";
                return View(clientes);
            }
            catch
            {
                return View(clientes);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}