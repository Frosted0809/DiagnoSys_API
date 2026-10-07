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
    public class LabTestsController : ControllerBase
    {
        private readonly DiagnoSysDbContext _context;
        private readonly IMapper _mapper;

        public LabTestsController(DiagnoSysDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/LabTests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTestDto>>> GetLabTests()
        {
            // .Include() pulls in the related Patient and Test data automatically
            //kung meren Related na Patient at Test data sya sa sql hahatakin sa get
            var orders = await _context.LabTests
                .Include(o => o.Patient)
                .Include(o => o.Test)
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<LabTestDto>>(orders));
        }

        // GET: api/LabTests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LabTestDto>> GetLabTest(int id)
        {
            var order = await _context.LabTests
                .Include(o => o.Patient)
                .Include(o => o.Test)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }

            return Ok(_mapper.Map<LabTestDto>(order));
        }

        // POST: api/LabTests
        [HttpPost]
        public async Task<ActionResult<LabTestDto>> CreateLabTest(CreateLabTestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 1. Validate Foreign Keys exist
            // checheck nya lng yung Foreign Keys kung meron dahil sa await at Async
            var patientExists = await _context.ClinicPatients.AnyAsync(p => p.PatientId == createDto.PatientId);
            if (!patientExists)
            {
                return BadRequest(new { message = $"Patient with ID '{createDto.PatientId}' does not exist." });
            }

            var testExists = await _context.LabTestCatalogs.AnyAsync(t => t.TestId == createDto.TestId);
            if (!testExists)
            {
                return BadRequest(new { message = $"Test Catalog item with ID {createDto.TestId} does not exist." });
            }

            // 2. Map and set defaults
            var order = _mapper.Map<LabTest>(createDto);
            order.OrderDate = createDto.OrderDate ?? DateOnly.FromDateTime(DateTime.Today);
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            _context.LabTests.Add(order);
            await _context.SaveChangesAsync();

            // 3. Return rich DTO
            var createdOrder = await _context.LabTests
                .Include(o => o.Patient)
                .Include(o => o.Test)
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            return CreatedAtAction(nameof(GetLabTest), new { id = order.OrderId }, _mapper.Map<LabTestDto>(createdOrder));
        }

        // PUT: api/LabTests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLabTest(int id, UpdateLabTestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.LabTests.FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }

            _mapper.Map(updateDto, order);
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/LabTests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabTest(int id)
        {
            var order = await _context.LabTests.FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }

            _context.LabTests.Remove(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // This catches the SQL Server error if related records (Cbc, Payments, etc.) exist
                //sasalo lng sya kung may error sa sql server na may related na records sa cbc payments na meron gawa
                return BadRequest(new { message = "Cannot delete this order because it has associated test results or payments." });
            }

            return NoContent();
        }
    }
}