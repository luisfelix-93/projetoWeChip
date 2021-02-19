using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeChip.Models
{
    public class Status
    { 
        [Key]
        public int StatusId { get; set; }
        public string DescricaoStatus { get; set; }
        public bool FinalizaCliente { get; set; }
        public bool ContabCliente { get; set; }

        
    }
}