using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Dtos;

namespace PortfolioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        public ExperienceController(PortfolioDbContext context) => _context = context;

        // GET: api/experience
        [HttpGet]
        public async Task<IActionResult> GetExperience()
        {
            var items = await _context.Experience.ToListAsync();
            var dtos = items.Select(e => new ExperienceDto
            {
                ExperienceID = e.ExperienceID,
                BioID = e.BioID,
                Company = e.Company,
                Role = e.Role,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Responsibilities = e.Responsibilities
            });

            return Ok(dtos);
        }

        // GET: api/experience/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperienceItem(int id)
        {
            var e = await _context.Experience.FindAsync(id);
            if (e == null) return NotFound();

            var dto = new ExperienceDto
            {
                ExperienceID = e.ExperienceID,
                BioID = e.BioID,
                Company = e.Company,
                Role = e.Role,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Responsibilities = e.Responsibilities
            };

            return Ok(dto);
        }
    }
}
