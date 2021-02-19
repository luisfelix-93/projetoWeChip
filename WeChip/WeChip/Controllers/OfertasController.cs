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
    public class OfertasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Ofertas
        public ActionResult Index(Cliente cliente)
        {
            //O parametro cliente.Nome recebe nome/CPF, necessário fazer validação
            string NomeOuCPF = cliente.Nome;
            //Validação para tentar encontrar no Nome ou CPF na lista de clientes
            if (String.IsNullOrEmpty(NomeOuCPF) == false)
            {
                //Filtrando por Nome
                cliente = db.Cliente.Where(c => c.Nome == NomeOuCPF).ToList().FirstOrDefault();
                //Checando se esse cliente buscado é null, quer dizer que nada foi encontrado pelo nome comparado ao que o user digitou
                if (cliente == null)
                {
                    //Filtrando por CPf
                    cliente = db.Cliente.Where(c => c.Cpf == NomeOuCPF).ToList().FirstOrDefault();
                }
            }
            //Checando se esse cliente buscado é null, quer dizer que nada foi encontrado pelo cpf comparado ao que o user digitou
            if (cliente == null)
            {
                //Criar instancia caso não encontre nenhume cliente com ese valor que o user digitou
                cliente = new Cliente();
            }

            //Validação para saber se o status desse cliente encontrado permite finaliza cliente
            if (cliente.IdStatus != 0 && db.Status.Find(cliente.IdStatus).FinalizaCliente == true)
            {
                ViewBag.Msg = "O Cliente " + cliente.Nome +" nao pode ter ofertas pois o Status contem o Finaliza Cliente.";
            }
            else
            {
                cliente.ListaProdutos = db.Produto.ToList();
                //Adicionando valores na viewbag para nao quebrar a pagina
                ViewBag.Total = 0.0;
                ViewBag.Msg = null;

                //Adicionando validação para  exibir msg de erro na pesquisa
                if (cliente.IdCliente == 0)
                {
                    cliente.Nome = NomeOuCPF;
                }
            }
            //Adicionando validação para exibir status do cliente
            if (cliente.IdStatus == 0)
            {
                cliente.DescricaoStatus = "Sem status definido";
            }
            else
            {
                cliente.DescricaoStatus = db.Status.Where(s => s.StatusId == cliente.IdStatus).ToList().FirstOrDefault().DescricaoStatus;
            }
            return View(cliente);
        }

        public ActionResult Selecionar(int? id, int? idCliente, string lista)
        {
            List<Produto> listProd = System.Web.Helpers.Json.Decode<List<Produto>>(lista);//Metodo que transforma um JSON no formato  string numa lista
            Cliente _cliente = db.Cliente.Find(idCliente);
            //Percorrendo toda a lista de produtos e setando o valor selecionado para o que o user selecionou
            foreach (var item in listProd)
            {
                if (item.IdProduto == id)
                {
                    item.Selecionado = true;

                }
            }
            _cliente.ListaProdutos = listProd;

            decimal valorTotal = 0;
            //Percorreendo toda a lista de produtos, mas desta vez, somando todos os valores o produtos que já foram seleconados anteriormente
            foreach (var item in _cliente.ListaProdutos)
            {
                if (item.Selecionado)
                {
                    valorTotal = valorTotal + item.Preco;
                }
            }
            ViewBag.Total = valorTotal;//Adicionando o valor final na viewbag para mandaar para a VIEW
            return View("Index", _cliente);
        }

        public ActionResult OfertarCliente(int idStatus, int idProduto, string CPF, decimal Valor, int Credito)
        {
            Cliente _cliente = db.Cliente.Where(c => c.Cpf == CPF).ToList().FirstOrDefault();
            //Validações para Oferta
            if (db.Status.Find(idStatus).ContabCliente == false)
            {
                ViewBag.Msg = "O Status nao pode contabilizar a venda.";
            }
            else if (Valor > Credito)
            {
                ViewBag.Msg = "O Valor total de produtos e maior que o credito.";
            }
            else if (db.Produto.Find(idProduto).Tipo == "HARDWARE" &&  String.IsNullOrEmpty(_cliente.Rua))
            {
                ViewBag.Msg = "E obrigatorio caso seja selecionado algum produto do tipo HARDWARE.";
            }
            else
            {
                //Caso não caia em nenhuma validação, adiciona na table de ofertas para ser listado na apiRest
                db.Oferta.Add(new Oferta()
                {
                    IdProduto = idProduto,
                    IdStatus = idStatus,
                    CPF = CPF
                });
                db.SaveChanges();
                // Remover a quantidade de creditos do cliente
                _cliente.Credito = _cliente.Credito - Credito;
                db.Entry(_cliente).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Msg = "Oferta Salva com sucesso";
            }
            return View("Index", _cliente);
        }

        
    }
}