namespace PortfolioApi.DbModels
{
    public class Skill
    {
        public int SkillID { get; set; }
        public int BioID { get; set; }
        public string? SkillName { get; set; }
        public string? Category { get; set; }
        public string? ProficiencyLevel { get; set; }

        // Navigation
        public Bio? Bio { get; set; }
        public ICollection<ProjectSkill> ProjectSkills { get; set; }
    }

}
