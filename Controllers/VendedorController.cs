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
    public class VendedorController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public VendedorController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var vendedor = _context.Vendedor.Find(id);

            if (vendedor == null)
                return NotFound();

            return Ok(vendedor);
        }

        [HttpPost]
        public IActionResult Criar(Vendedor vendedor)
        {
            if (vendedor.Nome == null || vendedor.Nome == "" )
                return BadRequest(new { Erro = "O nome é obrigatório" });
            if (vendedor.Cpf == null || vendedor.Cpf == "" )
                return BadRequest(new { Erro = "O CPF é obrigatório" });

            _context.Add(vendedor);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = vendedor.Id }, vendedor);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Vendedor vendedor)
        {
            var vendedorBanco = _context.Vendedor.Find(id);

            if (vendedorBanco == null)
                return NotFound();

            if (vendedor.Nome == null || vendedor.Nome == "" )
                return BadRequest(new { Erro = "O nome é obrigatório" });
            if (vendedor.Cpf == null || vendedor.Cpf == "" )
                return BadRequest(new { Erro = "O CPF é obrigatório" });

            vendedorBanco.Nome = vendedor.Nome;
            vendedorBanco.Cpf = vendedor.Cpf;
            vendedorBanco.Email = vendedor.Email;
            vendedorBanco.Telefone = vendedor.Telefone;

            _context.Vendedor.Update(vendedorBanco);
            _context.SaveChanges();
            return Ok();
        }
    }
}