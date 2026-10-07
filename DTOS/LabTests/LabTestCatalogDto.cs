namespace DiagnoSys_API.DTOs
{
    public class LabTestCatalogDto
    {
        public int TestId { get; set; }
        public string TestName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StandardPrice { get; set; }
    }
}