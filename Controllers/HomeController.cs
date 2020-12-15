using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Test.Models.DB;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBTestContext _context;
        public HomeController(ILogger<HomeController> logger, DBTestContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(Guid? guidInvoice)
        {
            var datas = _context.Tbinvoices.Include(t => t.GuidcourierNavigation).Include(t => t.GuidpaymentNavigation).Include(t => t.GuidsalesNavigation).Include(t => t.TbinvoiceDetails);
            var data = new Tbinvoice();
            if (guidInvoice !=null)
            {
                data = datas.FirstOrDefault(t => t.Guidinvoice == guidInvoice);
            }
            else {
                data = datas.FirstOrDefault();
            }
            if (data == null)
            {
                return NotFound();
            }
            ViewData["invoiceNumbers"] = new SelectList(_context.Tbinvoices, "Guidinvoice", "InvoiceNumber");
            var subtotal = data.TbinvoiceDetails.Sum(x => x.UnitPrice * x.Qty);
            var courierFee = data.TbinvoiceDetails.Sum(x => x.Weight) * data.GuidcourierNavigation.Cost;
            ViewData["subTotal"] = subtotal;
            ViewData["courierFee"] = courierFee;
            ViewData["total"] = subtotal + courierFee;
            return View(data);
        }

   
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
