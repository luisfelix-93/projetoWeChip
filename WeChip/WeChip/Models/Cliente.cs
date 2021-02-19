using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeChip.Models
{
    public class Cliente
    {
        [Key]
       public int IdCliente { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Telefone { get; set; }
        public int Credito { get; set; }
        public int IdStatus { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public virtual string DescricaoStatus { get; set; }
        public virtual List<Produto> ListaProdutos { get; set; }
    }
}