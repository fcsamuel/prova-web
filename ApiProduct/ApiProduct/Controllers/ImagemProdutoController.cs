using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProduct.Models;

namespace ApiProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemProdutoController : ControllerBase
    {
        private readonly produtodbContext _context;

        public ImagemProdutoController(produtodbContext context)
        {
            _context = context;
        }

        // GET: api/ImagemProduto
        [HttpGet]
        public IEnumerable<ImagemProduto> GetImagemProduto()
        {
            return _context.ImagemProduto;
        }

        // GET: api/ImagemProduto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImagemProduto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagemProduto = await _context.ImagemProduto.FindAsync(id);

            if (imagemProduto == null)
            {
                return NotFound();
            }

            return Ok(imagemProduto);
        }

        // PUT: api/ImagemProduto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagemProduto([FromRoute] int id, [FromBody] ImagemProduto imagemProduto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imagemProduto.CdImagem)
            {
                return BadRequest();
            }

            _context.Entry(imagemProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagemProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ImagemProduto
        [HttpPost]
        public async Task<IActionResult> PostImagemProduto([FromBody] ImagemProduto imagemProduto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ImagemProduto.Add(imagemProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagemProduto", new { id = imagemProduto.CdImagem }, imagemProduto);
        }

        // DELETE: api/ImagemProduto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagemProduto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagemProduto = await _context.ImagemProduto.FindAsync(id);
            if (imagemProduto == null)
            {
                return NotFound();
            }

            _context.ImagemProduto.Remove(imagemProduto);
            await _context.SaveChangesAsync();

            return Ok(imagemProduto);
        }

        private bool ImagemProdutoExists(int id)
        {
            return _context.ImagemProduto.Any(e => e.CdImagem == id);
        }

        [HttpGet("NextId")]
        public async Task<IActionResult> GetNextId() =>
            Ok(_context.ImagemProduto.Count() + 1);
            


    }
}