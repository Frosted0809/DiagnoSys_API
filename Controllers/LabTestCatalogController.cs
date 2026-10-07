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
    public class LabTestCatalogController : ControllerBase
    {
        private readonly DiagnoSysDbContext _context;
        private readonly IMapper _mapper;

        public LabTestCatalogController(DiagnoSysDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/LabTestCatalog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTestCatalogDto>>> GetCatalog()
        {
            var items = await _context.LabTestCatalogs.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<LabTestCatalogDto>>(items));
        }

        // GET: api/LabTestCatalog/1
        [HttpGet("{id}")]
        public async Task<ActionResult<LabTestCatalogDto>> GetCatalogItem(int id)
        {
            var item = await _context.LabTestCatalogs.AsNoTracking()
                .FirstOrDefaultAsync(i => i.TestId == id);

            if (item == null)
            {
                return NotFound(new { message = $"Test catalog item with ID {id} not found." });
            }

            return Ok(_mapper.Map<LabTestCatalogDto>(item));
        }

        // POST: api/LabTestCatalog
        [HttpPost]
        public async Task<ActionResult<LabTestCatalogDto>> CreateCatalogItem(CreateLabTestCatalogDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _mapper.Map<LabTestCatalog>(createDto);
            _context.LabTestCatalogs.Add(item);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<LabTestCatalogDto>(item);
            return CreatedAtAction(nameof(GetCatalogItem), new { id = item.TestId }, resultDto);
        }

        // PUT: api/LabTestCatalog/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatalogItem(int id, UpdateLabTestCatalogDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingItem = await _context.LabTestCatalogs.FirstOrDefaultAsync(i => i.TestId == id);
            if (existingItem == null)
            {
                return NotFound(new { message = $"Test catalog item with ID {id} not found." });
            }

            _mapper.Map(updateDto, existingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/LabTestCatalog/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogItem(int id)
        {
            var item = await _context.LabTestCatalogs.FirstOrDefaultAsync(i => i.TestId == id);
            if (item == null)
            {
                return NotFound(new { message = $"Test catalog item with ID {id} not found." });
            }

            _context.LabTestCatalogs.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}