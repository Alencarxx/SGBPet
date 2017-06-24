using System.Web.Mvc;
using SGB_Beta1.Models;
using System.Data.Entity.Infrastructure;

namespace SGB_Beta1.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class ClientesController : Controller
    {
        SGBEntities db = new SGBEntities();

        public ActionResult Criar()
        {
            return View(new Models.Clientes());
        }
       
        [HttpPost]
        public ActionResult Criar(Clientes model)
        {
            try
            {
                if (ModelState.IsValid)
                {                       
                    db.Clientes.Add(model);
                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Clientes", "Home");
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
            Clientes clientes = db.Clientes.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost]
        public ActionResult Editar(int id, Clientes model)
        {
            try
            {             
                db.Entry<Clientes>(model).State = System.Data.Entity.EntityState.Modified;             

                db.SaveChanges();

                TempData["mensagem"] = "Cadastro alterado com sucesso.";
                return RedirectToAction("Clientes", "Home");
            }
            catch
            {                
                return View(model);
            }
        }

        public ActionResult Deletar(int id)
        {
            Clientes clientes = db.Clientes.Find(id);

            if (clientes == null)
                return HttpNotFound();

            return View(clientes);
        }

        [HttpPost, ActionName("Deletar")]
        public ActionResult ConfirmarExclusao(int id)
        {
            Clientes clientes = db.Clientes.Find(id);

            if (clientes == null)
                return HttpNotFound();

            try
            {
                db.Clientes.Remove(clientes);
                db.SaveChanges();
                ViewData["mensagem"] = "Cadastro foi excluido com sucesso.";
                return RedirectToAction("Clientes", "Home");
            }
            catch (DbUpdateException)
            {
                db.Entry<Clientes>(clientes).State = System.Data.Entity.EntityState.Unchanged;

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