using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeChip.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; }

        public virtual bool Selecionado { get; set; }//Variavel usada para controle os produtos selecionados para cada cliente na oferta
    }
}
