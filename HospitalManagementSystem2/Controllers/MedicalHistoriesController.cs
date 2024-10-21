using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagementSystem2.Controllers
{
    public class MedicalHistoriesController : Controller
    {
        private readonly HospitalContext _context;

        public MedicalHistoriesController(HospitalContext context)
        {
            _context = context;
        }

        // GET: MedicalHistories (for users without specifying patient ID)
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.MedicalHistories.Include(m => m.Patient).Include(m => m.Staff);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: MedicalHistories/PatientHistory/5 (for patients with specified patient ID)
        public async Task<IActionResult> PatientHistory(int? patientId)
        {
            if (patientId == null)
            {
                return NotFound();
            }

            var medicalHistories = await _context.MedicalHistories
                .Include(m => m.Patient)
                .Include(m => m.Staff)
                .Where(m => m.PatientId == patientId)
                .ToListAsync();

            if (medicalHistories == null || !medicalHistories.Any())
            {
                return NotFound();
            }

            return View("PatientHistory", medicalHistories);
        }

        // POST: MedicalHistories/CreateOrUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate([Bind("Id,PatientId,StaffId,Diagnosis,TreatmentPlan,Prescription,VisitedDate")] MedicalHistory medicalHistory)
        {
            if (ModelState.IsValid)
            {
                if (medicalHistory.Id == 0)
                {
                    _context.Add(medicalHistory);
                }
                else
                {
                    try
                    {
                        _context.Update(medicalHistory);
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
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", medicalHistory.PatientId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Id", medicalHistory.StaffId);
            return View(medicalHistory);
        }

        private bool MedicalHistoryExists(int id)
        {
            return _context.MedicalHistories.Any(e => e.Id == id);
        }
    }
}
