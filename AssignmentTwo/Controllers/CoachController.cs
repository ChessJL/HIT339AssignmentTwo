using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssignmentTwo.Data;
using AssignmentTwo.Models;

namespace AssignmentTwo.Controllers
{
    [Authorize(Roles = "Coach")]     public class CoachController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CoachController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var coach = _userManager.GetUserName(User);
            var coaches = _context.Coach
                .Where(m => m.Email == coach);
         
            return View("Index", coaches);
        }
        
        // GET: Coach/MySchedule
        public ActionResult Myschedule()
        {
            var coach = _userManager.GetUserName(User);
            var schedules = _context.Schedule
                .Where(m => m.CoachEmail == coach);
            return View("Myschedule", schedules);
        }
        public ActionResult Showbiography()
        {
            var coach = _userManager.GetUserName(User);
            var coaches = _context.Coach
                .Where(m => m.Email == coach);
            return View("Showbiography", coaches);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Biography,PhotoUrl")] Coach coach)
        {
            if (id != coach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(coach);
        }

        private bool CoachExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Showenrolmembers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var members = _context.ScheduleMembers
                .Where(m => m.ScheduleId == id);
            return View("Showenrolmembers", members);
  
        }

    }
}
