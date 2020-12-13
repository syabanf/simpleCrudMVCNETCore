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
    public class TbinvoicesController : Controller
    {
        private readonly DBTestContext _context;

        public TbinvoicesController(DBTestContext context)
        {
            _context = context;
        }

        // GET: Tbinvoices
        public async Task<IActionResult> Index()
        {
            var dBTestContext = _context.Tbinvoices.Include(t => t.GuidcourierNavigation).Include(t => t.GuidpaymentNavigation).Include(t => t.GuidsalesNavigation).Include(t=>t.TbinvoiceDetails);
            return View(await dBTestContext.ToListAsync());
        }

        // GET: Tbinvoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbinvoice = await _context.Tbinvoices
                .Include(t => t.GuidcourierNavigation)
                .Include(t => t.GuidpaymentNavigation)
                .Include(t => t.GuidsalesNavigation)
                .FirstOrDefaultAsync(m => m.Guidinvoice == id);
            if (tbinvoice == null)
            {
                return NotFound();
            }

            return View(tbinvoice);
        }

        // GET: Tbinvoices/Create
        public IActionResult Create()
        {
            ViewData["Guidcourier"] = new SelectList(_context.Tbcouriers, "Guidcourier", "CourierName");
            ViewData["Guidpayment"] = new SelectList(_context.Tbpayments, "Guidpayment", "PaymentType");
            ViewData["Guidsales"] = new SelectList(_context.Tbsales, "Guidsales", "SalesName");
            return View();
        }

        // POST: Tbinvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guidinvoice,InvoiceDate,Guidsales,Guidcourier,Guidpayment,ShipTo,TargetTo")] Tbinvoice tbinvoice)
        {
            if (ModelState.IsValid)
            {
                tbinvoice.InvoiceNumber ="INV-"+  _context.Tbinvoices.Count();
                tbinvoice.Guidinvoice = Guid.NewGuid();
                _context.Add(tbinvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Guidcourier"] = new SelectList(_context.Tbcouriers, "Guidcourier", "Guidcourier", tbinvoice.Guidcourier);
            ViewData["Guidpayment"] = new SelectList(_context.Tbpayments, "Guidpayment", "Guidpayment", tbinvoice.Guidpayment);
            ViewData["Guidsales"] = new SelectList(_context.Tbsales, "Guidsales", "Guidsales", tbinvoice.Guidsales);
            return View(tbinvoice);
        }

        // GET: Tbinvoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbinvoice = await _context.Tbinvoices.FindAsync(id);
            if (tbinvoice == null)
            {
                return NotFound();
            }
            ViewData["Guidcourier"] = new SelectList(_context.Tbcouriers, "Guidcourier", "CourierName", tbinvoice.Guidcourier);
            ViewData["Guidpayment"] = new SelectList(_context.Tbpayments, "Guidpayment", "PaymentType", tbinvoice.Guidpayment);
            ViewData["Guidsales"] = new SelectList(_context.Tbsales, "Guidsales", "SalesName", tbinvoice.Guidsales);
            return View(tbinvoice);
        }

        // POST: Tbinvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guidinvoice,InvoiceDate,Guidsales,Guidcourier,Guidpayment,ShipTo,TargetTo")] Tbinvoice tbinvoice)
        {
            if (id != tbinvoice.Guidinvoice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbinvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbinvoiceExists(tbinvoice.Guidinvoice))
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
            ViewData["Guidcourier"] = new SelectList(_context.Tbcouriers, "Guidcourier", "Guidcourier", tbinvoice.Guidcourier);
            ViewData["Guidpayment"] = new SelectList(_context.Tbpayments, "Guidpayment", "Guidpayment", tbinvoice.Guidpayment);
            ViewData["Guidsales"] = new SelectList(_context.Tbsales, "Guidsales", "Guidsales", tbinvoice.Guidsales);
            return View(tbinvoice);
        }

        // GET: Tbinvoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbinvoice = await _context.Tbinvoices
                .Include(t => t.GuidcourierNavigation)
                .Include(t => t.GuidpaymentNavigation)
                .Include(t => t.GuidsalesNavigation)
                .FirstOrDefaultAsync(m => m.Guidinvoice == id);
            if (tbinvoice == null)
            {
                return NotFound();
            }

            return View(tbinvoice);
        }

        // POST: Tbinvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tbinvoice = await _context.Tbinvoices.FindAsync(id);
            foreach (var m in _context.TbinvoiceDetails.Where(f => f.Guidinvoice == tbinvoice.Guidinvoice))
            {
                _context.TbinvoiceDetails.Remove(m);
            }
            _context.Tbinvoices.Remove(tbinvoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbinvoiceExists(Guid id)
        {
            return _context.Tbinvoices.Any(e => e.Guidinvoice == id);
        }
    }
}
