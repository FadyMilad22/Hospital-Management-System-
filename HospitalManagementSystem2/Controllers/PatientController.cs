using HospitalManagementSystem2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;

namespace HospitalManagementSystem2.Controllers
{
    public class PatientController : Controller
    {
        HospitalContext context ;
        public PatientController()
        {

        }
        [HttpGet]
        public IActionResult GetAllPatients()
        {           
            List<Patient> patients = context.Patients.Include(p => p.User).ToList();
            for (int i=0;i< patients.Count;i++) {
                if (patients[i].User.IsDeleted == true) {
                    patients.Remove(patients[i]);
                }
            }
            return View("GetAllPatients", patients);
        }
        [HttpGet]
        public IActionResult GetPatientById(int id)
        {
            Patient patient = context.Patients.Include(p => p.User).FirstOrDefault(e=>e.Id==id);
            if (patient == null)
            {
                TempData["ErrorMessage"] = "Patient not found!";
                return RedirectToAction("GetAllPatients");
            }
            return View("GetPatientById", patient);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["usersList"] = context.Users.ToList();
            return View("AddPatient");
        }
        [HttpPost]
        public IActionResult AddPatient(Patient patientFromReq) {
            
            if (patientFromReq.InsuranceProvider != null
                &&patientFromReq.InsuranceNumber!=null
                &&patientFromReq.Dob!=null
                &&patientFromReq.Address!=null
                &&patientFromReq.EmergencyContact!=null
                ) {
                try
                {
                    
                        context.Patients.Add(patientFromReq);
                        context.SaveChanges();
                    

                    return RedirectToAction("GetAllPatients");
                }
                catch (Exception ex)
                {
                    return Content("there  are error in db");
                }
            }
            
            ViewData["usersList"] = context.Users.ToList();
            return View("AddPatient", patientFromReq);

        }
        [HttpGet]
        public IActionResult EditPatient(int id)
        {
            Patient patient = context.Patients.FirstOrDefault(e => e.Id == id);
            if (patient == null)
            {
                return RedirectToAction("GetAllPatients");
            }
            ViewData["usersList"] = context.Users.ToList();
            return View("EditPatient", patient);

        }
        [HttpPost]
        public IActionResult SaveEditPatient(Patient patientFromReq ,int id)
        {
            if (patientFromReq.InsuranceProvider != null
                && patientFromReq.InsuranceNumber != null
                && patientFromReq.Dob != null
                && patientFromReq.Address != null
                && patientFromReq.EmergencyContact != null
           
                )
            {
                Patient patientFromDb = context.Patients.FirstOrDefault(e=>e.Id==id);
                if (patientFromDb != null)
                {
                    try
                    {
                        patientFromDb.Address = patientFromReq.Address;
                        patientFromDb.InsuranceNumber = patientFromReq.InsuranceNumber;
                        patientFromDb.InsuranceProvider = patientFromReq.InsuranceProvider;
                        patientFromDb.EmergencyContact = patientFromReq.EmergencyContact;
                        patientFromDb.Dob = patientFromReq.Dob;
                        patientFromDb.UserId = patientFromReq.UserId;                       
                        context.SaveChanges();
                        return RedirectToAction("GetAllPatients");
                    }
                    catch (Exception ex) {
                        return Content("there are problem in db");
                    }
                   
                }
                return NotFound(id);
            }
            
            ViewData["usersList"] = context.Users.ToList();
            return View("EditPatient", patientFromReq);


        }

        [HttpGet]
        public IActionResult DeletePatient(int id)
        {
         
            Patient patient = context.Patients.FirstOrDefault(e => e.Id == id);
            if (patient == null)
            {
                return RedirectToAction("GetAllPatients");
            }

            return View("DeletePatient",patient);



        }
        [HttpPost]
        public IActionResult SaveDeletePatient(int id)
        {
            Patient patient = context.Patients.FirstOrDefault(e => e.Id == id);
           
            if (patient != null)
            {
                User userFromDb = context.Users.FirstOrDefault(e => e.Id == patient.UserId);
                userFromDb.IsDeleted = true;
                userFromDb.Name = userFromDb.Name;
                userFromDb.Email = userFromDb.Email;
                userFromDb.PhoneNumber = userFromDb.PhoneNumber;
                userFromDb.RoleId = userFromDb.RoleId;
                userFromDb.Password = userFromDb.Password;
                context.SaveChanges();
                return RedirectToAction("GetAllPatients");


            }
            return NotFound();

       }


    }
}
