﻿using Homies.Common.Constants;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;

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
                    Start = x.Start.ToString(DataConstants.DateFormat),
                    Type = x.Type.Name,
                    Organiser = x.Organiser.UserName
                })
                .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            EventFormViewModel model = new EventFormViewModel();
            model.Types = await GetTypesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;


            if (!DateTime.TryParseExact
                (model.Start,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState
                    .AddModelError(nameof(model.Start), $"Invalid date! Format must be: {DataConstants.DateFormat}");
            }

            if (!DateTime.TryParseExact
                (model.End,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState
                    .AddModelError(nameof(model.End), $"Invalid date! Format must be: {DataConstants.DateFormat}");
            }


            if (!ModelState.IsValid)
            {
                model.Types = await GetTypesAsync();
                return View(model);
            }

            var entityModel = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                Start = start,
                End = end,
                TypeId = model.TypeId,
                CreatedOn = DateTime.Now,
                OrganiserId = GetUserId()
            };

            await _context.Events.AddAsync(entityModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            if (model.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            var viewModel = new EventFormViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Start = model.Start.ToString(DataConstants.DateFormat),
                End = model.End.ToString(DataConstants.DateFormat),
                TypeId = model.TypeId,
                Types = await GetTypesAsync(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventFormViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            var model = await _context.Events.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            if (model.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;


            if (!DateTime.TryParseExact
                (viewModel.Start,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState
                    .AddModelError(nameof(viewModel.Start), $"Invalid date! Format must be: {DataConstants.DateFormat}");
            }

            if (!DateTime.TryParseExact
                (viewModel.End,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState
                    .AddModelError(nameof(viewModel.End), $"Invalid date! Format must be: {DataConstants.DateFormat}");
            }


            if (!ModelState.IsValid)
            {
                viewModel.Types = await GetTypesAsync();
                return View(viewModel);
            }

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.Start = start;
            model.End = end;
            model.TypeId = viewModel.TypeId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Join(int id)
        {
            return View();
        }





        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<TypeViewModel>> GetTypesAsync()
        {
            return await _context.Types
                .AsNoTracking()
                .Select(x => new TypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();
        }
    }
}
