using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.Contexts;

namespace APIWeb_SPASentirseBien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ServicioController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicio()
        {
            return await _context.Servicio.ToListAsync();
        }

        // GET: api/Servicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> GetServicio(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        // PUT: api/Servicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, Servicio servicio)
        {
            if (id != servicio.ServicioId)
            {
                return BadRequest();
            }

            _context.Entry(servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id))
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

        // POST: api/Servicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Servicio>> PostServicio(Servicio servicio)
        {
            _context.Servicio.Add(servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServicio", new { id = servicio.ServicioId }, servicio);
        }

        // DELETE: api/Servicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.ServicioId == id);
        }
    }
}
