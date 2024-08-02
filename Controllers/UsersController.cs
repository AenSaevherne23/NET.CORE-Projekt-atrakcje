using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_atrakcje.Data;
using Projekt_atrakcje.Models;

namespace Projekt_atrakcje.Controllers
{
    public class UsersController : Controller
    {
        private readonly Projekt_atrakcjeContext _context;

        public UsersController(Projekt_atrakcjeContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.Include(c => c.Cities).ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name_user,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name_user,Email,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        //GET: Users/5/Grade/5        
        [Route("Users/{id}/Grade/{cityid}")]
        public async Task<IActionResult> Grade(int id, int cityid)
        {
            var @user = await _context.User.FindAsync(id);
            var city = await _context.City.FindAsync(cityid);
            ViewData["gradelist"] = GetGradesList(@user, city);
            ViewData["city"] = city;
            return View(@user);
        }

        //GET: Users/5/SetGrade/5        
        [Route("Users/{id}/SetGrade/{cityid}")]
        public async Task<IActionResult> SetGrade(int id, int cityid)
        {
            var @user = await _context.User.FindAsync(id);
            var city = await _context.City.FindAsync(cityid);
            ViewData["gradelist"] = GetGradesList(@user, city);
            ViewData["city"] = city;
            return View(@user);
        }

        // POST: Users/5/SetGrade/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/{id}/SetGrade/{cityid}")]
        public async Task<IActionResult> SetGrade(int id)
        {
            var @user = await _context.User.FindAsync(id);
            var cityid = int.Parse(HttpContext.Request.Form["cityid"]);
            var ids = HttpContext.Request.Form["ids"];
            var grades = HttpContext.Request.Form["oceny"];
            var i = 0;
            var xid = 0;
            foreach(var attractionid in ids)
            {
                xid = int.Parse(attractionid);
                var xgr = _context.Grade.Where(g => g.AttractionId == xid & g.UserId == id);
                if (xgr.Any())
                {
                    var ocena = _context.Grade.Where(g => g.AttractionId == xid & g.UserId == id).Single();
                    ocena.Ocena = decimal.Parse(grades[i]);
                    _context.Update(ocena);
                }
                else
                {
                    var grade = new Grade()
                    {
                        AttractionId = xid,
                        UserId = id,
                        Ocena = decimal.Parse(grades[i])
                    };
                    _context.Add(grade);
                }
                i++;
            }
            await _context.SaveChangesAsync();
            var city = await _context.City.FindAsync(cityid);
            return RedirectToAction("Grade", new { id = user.Id, cityid = city.Id });

        }


        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        //funkcja budująca listę ocen atrackji
        private List<AttractionGrades> GetGradesList(User user, City city)
        {
            var attractions = _context.Attraction.Where(s => s.CityId == city.Id).ToList();
            var grades = new List<AttractionGrades>();
            var xocena = 0.0M;
            foreach(Attraction? attraction in attractions)
            {
                if (_context.Grade.Where(g => g.UserId == user.Id & g.AttractionId == attraction.Id).Any())
                {
                    xocena = _context.Grade.Where(g => g.UserId == user.Id & g.AttractionId == attraction.Id).First().Ocena;
                }
                else
                {
                    xocena = 0;
                }
                grades.Add(
                    new AttractionGrades
                    {
                        AttractionId = attraction.Id,
                        Name_attraction = attraction.Name_attraction,
                        Ocena = xocena,
                    }
                    );
            }
            return grades;
        }
    }
}
