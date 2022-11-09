using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace payment_api.Models
{
    public class Venda
    {
        public int Id { get; set; }

        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public DateTime Data { get; set; }
        public int IdStatusVenda { get; set; }
        public List<ItensVenda> ItensVenda { get; set; }
    }
}