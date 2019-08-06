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
    public class VehiclesController : Controller
    {
        private readonly VehicleAppTContext _context;

        public VehiclesController(VehicleAppTContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index(string sortOrder)
        {
            
            var vehicleAppTContext = _context.Vehicle.Include(v => v.VModel);
            ViewData["modelSort"] = String.IsNullOrEmpty(sortOrder) ? "vModel_desc" : "";
            var vehicle = from v in vehicleAppTContext
                          select v;
            switch (sortOrder)
            {
                case "vModel_desc":
                    vehicle = vehicle.OrderByDescending(v =>v.VModel.Model);
                    break;
                default:
                    vehicle = vehicle.OrderBy(v => v.VModel.Model);
                    break;

            }
            return View(await vehicle.AsNoTracking().ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        public ActionResult Print()
        {
            var model = new List<Vehicle>();
            model.Add(new Vehicle { LicensePNr = "John", VinMark = "Doe" });
            model.Add(new Vehicle { LicensePNr = "John", VinMark = "Doe" });
            return PartialView(model);
        }
        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["MID"] = new SelectList(_context.VModel, "MID", "MID");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LicensePNr,VinMark,MID")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MID"] = new SelectList(_context.VModel, "MID", "MID", vehicle.MID);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["MID"] = new SelectList(_context.VModel, "MID", "MID", vehicle.MID);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LicensePNr,VinMark,MID")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["MID"] = new SelectList(_context.VModel, "MID", "MID", vehicle.MID);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
