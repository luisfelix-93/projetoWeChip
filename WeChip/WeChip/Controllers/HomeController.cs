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
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            if (db.Produto.ToList().Count() == 0)
            {
                db.Produto.Add(new Produto()
                {
                    DescricaoProduto = "Mouse",
                    Preco = 20,
                    Tipo = "HARDWARE"
                });
                db.Produto.Add(new Produto()
                {
                    DescricaoProduto = "Teclado",
                    Preco = 30,
                    Tipo = "HARDWARE"
                });
                db.Produto.Add(new Produto()
                {
                    DescricaoProduto = "Monitor 17’",
                    Preco = 350,
                    Tipo = "HARDWARE"
                });
                db.SaveChanges();
            }
            if (db.Status.ToList().Count() == 0)
            {
                db.Status.Add(new Status()
                {
                    DescricaoStatus = "Nome Disponível",
                    FinalizaCliente = false,
                    ContabCliente = false
                });
                db.Status.Add(new Status()
                {
                    DescricaoStatus = "Cliente Aceitou Oferta",
                    FinalizaCliente = true,
                    ContabCliente = true
                });
                db.SaveChanges();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}