using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test.Models.DB;

namespace Test.Controllers
{
    public class TbcouriersController : Controller
    {
        private readonly DBTestContext _context;

        public TbcouriersController(DBTestContext context)
        {
            _context = context;
        }

        // GET: Tbcouriers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tbcouriers.ToListAsync());
        }

        // GET: Tbcouriers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbcourier = await _context.Tbcouriers
                .FirstOrDefaultAsync(m => m.Guidcourier == id);
            if (tbcourier == null)
            {
                return NotFound();
            }

            return View(tbcourier);
        }

        // GET: Tbcouriers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tbcouriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guidcourier,CourierName,Cost")] Tbcourier tbcourier)
        {
            if (ModelState.IsValid)
            {
                tbcourier.Guidcourier = Guid.NewGuid();
                _context.Add(tbcourier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbcourier);
        }

        // GET: Tbcouriers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbcourier = await _context.Tbcouriers.FindAsync(id);
            if (tbcourier == null)
            {
                return NotFound();
            }
            return View(tbcourier);
        }

        // POST: Tbcouriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guidcourier,CourierName,Cost")] Tbcourier tbcourier)
        {
            if (id != tbcourier.Guidcourier)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbcourier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbcourierExists(tbcourier.Guidcourier))
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
            return View(tbcourier);
        }

        // GET: Tbcouriers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbcourier = await _context.Tbcouriers
                .FirstOrDefaultAsync(m => m.Guidcourier == id);
            if (tbcourier == null)
            {
                return NotFound();
            }

            return View(tbcourier);
        }

        // POST: Tbcouriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tbcourier = await _context.Tbcouriers.FindAsync(id);
            _context.Tbcouriers.Remove(tbcourier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbcourierExists(Guid id)
        {
            return _context.Tbcouriers.Any(e => e.Guidcourier == id);
        }
    }
}
