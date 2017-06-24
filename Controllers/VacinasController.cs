using System.Web.Mvc;
using SGB_Beta1.Models;
using System.Data.Entity.Infrastructure;

namespace SGB_Beta1.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class VacinasController : Controller
    {
        SGBEntities db = new SGBEntities();
        
        public ActionResult Criar()
        {
            return View(new Models.Vacinas());
        }

        [HttpPost]
        public ActionResult Criar(Vacinas model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Vacinas.Add(model);
                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Vacinas", "Home");
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
            Vacinas clientes = db.Vacinas.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost]
        public ActionResult Editar(int id, Vacinas model)
        {
            try
            {
                db.Entry<Vacinas>(model).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                TempData["mensagem"] = "Cadastro alterado com sucesso.";
                return RedirectToAction("Vacinas", "Home");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Deletar(int id)
        {
            Vacinas clientes = db.Vacinas.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost, ActionName("Deletar")]
        public ActionResult ConfirmarExclusao(int id)
        {
            Vacinas clientes = db.Vacinas.Find(id);

            if (clientes == null)
                return HttpNotFound();

            try
            {
                db.Vacinas.Remove(clientes);
                db.SaveChanges();
                ViewData["mensagem"] = "Cadastro foi excluido com sucesso.";
                return RedirectToAction("Vacinas", "Home");
            }
            catch (DbUpdateException)
            {
                db.Entry<Vacinas>(clientes).State = System.Data.Entity.EntityState.Unchanged;

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