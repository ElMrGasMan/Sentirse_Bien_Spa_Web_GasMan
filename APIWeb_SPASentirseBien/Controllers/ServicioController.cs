using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWeb_SPASentirseBien.Models;
using APIWeb_SPASentirseBien.Models.Contexts;
using AutoMapper;
using APIWeb_SPASentirseBien.Models.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using APIWeb_SPASentirseBien.Models.DTOs.PatchDTOs;

namespace APIWeb_SPASentirseBien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ServicioController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Servicio
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ServicioDTO>>> GetServicio()
        {
            var servicio = await _context.Servicio.ToListAsync();
            var servicioDTO = _mapper.Map<List<ServicioDTO>>(servicio);
            return servicioDTO;
        }

        // GET: api/Servicio/tipo/{tipo}
        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ServicioDTO>>> GetServicio([FromQuery] string tipo)
        {
            if (tipo == null)
            {
                return BadRequest();
            }
            var servicios = await _context.Servicio.Where(s => s.TipoServicio == tipo).ToListAsync();

            if (servicios == null || servicios.Count == 0)
            {
                return NotFound();
            }

            var servicioDtos = _mapper.Map<List<ServicioDTO>>(servicios);
            return Ok(servicioDtos);
        }

        // GET: api/Servicio/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServicioDTO>> GetServicio(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            
            if (id < 0)
            {
                return BadRequest();
            }
            if (servicio == null)
            {
                return NotFound();
            }

            var servicioDTO = _mapper.Map<ServicioDTO>(servicio);
            return servicioDTO;
        }

        // PUT: api/Servicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> PutServicio(int id, ServicioDTO servicioDTO)
        {
            if (id != servicioDTO.ServicioId)
            {
                return BadRequest();
            }

            var Servicio = _mapper.Map<Servicio>(servicioDTO);

            _context.Entry(Servicio).State = EntityState.Modified;

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

        // PATCH: api/Servicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public async Task<IActionResult> PatchServicio(int id, [FromBody] JsonPatchDocument<ServicioPatchDTO> servicioDTOPatch)
        {
            if (servicioDTOPatch == null)
            {
                return BadRequest();
            }

            var servicio = await _context.Servicio.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            var servicioDTO = _mapper.Map<ServicioPatchDTO>(servicio);

            servicioDTOPatch.ApplyTo(servicioDTO, ModelState);
            _mapper.Map(servicioDTO, servicio);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Servicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServicioDTO>> PostServicio(ServicioDTO servicioDTO)
        {
            if (servicioDTO == null)
            {
                return BadRequest();
            }
            var servicio = _mapper.Map<Servicio>(servicioDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Servicio.Add(servicio);
            await _context.SaveChangesAsync();

            var servicioToReturn = _mapper.Map<ServicioDTO>(servicio); 

            return CreatedAtAction("GetServicio", new { id = servicioToReturn.ServicioId }, servicioToReturn);
        }

        // DELETE: api/Servicio/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
