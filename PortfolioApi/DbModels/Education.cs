namespace PortfolioApi.DbModels
{
    public class Education
    {
        public int EducationID { get; set; }
        public int BioID { get; set; }
        public string? Institution { get; set; }
        public string? Degree { get; set; }
        public string? FieldOfStudy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }

        // Navigation
        public Bio Bio { get; set; }
    }

}
