using System.Web.Mvc;
using SGB_Beta1.Models;
using System.Data.Entity.Infrastructure;

namespace SGB_Beta1.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class ContratosController : Controller
    {
        SGBEntities db = new SGBEntities();

        #region Handles        
        public ActionResult Criar()
        {
            return View(new Models.Contratos());
        }

        [HttpPost]
        public ActionResult Criar(Contratos model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Contratos.Add(model);
                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Contratos", "Home");
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
            Contratos clientes = db.Contratos.Find(id);

            if (clientes == null)
                return HttpNotFound();          

            return View(clientes);
        }

        [HttpPost]
        public ActionResult Editar(int id, Contratos model)
        {
            try
            {
                db.Entry<Contratos>(model).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                TempData["mensagem"] = "Cadastro alterado com sucesso.";
                return RedirectToAction("Contratos", "Home");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Deletar(int id)
        {
            Contratos clientes = db.Contratos.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost, ActionName("Deletar")]
        public ActionResult ConfirmarExclusao(int id)
        {
            Contratos clientes = db.Contratos.Find(id);

            if (clientes == null)
                return HttpNotFound();

            try
            {
                db.Contratos.Remove(clientes);
                db.SaveChanges();
                ViewData["mensagem"] = "Cadastro foi excluido com sucesso.";
                return RedirectToAction("Contratos", "Home");
            }
            catch (DbUpdateException)
            {
                db.Entry<Contratos>(clientes).State = System.Data.Entity.EntityState.Unchanged;

                ViewData["mensagem"] = "Não foi possível excluir o registro.";
                return View(clientes);
            }
            catch
            {
                return View(clientes);
            }
        }

        #endregion


        #region Imprimir
        public ActionResult Imprimir(int id)
        {
            Contratos clientes = db.Contratos.Find(id);            

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        } 
     
         public ActionResult Imprimir2(int id)
        {
            Contratos clientes = db.Contratos.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }
       

        #endregion
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
	}
}