using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WeChip.Models
{
    public class Contexto: DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Oferta> Oferta { get; set; }
    }
}