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
    public class ProdutoController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public ProdutoController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var produto = _context.Produto.Find(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Criar(Produto produto)
        {
            if (produto.Nome == null || produto.Nome == "" )
                return BadRequest(new { Erro = "O nome é obrigatório" });
            
            _context.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Produto produto)
        {
            var produtoBanco = _context.Produto.Find(id);

            if (produtoBanco == null)
                return NotFound();

            if (produto.Nome == null || produto.Nome == "" )
                return BadRequest(new { Erro = "O nome é obrigatório" });

            produtoBanco.Nome = produto.Nome;

            _context.Produto.Update(produtoBanco);
            _context.SaveChanges();
            return Ok();
        }
    }
}