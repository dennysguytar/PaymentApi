using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace payment_api.Models
{
    public class ItensVenda
    {
        [Key]
        public int Id { get; set; }
        public int VendaId { get; set; }
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public int Qtde { get; set; }
    }
}