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
    public class TurnoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TurnoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Turno
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetTurno()
        {
            var turnos = await _context.Turno.ToListAsync();
            var turnosDTO = _mapper.Map<List<TurnoDTO>>(turnos);
            return turnosDTO;
        }

        // Limited GET: api/Turno/limitado
        [HttpGet("limitado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetLimitedTurnos([FromQuery] int count = 10)
        {
            if (count <= 0)
            {
                return BadRequest("El parámetro 'count' debe ser mayor que 0.");
            }

            var turnos = await _context.Turno
                                        .OrderByDescending(n => n.TurnoId) 
                                        .Take(count)
                                        .ToListAsync();

            var turnosDTO = _mapper.Map<List<TurnoDTO>>(turnos);

            return Ok(turnosDTO);
        }

        // GET: api/Turno/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TurnoDTO>> GetTurno(int id)
        {
            var turno = await _context.Turno.FindAsync(id);
            
            if (id < 0)
            {
                return BadRequest();
            }
            if (turno == null)
            {
                return NotFound();
            }

            var turnoDTO = _mapper.Map<TurnoDTO>(turno);
            return turnoDTO;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TurnoDTO>> PostTurno(TurnoDTO turnoDTO)
        {
            if (turnoDTO == null)
            {
                return BadRequest();
            }
            var turno = _mapper.Map<Turno>(turnoDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Turno.Add(turno);
            await _context.SaveChangesAsync();

            var turnoToReturn = _mapper.Map<TurnoDTO>(turno); 

            return CreatedAtAction("GetTurno", new { id = turnoToReturn.TurnoId }, turnoToReturn);
        }

        // DELETE: api/Turno/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
