using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.Contexts;
using AutoMapper;
using APIWeb_SPASentirseBien.Models.DTOs;

namespace APIWeb_SPASentirseBien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public NoticiaController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Noticia
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NoticiaDTO>>> GetNoticia()
        {
            var noticias = await _context.Noticia.ToListAsync();
            var noticiasDTO = _mapper.Map<List<NoticiaDTO>>(noticias);
            return noticiasDTO;
        }


        // GET: api/Noticia/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NoticiaDTO>> GetNoticia(int id)
        {
            var noticia = await _context.Noticia.FindAsync(id);
            
            if (id < 0)
            {
                return BadRequest();
            }
            if (noticia == null)
            {
                return NotFound();
            }

            var noticiaDTO = _mapper.Map<NoticiaDTO>(noticia);
            return noticiaDTO;
        }

        // PUT: api/Noticia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public async Task<IActionResult> PutNoticia(int id, NoticiaDTO noticiaDTO)
        {
            if (id != noticiaDTO.NoticiaId)
            {
                return BadRequest();
            }

            var noticia = _mapper.Map<Noticia>(noticiaDTO);

            _context.Entry(noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
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

        // POST: api/Noticia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Noticia>> PostNoticia(NoticiaDTO noticiaDTO)
        {
            if (noticiaDTO == null)
            {
                return BadRequest();
            }
            var noticia = _mapper.Map<Noticia>(noticiaDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Noticia.Add(noticia);
            await _context.SaveChangesAsync();

            var noticiaToReturn = _mapper.Map<NoticiaDTO>(noticia); 

            return CreatedAtAction("GetNoticia", new { id = noticiaToReturn.NoticiaId }, noticiaToReturn);
        }

        // DELETE: api/Noticia/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNoticia(int id)
        {
            var noticia = await _context.Noticia.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }

            _context.Noticia.Remove(noticia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticia.Any(e => e.NoticiaId == id);
        }
    }
}
