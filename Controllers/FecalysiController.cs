using AutoMapper;
using DiagnoSys_API.Data;
using DiagnoSys_API.DTOs;
using DiagnoSys_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiagnoSys_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FecalysiController : ControllerBase
    {
        private readonly DiagnoSysDbContext _context;
        private readonly IMapper _mapper;

        public FecalysiController(DiagnoSysDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Fecalysi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FecalysiDto>>> GetAllFecalysi()
        {
            var results = await _context.Fecalyses // papalitan ule tulad ng ginawa ko sa Urinalysis papalitan para tumulad sa DbContext Data
                .Include(f => f.Order)
                    .ThenInclude(o => o.Patient)
                .Include(f => f.Order)
                    .ThenInclude(o => o.Test)
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<FecalysiDto>>(results));
        }

        // GET: api/Fecalysi/1
        [HttpGet("{id}")]
        public async Task<ActionResult<FecalysiDto>> GetFecalysi(int id)
        {
            var result = await _context.Fecalyses 
                .Include(f => f.Order)
                    .ThenInclude(o => o.Patient)
                .Include(f => f.Order)
                    .ThenInclude(o => o.Test)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FaId == id);

            if (result == null)
            {
                return NotFound(new { message = $"Fecalysis result with ID {id} not found." });
            }

            return Ok(_mapper.Map<FecalysiDto>(result));
        }

        // POST: api/Fecalysi
        [HttpPost]
        public async Task<ActionResult<FecalysiDto>> CreateFecalysi(CreateFecalysiDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate Order exists
            var orderExists = await _context.LabTests.AnyAsync(o => o.OrderId == createDto.OrderId);
            if (!orderExists)
            {
                return BadRequest(new { message = $"Order with ID {createDto.OrderId} does not exist." });
            }

            // Check for duplicate (1-to-1 constraint)
            var alreadyExists = await _context.Fecalyses.AnyAsync(f => f.OrderId == createDto.OrderId); 
            if (alreadyExists)
            {
                return Conflict(new { message = $"Order {createDto.OrderId} already has a Fecalysis result." });
            }

            var fa = _mapper.Map<Fecalysi>(createDto);
            fa.CreatedAt = DateTime.UtcNow;
            fa.UpdatedAt = DateTime.UtcNow;

            _context.Fecalyses.Add(fa); 
            await _context.SaveChangesAsync();

            // Return rich DTO
            var created = await _context.Fecalyses 
                .Include(f => f.Order)
                    .ThenInclude(o => o.Patient)
                .Include(f => f.Order)
                    .ThenInclude(o => o.Test)
                .FirstOrDefaultAsync(f => f.FaId == fa.FaId);

            return CreatedAtAction(nameof(GetFecalysi), new { id = fa.FaId }, _mapper.Map<FecalysiDto>(created));
        }

        // PUT: api/Fecalysi/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFecalysi(int id, UpdateFecalysiDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fa = await _context.Fecalyses.FirstOrDefaultAsync(f => f.FaId == id); 
            if (fa == null)
            {
                return NotFound(new { message = $"Fecalysis result with ID {id} not found." });
            }

            _mapper.Map(updateDto, fa);
            fa.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Fecalysi/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFecalysi(int id)
        {
            var fa = await _context.Fecalyses.FirstOrDefaultAsync(f => f.FaId == id);
            if (fa == null)
            {
                return NotFound(new { message = $"Fecalysis result with ID {id} not found." });
            }

            _context.Fecalyses.Remove(fa); 
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}