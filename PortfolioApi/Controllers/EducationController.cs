using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Dtos;

namespace PortfolioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        public EducationController(PortfolioDbContext context) => _context = context;

        // GET: api/education
        [HttpGet]
        public async Task<IActionResult> GetEducation()
        {
            var items = await _context.Education.ToListAsync();
            var dtos = items.Select(ed => new EducationDto
            {
                EducationID = ed.EducationID,
                BioID = ed.BioID,
                Institution = ed.Institution,
                Degree = ed.Degree,
                FieldOfStudy = ed.FieldOfStudy,
                StartDate = ed.StartDate,
                EndDate = ed.EndDate,
                Description = ed.Description
            });

            return Ok(dtos);
        }

        // GET: api/education/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEducationItem(int id)
        {
            var ed = await _context.Education.FindAsync(id);
            if (ed == null) return NotFound();

            var dto = new EducationDto
            {
                EducationID = ed.EducationID,
                BioID = ed.BioID,
                Institution = ed.Institution,
                Degree = ed.Degree,
                FieldOfStudy = ed.FieldOfStudy,
                StartDate = ed.StartDate,
                EndDate = ed.EndDate,
                Description = ed.Description
            };

            return Ok(dto);
        }
    }
}
