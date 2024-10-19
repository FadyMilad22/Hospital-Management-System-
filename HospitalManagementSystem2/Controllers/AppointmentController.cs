using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem2.Controllers
{
    public class AppointmentController : Controller
    {
        /*
         •	Get appointments by patient 
•	Get appointments by doctor. 
•	Check availability of appointment
•	 get available appointments time slots.

         */
        //patientid 
        public IActionResult GetAppointmentsByPatient()
        {
            return View();
        }
    }
}
