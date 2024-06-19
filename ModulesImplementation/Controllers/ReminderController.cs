using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModulesImplementation.Data;
using ModulesImplementation.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ModulesImplementation.Controllers
{
    public class ReminderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReminderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Reminders.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        //Create a reminder, add a datetime and email address. In the DB, infor will be added and once email remiknder is sent, that value will be updated
        [HttpPost]
        public async Task<IActionResult> Create(ReminderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var reminder = new Reminder
                {
                    Title = viewModel.Title,
                    ReminderDateTime = viewModel.ReminderDateTime,
                    Email = viewModel.Email
                };

                _context.Add(reminder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

    }
}
