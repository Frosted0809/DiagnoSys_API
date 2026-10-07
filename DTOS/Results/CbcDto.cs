namespace DiagnoSys_API.DTOs
{
    public class CbcDto
    {
        public int CbcId { get; set; }
        public int OrderId { get; set; }

        // All medical fields (nullable because they may not be filled in yet)
        //Bale kahit walang laman dahil di pa nilalagyan ng resulta mga toh or di pa lalagyan
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

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Nested order info so you can see which patient/test this belongs to
        //hahanapin lng kung info ng order para malaman kung kanina itong result
        public LabTestDto? Order { get; set; }
    }
}