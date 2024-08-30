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
    public class RespuestaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public RespuestaController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Respuesta
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RespuestaDTO>>> GetRespuesta()
        {
            var respuesta = await _context.Respuesta.ToListAsync();
            var respuestaDTO = _mapper.Map<List<RespuestaDTO>>(respuesta);
            return respuestaDTO;
        }

        // Limited GET: api/Respuesta/limitado
        [HttpGet("limitado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RespuestaDTO>>> GetLimitedRespuestas([FromQuery] int count = 10)
        {
            if (count <= 0)
            {
                return BadRequest("El parámetro 'count' debe ser mayor que 0.");
            }

            var respuestas = await _context.Respuesta
                                        .OrderByDescending(n => n.RespuestaId) 
                                        .Take(count)
                                        .ToListAsync();

            var respuestasDTO = _mapper.Map<List<RespuestaDTO>>(respuestas);

            return Ok(respuestasDTO);
        }

        // GET: api/Respuesta/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RespuestaDTO>> GetRespuesta(int id)
        {
            var respuesta = await _context.Respuesta.FindAsync(id);
            
            if (id < 0)
            {
                return BadRequest();
            }
            if (respuesta == null)
            {
                return NotFound();
            }

            var respuestaDTO = _mapper.Map<RespuestaDTO>(respuesta);
            return respuestaDTO;
        }

        // PUT: api/Respuesta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> PutRespuesta(int id, RespuestaDTO respuestaDTO)
        {
            if (id != respuestaDTO.RespuestaId)
            {
                return BadRequest();
            }

            var respuesta = _mapper.Map<Respuesta>(respuestaDTO);

            _context.Entry(respuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespuestaExists(id))
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

        // PATCH: api/Respuesta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public async Task<IActionResult> PatchRespuesta(int id, [FromBody] JsonPatchDocument<RespuestaPatchDTO> respuestaDTOPatch)
        {
            if (respuestaDTOPatch == null)
            {
                return BadRequest();
            }

            var respuesta = await _context.Respuesta.FindAsync(id);

            if (respuesta == null)
            {
                return NotFound();
            }

            var respuestaDTO = _mapper.Map<RespuestaPatchDTO>(respuesta);

            respuestaDTOPatch.ApplyTo(respuestaDTO, ModelState);
            _mapper.Map(respuestaDTO, respuesta);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespuestaExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Respuesta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RespuestaDTO>> PostRespuesta(RespuestaDTO respuestaDTO)
        {
            if (respuestaDTO == null)
            {
                return BadRequest();
            }
            var respuesta = _mapper.Map<Respuesta>(respuestaDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Respuesta.Add(respuesta);
            await _context.SaveChangesAsync();

            var respuestaToReturn = _mapper.Map<RespuestaDTO>(respuesta); 

            return CreatedAtAction("GetRespuesta", new { id = respuestaToReturn.RespuestaId }, respuestaToReturn);
        }

        // DELETE: api/Respuesta/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRespuesta(int id)
        {
            var respuesta = await _context.Respuesta.FindAsync(id);
            if (respuesta == null)
            {
                return NotFound();
            }

            _context.Respuesta.Remove(respuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RespuestaExists(int id)
        {
            return _context.Respuesta.Any(e => e.RespuestaId == id);
        }
    }
}
