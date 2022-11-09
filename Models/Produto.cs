using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace payment_api.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Produto(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
    }
}