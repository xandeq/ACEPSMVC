using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ACEPSMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace ACEPSMVC.Controllers
{
    [Authorize]
    public class InstitucionalsController : Controller
    {
        private readonly ContextoDBAplicacao _context;

        public InstitucionalsController(ContextoDBAplicacao context)
        {
            _context = context;
        }

        // GET: Institucionals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Institucional.ToListAsync());
        }

        // GET: Institucionals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institucional = await _context.Institucional
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institucional == null)
            {
                return NotFound();
            }

            return View(institucional);
        }

        // GET: Institucionals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Institucionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto,UltimaAlteracao")] Institucional institucional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institucional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institucional);
        }

        // GET: Institucionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institucional = await _context.Institucional.FindAsync(id);
            if (institucional == null)
            {
                return NotFound();
            }
            return View(institucional);
        }

        // POST: Institucionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Texto,UltimaAlteracao")] Institucional institucional)
        {
            if (id != institucional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institucional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitucionalExists(institucional.Id))
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
            return View(institucional);
        }

        // GET: Institucionals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institucional = await _context.Institucional
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institucional == null)
            {
                return NotFound();
            }

            return View(institucional);
        }

        // POST: Institucionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institucional = await _context.Institucional.FindAsync(id);
            _context.Institucional.Remove(institucional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitucionalExists(int id)
        {
            return _context.Institucional.Any(e => e.Id == id);
        }
    }
}
