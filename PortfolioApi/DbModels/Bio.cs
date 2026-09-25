namespace PortfolioApi.DbModels
{
    public class Bio
    {
        public int BioID { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        // New columns
        public string? ShortName { get; set; }
        public string? AboutMe { get; set; }
        public string? Summary { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string? PortfolioURL { get; set; }
        public string? Location { get; set; }
        public string? ResumeUrl { get; set; }

        // Navigation properties
        public ICollection<Project> Projects { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Experience> Experience { get; set; }
        public ICollection<Education> Education { get; set; }
        public ICollection<Achievement> Achievements { get; set; }
    }

}
