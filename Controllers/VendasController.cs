using SGB_Beta1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace SGB_Beta1.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class VendasController : Controller
    {
        SGBEntities db = new SGBEntities();

        public ActionResult Criar(int? Id, int? pagina)
        {
            var model = db.Pedido.ToList();
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            string administrador = ConfigurationManager.AppSettings.Get("Administrador");

            var usuario = db.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

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

        [HttpPost]
        public ActionResult Criar(Pedido model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Pedido.Add(model);
                    db.SaveChanges();

                    string administrador = ConfigurationManager.AppSettings.Get("Administrador");
                    
                    var usuario = db.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(administrador));

                    if (usuario.UserName == User.Identity.GetUserName())
                    {
                        TempData["Autorizacao"] = User.Identity.GetUserName();
                    }
                    else
                        TempData["Autorizacao"] = "Não autorizado";


                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Vendas", "Home");
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

        public ActionResult Editar(int id, int? pagina)
        {
            Pedido ped = db.Pedido.Find(id);
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            if (ped == null)
                return HttpNotFound();
            else
                return View(ped);
            //return View(model);

            //return View(ped);
        }

        [HttpPost]
        public ActionResult Editar(int id, Pedido model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry<Pedido>(model).State = EntityState.Modified;

                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro alterado com sucesso.";
                    return RedirectToAction("Criar", "Vendas");
                }
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }

        public ActionResult Deletar(int id, int? pagina)
        {
            Pedido ped = db.Pedido.Find(id);
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            if (ped == null)
                return HttpNotFound();
            else
                return View(ped);
            //return View(model);

            //return View(ped);
        }

        [HttpPost, ActionName("Deletar")]
        public ActionResult ConfirmarExclusao(int id)
        {
            Pedido ped = db.Pedido.Find(id);

            if (ped == null)
                return HttpNotFound();

            try
            {
                db.Pedido.Remove(ped);
                db.SaveChanges();
                ViewData["mensagem"] = "Cadastro foi excluido com sucesso.";
                return RedirectToAction("Criar", "Vendas");
            }
            catch (DbUpdateException)
            {
                db.Entry<Pedido>(ped).State = EntityState.Unchanged;

                ViewData["mensagem"] = "Não foi possível excluir o registro.";
                return View(ped);
            }
            catch
            {
                return View(ped);
            }
        }

        public ActionResult Relatorio()
        {
            return View(new Vendas());
        }

        [HttpPost]
        public ActionResult Relatorio(Vendas model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Vendas.Add(model);
                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Vendas", "Home");
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

        public ActionResult Imprimir(int id, int? pagina)
        {
            var model = db.Pedido.ToList();
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            if (model == null)
                return View("NotFound");
            else
                return View(model.ToPagedList(paginaNumero, paginaTamanho));
            //return View(model);    
        }

        [HttpPost]
        public ActionResult Imprimir(Pedido model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Pedido.Add(model);
                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Vendas", "Home");
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

        public ActionResult Pedidos(int? pedido)
        {
            //ViewData["produtos"] = new SelectList(db.Produtos.OrderBy(o => o.Descricao).AsEnumerable(), "Id", "Descricao");
            return View(new Pedido());
        }

        [HttpPost]
        public ActionResult Pedidos(Pedido model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Codigo_Produto = model.PedidoID.ToString();
                    var banco = db.Pedido.ToList();
                    if (banco.Count >= 1)
                    {
                        var ultimoId = db.Pedido.OrderByDescending(o => o.PedidoID).First();

                        var total = ultimoId.PedidoID + 1;
                        model.PedidoID = total;
                    }
                    else
                    {
                        var pedidoId = db.PedidoFechado.OrderByDescending(o => o.codigoProduto).First();

                        var total2 = pedidoId.codigoProduto + 1;
                        model.PedidoID = (int) total2;
                    }

                    db.Pedido.Add(model);
                    db.SaveChanges();

                    TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    return RedirectToAction("Criar", "Vendas");
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

        public ActionResult DeletarVendas(int id, int? pagina)
        {
            Vendas ved = db.Vendas.Find(id);
            int paginaTamanho = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TamanhoPagina"));
            int paginaNumero = (pagina ?? 1);
            if (ved == null)
                return HttpNotFound();
            else
                return View(ved);        
        }

        [HttpPost]
        public ActionResult DeletarVendas(int id, Vendas model)
        {
            Vendas ped = db.Vendas.Find(id);

            if (ped == null)
                return HttpNotFound();

            try
            {
                db.Vendas.Remove(ped);
                db.SaveChanges();
                ViewData["mensagem"] = "Cadastro foi excluido com sucesso.";
                return RedirectToAction("Vendas", "Home");
            }
            catch (DbUpdateException)
            {
                db.Entry<Vendas>(ped).State = EntityState.Unchanged;

                ViewData["mensagem"] = "Não foi possível excluir o registro.";
                return View(ped);
            }
            catch
            {
                return View(ped);
            }
        }

        [HttpGet]
        public JsonResult ObterCliente(string nome)
        {
            List<Object> consulta = new List<object>();
            var query = (from c in db.Clientes
                         where c.Nome.Contains(nome)
                         select new
                         {
                             c.Nome
                         });

            foreach (var item in query)
            {
                consulta.Add(new
                {
                    item.Nome
                    //Valor = String.Format("{C:2}", item.Valor.ToString())
                });
            }

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FecharPedido(string valor, string descricao, string cliente, string data, string formaPagamento)
        {
            var i = 0;
            var pedido = db.Pedido.ToList().Take(20);

            var valorR = valor.Replace("R$ ", "");

            //Copiar os pedidos para tabela PedidosFechados            
            IEnumerable<Pedido> enumerable = pedido as IList<Pedido> ?? pedido.ToList();
            try
            {
                var modelo = new PedidoFechado();

                foreach (var detail in enumerable)
                {
                    modelo.codigoProduto = detail.PedidoID;
                    modelo.produto = detail.Produto;
                    modelo.valor = detail.ValorUnit.ToString();
                    modelo.quantidade = detail.Quantia;
                    modelo.desconto = detail.Desconto.ToString();
                    modelo.valorTotal = detail.ValorTotal.ToString();
                    modelo.cliente = cliente;
                    modelo.data = DateTime.Today;
                    i++;

                    if (ModelState.IsValid)
                    {
                        db.PedidoFechado.Add(modelo);
                        db.SaveChanges();
                        TempData["mensagem"] = "Cadastro realizado com sucesso.";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Salvar na tabela vendas os pedidos realizados
            try
            {
                var model02 = new Vendas();
                var count = 1;

                foreach (var detail in enumerable)
                {
                    for (int j = 1; j == count; j++)
                    {
                        model02.codigoProduto = (int) detail.PedidoID;
                        model02.descricao = cliente;
                        model02.valor = detail.ValorUnit.ToString();
                        model02.quantia = i;
                        model02.valorTotal = valor;
                        model02.pedido = detail.PedidoID;
                        model02.pedidoFechado = detail.PedidoID;
                        model02.formaPagamento = formaPagamento;
                        model02.DataVenda = Convert.ToDateTime(data);
                        count = 0;

                        if (ModelState.IsValid)
                        {
                            db.Vendas.Add(model02);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Produtos model = new Produtos();
            //Baixar no estoque de produtos
            try
            {
                foreach (var detail in enumerable)
                {
                    Produtos produto = db.Produtos.Find(int.Parse(detail.Codigo_Produto));

                    var quanProduto = detail.Quantia;
                    var totalEstoque = produto.Quantidade;

                    model.Quantidade = totalEstoque - quanProduto;
                    model.Codigo = produto.Codigo;
                    model.Descricao = produto.Descricao;
                    model.ICMS = produto.ICMS;
                    model.Id = produto.Id;
                    model.Observacao = produto.Observacao;
                    model.Unidade = produto.Unidade;
                    if (produto.Validade == null)
                    {
                        model.Validade = DateTime.Today;
                    }
                    else
                        model.Validade = produto.Validade;
                    model.Valor = produto.Valor;

                    if (ModelState.IsValid)
                    {
                        AlterarCliente(model);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Apagar tabela Pedido

            var consulta = "";

            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }

        private void AlterarCliente(Produtos clienteDTO)
        {
            Produtos existeCliente = db.Produtos.Find(clienteDTO.Id);

            ((IObjectContextAdapter)db).ObjectContext.Detach(existeCliente);

            db.Entry(clienteDTO).State = EntityState.Modified;

            clienteDTO.Id = existeCliente.Id;

            db.SaveChanges();
        }

        [HttpGet]
        public JsonResult CancelarPedido(string criterio)
        {                 

            try
            {
                // Query the database for the rows to be deleted. 
                var deletePedido = from details in db.Pedido
                                   select details;

                foreach (var author in deletePedido)
                {
                    Pedido ped = db.Pedido.Find(author.PedidoID);

                    try
                    {
                        db.Pedido.Remove(ped);
                        db.SaveChanges();                        
                        
                    }
                    catch (DbUpdateException)
                    {
                        db.Entry<Pedido>(ped).State = EntityState.Unchanged;

                        ViewData["mensagem"] = "Não foi possível excluir o registro.";
                    }
                    
                } 
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }

            var consulta = "";

            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterValorTotalPedidos(string criterio)
        {
            List<Object> consulta = new List<object>();
            var query = (from c in db.Pedido
                         select new
                         {
                             Rate = db.Pedido.Sum(n => n.ValorTotal)
                         });

            foreach (var item in query)
            {
                consulta.Add(new
                {
                    Valor = item.Rate.Value.ToString("N2")
                    //Valor = String.Format("{C:2}", item.Valor.ToString())
                });
            }

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterCodigoPedido(string criterio)
        {
            List<Object> consulta = new List<object>();

            var query = (from p in db.Produtos
                         where p.Codigo.Contains(criterio)
                         select new
                         {
                             p.Codigo,
                             p.Descricao,
                             p.Valor
                         });

            foreach (var item in query)
            {
                consulta.Add(new
                {
                    item.Codigo, item.Descricao,
                    Valor = item.Valor.Value.ToString("N2")
                    //Valor = String.Format("{C:2}", item.Valor.ToString())
                });
            }

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterPedido(string criterio)
        {
            List<Object> consulta = new List<object>();
            var query = (from p in db.Produtos
                         where p.Descricao.Contains(criterio)
                         select new
                         {
                             p.Descricao,
                             p.Valor
                         });

            foreach (var item in query)
            {
                consulta.Add(new
                {
                    item.Descricao,
                    Valor = item.Valor.Value.ToString("N2")
                    //Valor = String.Format("{C:2}", item.Valor.ToString())
                });
            }

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterTroco(string dinheiro, string totalPedido, decimal? troco)
        {
            if (dinheiro != "")
            {
                var din = dinheiro.Replace(".", ",");
                var tPed = totalPedido.Replace("R$", "");

                decimal valorDinheiro = decimal.Parse(din);
                decimal totalPed = decimal.Parse(tPed);

                var total = valorDinheiro - totalPed;

                troco = total;
            }
            else
                troco = null;

            return Json(troco.ToString(), JsonRequestBehavior.AllowGet); 
        }


        [HttpGet]
        public JsonResult ObterTotal(string Valor, string quantidade, decimal? queryy)
        {
            if (quantidade != "")
            {
                var val = Valor.Replace(".", ",");

                decimal valor = decimal.Parse(val);
                decimal quant = decimal.Parse(quantidade);

                var total = quant * valor;

                queryy = total;
            }
            else
                queryy = null;

            return Json(queryy.ToString(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterDesconto(string Valor, string desconto, decimal? query)
        {
            if (desconto != "")
            {
                decimal valor = decimal.Parse(Valor);
                decimal desc = decimal.Parse(desconto);

                var percentagem = desc / 100;

                query = valor - (percentagem * valor);
            }
            else
            {
                decimal valor = decimal.Parse(Valor);
                query = valor;
            }

            return Json(query.ToString(), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
