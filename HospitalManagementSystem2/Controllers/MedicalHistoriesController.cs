using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem2.Data;
using HospitalManagementSystem2.Models;

namespace HospitalManagementSystem2.Controllers
{
    public class MedicalHistoriesController : Controller
    {
        private readonly HospitalManagementSystem2Context _context;

        public MedicalHistoriesController(HospitalManagementSystem2Context context)
        {
            _context = context;
        }

        // GET: MedicalHistories
        public async Task<IActionResult> Index()
        {
            var hospitalManagementSystem2Context = _context.MedicalHistory.Include(m => m.Patient).Include(m => m.Staff);
            return View(await hospitalManagementSystem2Context.ToListAsync());
        }

        // GET: MedicalHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistory
                .Include(m => m.Patient)
                .Include(m => m.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // GET: MedicalHistories/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Id");
            ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "Id", "Id");
            return View();
        }

        // POST: MedicalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,StaffId,Diagnosis,TreatmentPlan,Prescription,VisitedDate")] MedicalHistory medicalHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Id", medicalHistory.PatientId);
            ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "Id", "Id", medicalHistory.StaffId);
            return View(medicalHistory);
        }

        // GET: MedicalHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistory.FindAsync(id);
            if (medicalHistory == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Id", medicalHistory.PatientId);
            ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "Id", "Id", medicalHistory.StaffId);
            return View(medicalHistory);
        }

        // POST: MedicalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,StaffId,Diagnosis,TreatmentPlan,Prescription,VisitedDate")] MedicalHistory medicalHistory)
        {
            if (id != medicalHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalHistoryExists(medicalHistory.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Id", medicalHistory.PatientId);
            ViewData["StaffId"] = new SelectList(_context.Set<Staff>(), "Id", "Id", medicalHistory.StaffId);
            return View(medicalHistory);
        }

        // GET: MedicalHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistory
                .Include(m => m.Patient)
                .Include(m => m.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // POST: MedicalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalHistory = await _context.MedicalHistory.FindAsync(id);
            if (medicalHistory != null)
            {
                _context.MedicalHistory.Remove(medicalHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalHistoryExists(int id)
        {
            return _context.MedicalHistory.Any(e => e.Id == id);
        }
    }
}
