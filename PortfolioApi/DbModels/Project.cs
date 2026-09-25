namespace PortfolioApi.DbModels
{
    public class Project
    {
        public int ProjectID { get; set; }
        public int BioID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? TechStack { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ProjectURL { get; set; }
        public string? RepoURL { get; set; }

        // Navigation
        public Bio Bio { get; set; }
        public ICollection<ProjectSkill> ProjectSkills { get; set; }
    }

}
