using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using payment_api.Context;
using payment_api.Models;

namespace payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public VendaController(OrganizadorContext context)
        {
            _context = context;
        }

        protected StatusVenda GetStatusVenda(int id)
        {
            List<StatusVenda> getStatusVenda = new List<StatusVenda>();
            getStatusVenda.Add(new StatusVenda(0, "Aguardando pagamento"));
            getStatusVenda.Add(new StatusVenda(1, "Pagamento aprovado"));
            getStatusVenda.Add(new StatusVenda(2, "Enviado para transportadora"));
            getStatusVenda.Add(new StatusVenda(3, "Entregue"));
            getStatusVenda.Add(new StatusVenda(4, "Cancelada"));
            return getStatusVenda.Find(p => p.Id == id);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var venda = _context.Venda.Find(id);

            if (venda == null)
                return NotFound();

            var itensVenda = _context.ItensVenda.Where(p => p.VendaId == venda.Id).ToList();

            List<ItensVendaForm> itensVendaForm = new List<ItensVendaForm>();
            foreach(ItensVenda itensVendas in itensVenda)
            {
                var produto = _context.Produto.Find(itensVendas.ProdutoId);
                itensVendaForm.Add(new ItensVendaForm(itensVendas.Id, produto, itensVendas.Qtde));
            }

            VendaForm vendaForm = new VendaForm();

            vendaForm.Id = venda.Id;
            vendaForm.Vendedor = _context.Vendedor.Find(venda.VendedorId);
            vendaForm.Data = venda.Data;
            vendaForm.StatusVenda = GetStatusVenda(venda.IdStatusVenda);
            vendaForm.ItensVenda = itensVendaForm;

            return Ok(vendaForm);
        }

        [HttpPost]
        public IActionResult Criar(Venda venda)
        {
            if (venda.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            venda.IdStatusVenda = 0;
            
            _context.Add(venda);
            _context.SaveChanges();
            //return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);

            var itensVenda = _context.ItensVenda.Where(p => p.VendaId == venda.Id).ToList();

            List<ItensVendaForm> itensVendaForm = new List<ItensVendaForm>();
            foreach(ItensVenda itensVendas in itensVenda)
            {
                var produto = _context.Produto.Find(itensVendas.ProdutoId);
                itensVendaForm.Add(new ItensVendaForm(itensVendas.Id, produto, itensVendas.Qtde));
            }

            VendaForm vendaForm = new VendaForm();

            vendaForm.Id = venda.Id;
            vendaForm.Vendedor = _context.Vendedor.Find(venda.VendedorId);
            vendaForm.Data = venda.Data;
            vendaForm.StatusVenda = GetStatusVenda(venda.IdStatusVenda);
            vendaForm.ItensVenda = itensVendaForm;

            return Ok(vendaForm);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, int idStatusVenda)
        {
            var vendaBanco = _context.Venda.Find(id);

            if (vendaBanco == null)
                return NotFound();
            /*
            0 = Aguardando pagamento
            1 = Pagamento aprovado
            2 = Enviado para transportadora
            3 = Entregue
            4 = Cancelada

            De: Aguardando pagamento       Para: Pagamento Aprovado
            De: Aguardando pagamento       Para: Cancelada
            De: Pagamento Aprovado         Para: Enviado para Transportadora
            De: Pagamento Aprovado         Para: Cancelada
            De: Enviado para Transportador Para: Entregue
            */
            if (vendaBanco.IdStatusVenda == 0 && ( idStatusVenda != 1 && idStatusVenda != 4 ) )
                return BadRequest(new { Erro = "Status inválido!" });
            
            if (vendaBanco.IdStatusVenda == 1 && ( idStatusVenda != 2 && idStatusVenda != 4 ) )
                return BadRequest(new { Erro = "Status inválido!" });
            
            if (vendaBanco.IdStatusVenda == 2 && idStatusVenda != 3 )
                return BadRequest(new { Erro = "Status inválido!" });
            
            if (vendaBanco.IdStatusVenda == 3 )
                return BadRequest(new { Erro = "Status não pode ser alterado!" });

            if (vendaBanco.IdStatusVenda == 4 )
                return BadRequest(new { Erro = "Status não pode ser alterado!" });

            vendaBanco.IdStatusVenda = idStatusVenda;

            _context.Venda.Update(vendaBanco);
            _context.SaveChanges();
            return Ok();
        }
    }
}