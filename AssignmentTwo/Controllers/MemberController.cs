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
    [Authorize(Roles = "Member")]
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MemberController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;

        }
        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            return View();
        }
        // View all schedules
        public async Task<IActionResult> Schedule()
        {
            return View(await _context.Schedule.ToListAsync());
        }
        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        [Authorize]
        // GET: Schedules/Enrol/5
        public async Task<IActionResult> Enrol(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Enrol/5
        [HttpPost, ActionName("Enrol")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrolConfirmed([Bind("ScheduleId")] ScheduleMembers ScheduleMembers)
        {
            // get the member
            var member = _userManager.GetUserName(User);
            ScheduleMembers.MemberEmail = member;

            // find the schedule
            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.Id == ScheduleMembers.ScheduleId);
            if (schedule == null)
            {
                return NotFound();
            }

            // make the scheduleMembers
            _context.Add(ScheduleMembers);

            // Save the changes
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }
    }
}
