using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class CreatePaymentDto
    {
        [Required(ErrorMessage = "Order ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Order ID.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Amount paid is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Amount must be greater than 0.")]
        public decimal AmountPaid { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [RegularExpression("^(Cash|Card|GCash|Bank Transfer|Insurance)$", ErrorMessage = "Method must be Cash, Card, GCash, Bank Transfer, or Insurance.")]
        public string PaymentMethod { get; set; } = "Cash";

        // Optional: Defaults to today if not provided // same lng din sa nakaraan yung date at time mag dedefault kung wala nilagay ba
        public DateTime? PaymentDate { get; set; }

        [RegularExpression("^(Paid|Pending|Refunded)$", ErrorMessage = "Status must be Paid, Pending, or Refunded.")]
        public string Status { get; set; } = "Paid";

        public string? ReceiptNumber { get; set; }
    }
}