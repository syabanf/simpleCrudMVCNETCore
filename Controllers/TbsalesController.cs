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
    public class TbsalesController : Controller
    {
        private readonly DBTestContext _context;

        public TbsalesController(DBTestContext context)
        {
            _context = context;
        }

        // GET: Tbsales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tbsales.ToListAsync());
        }

        // GET: Tbsales/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbsale = await _context.Tbsales
                .FirstOrDefaultAsync(m => m.Guidsales == id);
            if (tbsale == null)
            {
                return NotFound();
            }

            return View(tbsale);
        }

        // GET: Tbsales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tbsales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guidsales,SalesName")] Tbsale tbsale)
        {
            if (ModelState.IsValid)
            {
                tbsale.Guidsales = Guid.NewGuid();
                _context.Add(tbsale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbsale);
        }

        // GET: Tbsales/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbsale = await _context.Tbsales.FindAsync(id);
            if (tbsale == null)
            {
                return NotFound();
            }
            return View(tbsale);
        }

        // POST: Tbsales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guidsales,SalesName")] Tbsale tbsale)
        {
            if (id != tbsale.Guidsales)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbsale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbsaleExists(tbsale.Guidsales))
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
            return View(tbsale);
        }

        // GET: Tbsales/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbsale = await _context.Tbsales
                .FirstOrDefaultAsync(m => m.Guidsales == id);
            if (tbsale == null)
            {
                return NotFound();
            }

            return View(tbsale);
        }

        // POST: Tbsales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tbsale = await _context.Tbsales.FindAsync(id);
            _context.Tbsales.Remove(tbsale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbsaleExists(Guid id)
        {
            return _context.Tbsales.Any(e => e.Guidsales == id);
        }
    }
}
