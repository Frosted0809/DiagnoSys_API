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
    public class CbcController : ControllerBase
    {
        private readonly DiagnoSysDbContext _context;
        private readonly IMapper _mapper;

        public CbcController(DiagnoSysDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cbc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CbcDto>>> GetAllCbc()
        {
            var results = await _context.Cbcs
                .Include(c => c.Order)
                    .ThenInclude(o => o.Patient)
                .Include(c => c.Order)
                    .ThenInclude(o => o.Test)
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<CbcDto>>(results));
        }

        // GET: api/Cbc/1
        [HttpGet("{id}")]
        public async Task<ActionResult<CbcDto>> GetCbc(int id)
        {
            var result = await _context.Cbcs
                .Include(c => c.Order)
                    .ThenInclude(o => o.Patient)
                .Include(c => c.Order)
                    .ThenInclude(o => o.Test)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CbcId == id);

            if (result == null)
            {
                return NotFound(new { message = $"CBC result with ID {id} not found." });
            }

            return Ok(_mapper.Map<CbcDto>(result));
        }

        // POST: api/Cbc
        [HttpPost]
        public async Task<ActionResult<CbcDto>> CreateCbc(CreateCbcDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate that the Order exists
            //checheck lng kung meron order na nagawa o mero na
            var orderExists = await _context.LabTests.AnyAsync(o => o.OrderId == createDto.OrderId);
            if (!orderExists)
            {
                return BadRequest(new { message = $"Order with ID {createDto.OrderId} does not exist." });
            }

            // Check if this order already has a CBC result (1-to-1 relationship)
            // kung yung cbc result may relationship na sa iba entity
            var alreadyExists = await _context.Cbcs.AnyAsync(c => c.OrderId == createDto.OrderId);
            if (alreadyExists)
            {
                return Conflict(new { message = $"Order {createDto.OrderId} already has a CBC result." });
            }

            var cbc = _mapper.Map<Cbc>(createDto);
            cbc.CreatedAt = DateTime.UtcNow;
            cbc.UpdatedAt = DateTime.UtcNow;

            _context.Cbcs.Add(cbc);
            await _context.SaveChangesAsync();

            // Return the full DTO with nested order info
            var created = await _context.Cbcs
                .Include(c => c.Order)
                    .ThenInclude(o => o.Patient)
                .Include(c => c.Order)
                    .ThenInclude(o => o.Test)
                .FirstOrDefaultAsync(c => c.CbcId == cbc.CbcId);

            return CreatedAtAction(nameof(GetCbc), new { id = cbc.CbcId }, _mapper.Map<CbcDto>(created));
        }

        // PUT: api/Cbc/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCbc(int id, UpdateCbcDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cbc = await _context.Cbcs.FirstOrDefaultAsync(c => c.CbcId == id);
            if (cbc == null)
            {
                return NotFound(new { message = $"CBC result with ID {id} not found." });
            }

            _mapper.Map(updateDto, cbc);
            cbc.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Cbc/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCbc(int id)
        {
            var cbc = await _context.Cbcs.FirstOrDefaultAsync(c => c.CbcId == id);
            if (cbc == null)
            {
                return NotFound(new { message = $"CBC result with ID {id} not found." });
            }

            _context.Cbcs.Remove(cbc);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}