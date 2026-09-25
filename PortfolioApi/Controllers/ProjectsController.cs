using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Dtos;
using PortfolioApi.DbModels;

namespace PortfolioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        public ProjectsController(PortfolioDbContext context) => _context = context;

        // GET: api/projects
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            var dtos = projects.Select(p => new ProjectDto
            {
                ProjectID = p.ProjectID,
                BioID = p.BioID,
                Title = p.Title,
                Description = p.Description,
                TechStack = p.TechStack,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProjectURL = p.ProjectURL,
                RepoURL = p.RepoURL
            });

            return Ok(dtos);
        }

        // GET: api/projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var p = await _context.Projects.FindAsync(id);
            if (p == null) return NotFound();

            var dto = new ProjectDto
            {
                ProjectID = p.ProjectID,
                BioID = p.BioID,
                Title = p.Title,
                Description = p.Description,
                TechStack = p.TechStack,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProjectURL = p.ProjectURL,
                RepoURL = p.RepoURL
            };

            return Ok(dto);
        }
    }
}
