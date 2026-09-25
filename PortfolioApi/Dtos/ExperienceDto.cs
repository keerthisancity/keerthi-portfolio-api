namespace PortfolioApi.Dtos
{
    public class ExperienceDto
    {
        public int ExperienceID { get; set; }
        public int BioID { get; set; }
        public string? Company { get; set; }
        public string? Role { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Responsibilities { get; set; }
    }
}
