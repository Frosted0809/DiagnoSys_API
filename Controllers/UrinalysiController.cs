using AutoMapper;
using DiagnoSys_API.Data;
using DiagnoSys_API.DTOs;
using DiagnoSys_API.Models; // Change to DiagnoSys_API.Data if needed
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiagnoSys_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrinalysiController : ControllerBase
    {
        private readonly DiagnoSysDbContext _context;
        private readonly IMapper _mapper;

        public UrinalysiController(DiagnoSysDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Urinalysi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrinalysiDto>>> GetAllUrinalysis()
        {
            var results = await _context.Urinalyses //bingago ko lahat dapat ng _context.Urinalysi dapat Urinalyses dahil sa Db Context
                .Include(u => u.Order)              // medyo quirky pala pluralization ni EF core nung ni auto generate sya...
                    .ThenInclude(o => o.Patient)
                .Include(u => u.Order)
                    .ThenInclude(o => o.Test)
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<UrinalysiDto>>(results));
        }

        // GET: api/Urinalysi/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UrinalysiDto>> GetUrinalysis(int id)
        {
            var result = await _context.Urinalyses
                .Include(u => u.Order)
                    .ThenInclude(o => o.Patient)
                .Include(u => u.Order)
                    .ThenInclude(o => o.Test)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UaId == id);

            if (result == null)
            {
                return NotFound(new { message = $"Urinalysis result with ID {id} not found." });
            }

            return Ok(_mapper.Map<UrinalysiDto>(result));
        }

        // POST: api/Urinalysi
        [HttpPost]
        public async Task<ActionResult<UrinalysiDto>> CreateUrinalysis(CreateUrinalysiDto createDto)
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
            var alreadyExists = await _context.Urinalyses.AnyAsync(u => u.OrderId == createDto.OrderId);
            if (alreadyExists)
            {
                return Conflict(new { message = $"Order {createDto.OrderId} already has a Urinalysis result." });
            }

            var ua = _mapper.Map<Urinalysi>(createDto);
            ua.CreatedAt = DateTime.UtcNow;
            ua.UpdatedAt = DateTime.UtcNow;

            _context.Urinalyses.Add(ua);
            await _context.SaveChangesAsync();

            // Return rich DTO
            var created = await _context.Urinalyses
                .Include(u => u.Order)
                    .ThenInclude(o => o.Patient)
                .Include(u => u.Order)
                    .ThenInclude(o => o.Test)
                .FirstOrDefaultAsync(u => u.UaId == ua.UaId);

            return CreatedAtAction(nameof(GetUrinalysis), new { id = ua.UaId }, _mapper.Map<UrinalysiDto>(created));
        }

        // PUT: api/Urinalysi/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUrinalysis(int id, UpdateUrinalysiDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ua = await _context.Urinalyses.FirstOrDefaultAsync(u => u.UaId == id);
            if (ua == null)
            {
                return NotFound(new { message = $"Urinalysis result with ID {id} not found." });
            }

            _mapper.Map(updateDto, ua);
            ua.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Urinalysi/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrinalysis(int id)
        {
            var ua = await _context.Urinalyses.FirstOrDefaultAsync(u => u.UaId == id);
            if (ua == null)
            {
                return NotFound(new { message = $"Urinalysis result with ID {id} not found." });
            }

            _context.Urinalyses.Remove(ua);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}