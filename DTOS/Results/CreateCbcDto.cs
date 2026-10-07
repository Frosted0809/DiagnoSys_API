using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class CreateCbcDto
    {
        [Required(ErrorMessage = "Order ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Order ID.")]
        public int OrderId { get; set; }

        // All medical fields are optional, you can fill them in later via PUT
        //optional lng lagay pero puwede lagay mga result sa put http
        public decimal? Wbc { get; set; }
        public decimal? Rbc { get; set; }
        public decimal? Hemoglobin { get; set; }
        public decimal? Hematocrit { get; set; }
        public int? Platelets { get; set; }
        public int? Mcv { get; set; }
        public int? Mch { get; set; }
        public decimal? Neutrophils { get; set; }
        public decimal? Lymphocytes { get; set; }
        public decimal? Monocytes { get; set; }
        public decimal? Eosinophils { get; set; }
        public decimal? Basophils { get; set; }
    }
}