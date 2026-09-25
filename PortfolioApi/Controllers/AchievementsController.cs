using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Dtos;

namespace PortfolioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        public AchievementsController(PortfolioDbContext context) => _context = context;

        // GET: api/achievements
        [HttpGet]
        public async Task<IActionResult> GetAchievements()
        {
            var items = await _context.Achievements.ToListAsync();
            var dtos = items.Select(a => new AchievementDto
            {
                AchievementID = a.AchievementID,
                BioID = a.BioID,
                Title = a.Title,
                Issuer = a.Issuer,
                DateAchieved = a.DateAchieved,
                Description = a.Description
            });

            return Ok(dtos);
        }

        // GET: api/achievements/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAchievement(int id)
        {
            var a = await _context.Achievements.FindAsync(id);
            if (a == null) return NotFound();

            var dto = new AchievementDto
            {
                AchievementID = a.AchievementID,
                BioID = a.BioID,
                Title = a.Title,
                Issuer = a.Issuer,
                DateAchieved = a.DateAchieved,
                Description = a.Description
            };

            return Ok(dto);
        }
    }
}
