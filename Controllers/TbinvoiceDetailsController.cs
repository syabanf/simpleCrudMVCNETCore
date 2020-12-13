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
    public class TbinvoiceDetailsController : Controller
    {
        private readonly DBTestContext _context;

        public TbinvoiceDetailsController(DBTestContext context)
        {
            _context = context;
        }

        // GET: TbinvoiceDetails
        public async Task<IActionResult> Index()
        {
            var dBTestContext = _context.TbinvoiceDetails.Include(t => t.GuidinvoiceNavigation);
            return View(await dBTestContext.ToListAsync());
        }

        // GET: TbinvoiceDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbinvoiceDetail = await _context.TbinvoiceDetails
                .Include(t => t.GuidinvoiceNavigation)
                .FirstOrDefaultAsync(m => m.GuidinvoiceDetail == id);
            if (tbinvoiceDetail == null)
            {
                return NotFound();
            }

            return View(tbinvoiceDetail);
        }

        // GET: TbinvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["Guidinvoice"] = new SelectList(_context.Tbinvoices, "Guidinvoice", "InvoiceNumber");
            return View();
        }

        // POST: TbinvoiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuidinvoiceDetail,Guidinvoice,Item,Weight,Qty,UnitPrice")] TbinvoiceDetail tbinvoiceDetail)
        {
            if (ModelState.IsValid)
            {
                tbinvoiceDetail.GuidinvoiceDetail = Guid.NewGuid();
                _context.Add(tbinvoiceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Guidinvoice"] = new SelectList(_context.Tbinvoices, "Guidinvoice", "Guidinvoice", tbinvoiceDetail.Guidinvoice);
            return View(tbinvoiceDetail);
        }

        // GET: TbinvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbinvoiceDetail = await _context.TbinvoiceDetails.FindAsync(id);
            if (tbinvoiceDetail == null)
            {
                return NotFound();
            }
            ViewData["Guidinvoice"] = new SelectList(_context.Tbinvoices, "Guidinvoice", "InvoiceNumber", tbinvoiceDetail.Guidinvoice);
            return View(tbinvoiceDetail);
        }

        // POST: TbinvoiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GuidinvoiceDetail,Guidinvoice,Item,Weight,Qty,UnitPrice")] TbinvoiceDetail tbinvoiceDetail)
        {
            if (id != tbinvoiceDetail.GuidinvoiceDetail)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbinvoiceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbinvoiceDetailExists(tbinvoiceDetail.GuidinvoiceDetail))
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
            ViewData["Guidinvoice"] = new SelectList(_context.Tbinvoices, "Guidinvoice", "Guidinvoice", tbinvoiceDetail.Guidinvoice);
            return View(tbinvoiceDetail);
        }

        // GET: TbinvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbinvoiceDetail = await _context.TbinvoiceDetails
                .Include(t => t.GuidinvoiceNavigation)
                .FirstOrDefaultAsync(m => m.GuidinvoiceDetail == id);
            if (tbinvoiceDetail == null)
            {
                return NotFound();
            }

            return View(tbinvoiceDetail);
        }

        // POST: TbinvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tbinvoiceDetail = await _context.TbinvoiceDetails.FindAsync(id);
            _context.TbinvoiceDetails.Remove(tbinvoiceDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbinvoiceDetailExists(Guid id)
        {
            return _context.TbinvoiceDetails.Any(e => e.GuidinvoiceDetail == id);
        }
    }
}
