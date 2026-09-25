using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Dtos;

namespace PortfolioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BioController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        public BioController(PortfolioDbContext context) => _context = context;

        // GET: api/bio
        [HttpGet]
        public async Task<IActionResult> GetBio()
        {
            var bio = await _context.Bio.FirstOrDefaultAsync();
            if (bio == null)
                return NotFound();

            var bioDto = new BioDto
            {
                BioID = bio.BioID,
                FullName = bio.FullName,
                Title = bio.Title,
                Email = bio.Email,
                Phone = bio.Phone,
                ShortName = bio.ShortName,
                AboutMe = bio.AboutMe,
                Summary = bio.Summary,
                LinkedIn = bio.LinkedIn,
                GitHub = bio.GitHub,
                PortfolioURL = bio.PortfolioURL,
                Location = bio.Location,
                ResumeUrl = bio.ResumeUrl,
            };

            return Ok(bioDto);
        }

    }
}
