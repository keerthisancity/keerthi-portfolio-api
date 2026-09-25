using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Dtos;

namespace PortfolioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        public SkillsController(PortfolioDbContext context) => _context = context;

        // GET: api/skills
        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            var skills = await _context.Skills.ToListAsync();
            var dtos = skills.Select(s => new SkillDto
            {
                SkillID = s.SkillID,
                BioID = s.BioID,
                SkillName = s.SkillName,
                Category = s.Category,
                ProficiencyLevel = s.ProficiencyLevel
            });

            return Ok(dtos);
        }

        // GET: api/skills/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            var s = await _context.Skills.FindAsync(id);
            if (s == null) return NotFound();

            var dto = new SkillDto
            {
                SkillID = s.SkillID,
                BioID = s.BioID,
                SkillName = s.SkillName,
                Category = s.Category,
                ProficiencyLevel = s.ProficiencyLevel
            };

            return Ok(dto);
        }
    }
}
