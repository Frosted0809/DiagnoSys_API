namespace DiagnoSys_API.DTOs
{
    public class UpdateCbcDto
    {
        // All fields optional — only send the ones you want to update
        // optional lahat need lng naman send yung mga bagay na need i update o palitan
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