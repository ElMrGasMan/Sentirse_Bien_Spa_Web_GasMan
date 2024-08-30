using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.Contexts;

namespace APIWeb_SPASentirseBien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TurnoController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Turno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turno>>> GetTurno()
        {
            return await _context.Turno.ToListAsync();
        }

        // GET: api/Turno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turno>> GetTurno(int id)
        {
            var turno = await _context.Turno.FindAsync(id);

            if (turno == null)
            {
                return NotFound();
            }

            return turno;
        }

        // PUT: api/Turno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /* Método PUT probablemente no necesario, un turno no debería poder cambiarse, lo dejo por las dudas */
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurno(int id, Turno turno)
        {
            if (id != turno.TurnoId)
            {
                return BadRequest();
            }

            _context.Entry(turno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnoExists(id))
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
        */

        // POST: api/Turno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Turno>> PostTurno(Turno turno)
        {
            _context.Turno.Add(turno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurno", new { id = turno.TurnoId }, turno);
        }

        // DELETE: api/Turno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurno(int id)
        {
            var turno = await _context.Turno.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }

            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TurnoExists(int id)
        {
            return _context.Turno.Any(e => e.TurnoId == id);
        }
    }
}
