namespace PortfolioApi.DbModels
{
    public class ProjectSkill
    {
        public int ProjectID { get; set; }
        public int SkillID { get; set; }

        // Navigation
        public Project? Project { get; set; }
        public Skill? Skill { get; set; }
    }

}
