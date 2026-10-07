using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class UpdatePaymentDto
    {
        [Range(0.01, 1000000.00, ErrorMessage = "Amount must be greater than 0.")]
        public decimal? AmountPaid { get; set; }

        [RegularExpression("^(Cash|Card|GCash|Bank Transfer|Insurance)$", ErrorMessage = "Method must be Cash, Card, GCash, Bank Transfer, or Insurance.")]
        public string? PaymentMethod { get; set; }

        public DateTime? PaymentDate { get; set; }

        [RegularExpression("^(Paid|Pending|Refunded)$", ErrorMessage = "Status must be Paid, Pending, or Refunded.")]
        public string? Status { get; set; }

        public string? ReceiptNumber { get; set; }
    }
}