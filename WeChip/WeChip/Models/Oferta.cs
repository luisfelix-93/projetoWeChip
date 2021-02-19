using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeChip.Models
{
    public class Oferta
    {
        [Key]
        public int IdOferta { get; set; }
        public int IdStatus { get; set; }
        public int IdProduto { get; set; }
        public string CPF { get; set; }
    }
}