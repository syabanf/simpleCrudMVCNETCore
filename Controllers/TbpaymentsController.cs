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
    public class TbpaymentsController : Controller
    {
        private readonly DBTestContext _context;

        public TbpaymentsController(DBTestContext context)
        {
            _context = context;
        }

        // GET: Tbpayments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tbpayments.ToListAsync());
        }

        // GET: Tbpayments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbpayment = await _context.Tbpayments
                .FirstOrDefaultAsync(m => m.Guidpayment == id);
            if (tbpayment == null)
            {
                return NotFound();
            }

            return View(tbpayment);
        }

        // GET: Tbpayments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tbpayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guidpayment,PaymentType")] Tbpayment tbpayment)
        {
            if (ModelState.IsValid)
            {
                tbpayment.Guidpayment = Guid.NewGuid();
                _context.Add(tbpayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbpayment);
        }

        // GET: Tbpayments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbpayment = await _context.Tbpayments.FindAsync(id);
            if (tbpayment == null)
            {
                return NotFound();
            }
            return View(tbpayment);
        }

        // POST: Tbpayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guidpayment,PaymentType")] Tbpayment tbpayment)
        {
            if (id != tbpayment.Guidpayment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbpayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbpaymentExists(tbpayment.Guidpayment))
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
            return View(tbpayment);
        }

        // GET: Tbpayments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbpayment = await _context.Tbpayments
                .FirstOrDefaultAsync(m => m.Guidpayment == id);
            if (tbpayment == null)
            {
                return NotFound();
            }

            return View(tbpayment);
        }

        // POST: Tbpayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tbpayment = await _context.Tbpayments.FindAsync(id);
            _context.Tbpayments.Remove(tbpayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbpaymentExists(Guid id)
        {
            return _context.Tbpayments.Any(e => e.Guidpayment == id);
        }
    }
}
