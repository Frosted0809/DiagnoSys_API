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
    public class PaymentsController : ControllerBase
    {
        private readonly DiagnoSysDbContext _context;
        private readonly IMapper _mapper;

        public PaymentsController(DiagnoSysDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
        {
            var payments = await _context.Payments
                .Include(p => p.Order)
                    .ThenInclude(o => o.Patient)
                .Include(p => p.Order)
                    .ThenInclude(o => o.Test)
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
        }

        // GET: api/Payments/1
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Order)
                    .ThenInclude(o => o.Patient)
                .Include(p => p.Order)
                    .ThenInclude(o => o.Test)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
            {
                return NotFound(new { message = $"Payment with ID {id} not found." });
            }

            return Ok(_mapper.Map<PaymentDto>(payment));
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(CreatePaymentDto createDto)
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

            var payment = _mapper.Map<Payment>(createDto);

            // Set defaults
            payment.PaymentDate = createDto.PaymentDate ?? DateTime.UtcNow;
            payment.CreatedAt = DateTime.UtcNow;
            payment.UpdatedAt = DateTime.UtcNow;

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Return rich DTO
            var created = await _context.Payments
                .Include(p => p.Order)
                    .ThenInclude(o => o.Patient)
                .Include(p => p.Order)
                    .ThenInclude(o => o.Test)
                .FirstOrDefaultAsync(p => p.PaymentId == payment.PaymentId);

            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, _mapper.Map<PaymentDto>(created));
        }

        // PUT: api/Payments/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == id);
            if (payment == null)
            {
                return NotFound(new { message = $"Payment with ID {id} not found." });
            }

            _mapper.Map(updateDto, payment);
            payment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Payments/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == id);
            if (payment == null)
            {
                return NotFound(new { message = $"Payment with ID {id} not found." });
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}