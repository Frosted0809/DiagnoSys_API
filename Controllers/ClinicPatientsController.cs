using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiagnoSys_API.Data;
using DiagnoSys_API.Models;
using DiagnoSys_API.DTOs;

namespace DiagnoSys_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicPatientsController : ControllerBase
    {
        private readonly DiagnoSysDbContext _context;
        private readonly IMapper _mapper;

        public ClinicPatientsController(DiagnoSysDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/clinicpatients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicPatientDto>>> GetPatients()
        {
            var patients = await _context.ClinicPatients.AsNoTracking().ToListAsync();
            var dtos = _mapper.Map<IEnumerable<ClinicPatientDto>>(patients);
            return Ok(dtos);
        }

        // GET: api/clinicpatients/PAT-001
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicPatientDto>> GetPatient(string id)
        {
            var patient = await _context.ClinicPatients.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound(new { message = $"Patient with ID {id} not found." });
            }

            var dto = _mapper.Map<ClinicPatientDto>(patient);
            return Ok(dto);
        }

        // POST: api/clinicpatients
        [HttpPost]
        public async Task<ActionResult<ClinicPatientDto>> CreatePatient(CreateClinicPatientDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exists = await _context.ClinicPatients.AnyAsync(p => p.PatientId == createDto.PatientId);
            if (exists)
            {
                return Conflict(new { message = $"Patient ID {createDto.PatientId} already exists." });
            }

            var patient = _mapper.Map<ClinicPatient>(createDto);
            patient.RegisteredAt = DateTime.Now;
            patient.CreatedAt = DateTime.Now;
            patient.UpdatedAt = DateTime.Now;

            _context.ClinicPatients.Add(patient);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ClinicPatientDto>(patient);

            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, resultDto);
        }

        // PUT: api/clinicpatients/PAT-001
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(string id, UpdateClinicPatientDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await _context.ClinicPatients.FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound(new { message = $"Patient with ID {id} not found." });
            }

            _mapper.Map(updateDto, patient);
            patient.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/clinicpatients/PAT-001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            var patient = await _context.ClinicPatients.FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound(new { message = $"Patient with ID {id} not found." });
            }

            _context.ClinicPatients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
} 