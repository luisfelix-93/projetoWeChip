using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeChip.Models;

namespace WeChip.Controllers
{
    public class ClientesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Clientes
        public ActionResult Index()
        {
            List<Cliente> ListaClientes = db.Cliente.ToList();
            foreach (var item in ListaClientes)
            {
                item.DescricaoStatus = db.Status.Where(x => x.StatusId == item.IdStatus).ToList().FirstOrDefault().DescricaoStatus;
            }
            return View(ListaClientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCliente,Nome,Cpf,Telefone,Credito,IdStatus,Cep,Rua,Numero,Complemento,Bairro,Cidade,Estado,DescricaoStatus")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //Add Regra: No momento do cadastro deverá ser salvo com status “Nome Disponível”                Obs “Nome Disponível” é uma coluna da tabela STATUS(item 4.1.2 Status).
                cliente.IdStatus = 1;//IdStatus 1 = NomeDisponivel
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Status = db.Status.ToList().OrderBy(x => x.DescricaoStatus);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            else
            {
                cliente.DescricaoStatus = db.Status.Where(x => x.StatusId == cliente.IdStatus).ToList().FirstOrDefault().DescricaoStatus;
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,Nome,Cpf,Telefone,Credito,IdStatus,Cep,Rua,Numero,Complemento,Bairro,Cidade,Estado,DescricaoStatus")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
