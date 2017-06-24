using System.Web.Mvc;
using SGB_Beta1.Models;
using System.Data.Entity.Infrastructure;

namespace SGB_Beta1.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class ProdutosController : Controller
    {
        SGBEntities db = new SGBEntities();

        public ActionResult Criar()
        {
            return View(new Models.Produtos());
        }

        [HttpPost]
        public ActionResult Criar(Produtos model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Produtos.Add(model);
                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Produtos", "Home");
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
            Produtos clientes = db.Produtos.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost]
        public ActionResult Editar(int id, Produtos model)
        {
            try
            {
                db.Entry<Produtos>(model).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                TempData["mensagem"] = "Cadastro alterado com sucesso.";
                return RedirectToAction("Produtos", "Home");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Deletar(int id)
        {
            Produtos clientes = db.Produtos.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost, ActionName("Deletar")]
        public ActionResult ConfirmarExclusao(int id)
        {
            Produtos clientes = db.Produtos.Find(id);

            if (clientes == null)
                return HttpNotFound();

            try
            {
                db.Produtos.Remove(clientes);
                db.SaveChanges();
                ViewData["mensagem"] = "Cadastro foi excluido com sucesso.";
                return RedirectToAction("Produtos", "Home");
            }
            catch (DbUpdateException)
            {
                db.Entry<Produtos>(clientes).State = System.Data.Entity.EntityState.Unchanged;

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