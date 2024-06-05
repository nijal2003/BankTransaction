using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankTransations.Models;

namespace BankTransations.Controllers
{
    public class TransationController : Controller
    {
        private readonly TransactionDbContext _context;

        public TransationController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: Transation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transcription.ToListAsync());
        }

        // GET: Transation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transation = await _context.Transcription
                .FirstOrDefaultAsync(m => m.TransationId == id);
            if (transation == null)
            {
                return NotFound();
            }

            return View(transation);
        }

        // GET: Transation/AddOrEdit
        public IActionResult AddOrEdit()
        {
            return View(new Transation());
        }

        // POST: Transation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransationId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transation transation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transation);
        }

        // GET: Transation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transation = await _context.Transcription.FindAsync(id);
            if (transation == null)
            {
                return NotFound();
            }
            return View(transation);
        }

        // POST: Transation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransationId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transation transation)
        {
            if (id != transation.TransationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransationExists(transation.TransationId))
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
            return View(transation);
        }

        // GET: Transation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transation = await _context.Transcription
                .FirstOrDefaultAsync(m => m.TransationId == id);
            if (transation == null)
            {
                return NotFound();
            }

            return View(transation);
        }

        // POST: Transation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transation = await _context.Transcription.FindAsync(id);
            if (transation != null)
            {
                _context.Transcription.Remove(transation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransationExists(int id)
        {
            return _context.Transcription.Any(e => e.TransationId == id);
        }
    }
}
