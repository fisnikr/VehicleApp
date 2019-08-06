using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleAppT.Models;

namespace VehicleAppT.Controllers
{
    public class VModelsController : Controller
    {
        private readonly VehicleAppTContext _context;

        public VModelsController(VehicleAppTContext context)
        {
            _context = context;
        }

        // GET: VModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.VModel.ToListAsync());
        }

        // GET: VModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vModel = await _context.VModel
                .FirstOrDefaultAsync(m => m.MID == id);
            if (vModel == null)
            {
                return NotFound();
            }

            return View(vModel);
        }

        // GET: VModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MID,Model")] VModel vModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vModel);
        }

        // GET: VModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vModel = await _context.VModel.FindAsync(id);
            if (vModel == null)
            {
                return NotFound();
            }
            return View(vModel);
        }

        // POST: VModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MID,Model")] VModel vModel)
        {
            if (id != vModel.MID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VModelExists(vModel.MID))
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
            return View(vModel);
        }

        // GET: VModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vModel = await _context.VModel
                .FirstOrDefaultAsync(m => m.MID == id);
            if (vModel == null)
            {
                return NotFound();
            }

            return View(vModel);
        }

        // POST: VModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vModel = await _context.VModel.FindAsync(id);
            _context.VModel.Remove(vModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VModelExists(int id)
        {
            return _context.VModel.Any(e => e.MID == id);
        }
    }
}
