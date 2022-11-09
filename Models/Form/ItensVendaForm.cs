using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace payment_api.Models
{
    public class ItensVendaForm
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int Qtde { get; set; }

        public ItensVendaForm(int id, Produto produto, int qtde)
        {
            this.Id = id;
            this.Produto = produto;
            this.Qtde = qtde;
        }
    }
}