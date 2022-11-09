using Microsoft.EntityFrameworkCore;
using payment_api.Models;

namespace payment_api.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {
            
        }

        public DbSet<Venda> Venda { get; set; }
        public DbSet<ItensVenda> ItensVenda { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<Produto> Produto { get; set; }

    }
}