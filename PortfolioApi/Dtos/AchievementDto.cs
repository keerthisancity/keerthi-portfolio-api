namespace PortfolioApi.Dtos
{
    public class AchievementDto
    {
        public int AchievementID { get; set; }
        public int BioID { get; set; }
        public string? Title { get; set; }
        public string? Issuer { get; set; }
        public DateTime? DateAchieved { get; set; }
        public string? Description { get; set; }
    }
}
