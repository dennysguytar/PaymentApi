using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace payment_api.Models
{
    public class StatusVenda
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public StatusVenda(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
    }
}