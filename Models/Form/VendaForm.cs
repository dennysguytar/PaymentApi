using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace payment_api.Models
{
    public class VendaForm
    {
        public int Id { get; set; }
        public Vendedor Vendedor { get; set; }
        public DateTime Data { get; set; }
        public StatusVenda StatusVenda { get; set; }
        public List<ItensVendaForm> ItensVenda { get; set; }
    }
}