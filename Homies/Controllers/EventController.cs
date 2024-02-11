using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Homies.Common.Constants.DataConstants;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext _context;

        public EventController(HomiesDbContext context)
        {
            _context = context;
        }

        [HttpGet]   
        public async Task<IActionResult> All()
        {
            var events = await _context.Events
                .AsNoTracking()
                .Select(x => new EventInfoViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Start = x.Start.ToString(DateFormat),
                    Type = x.Type.Name,
                    Organiser = x.Organiser.UserName
                })
                .ToListAsync();

            return View(events);
        }



    }
}
