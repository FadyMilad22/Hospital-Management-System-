using HospitalManagementSystem2.Models;
using HospitalManagementSystem2.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem2.Controllers
{
    public class AppointmentController : Controller
    {
        IAppointmentRepository appointmentRepository;

        public AppointmentController(IAppointmentRepository _appointmnetrepo)
        {
           appointmentRepository= _appointmnetrepo;
        }
        //Appointment/Test
        public IActionResult Test() { 
          return View();
        }

        //Appointment/GetAppointmentsByPatient/id
        public IActionResult GetAppointmentsByPatient(int Id)
        {
            
            List <Appointment> appointments=appointmentRepository.GetAllByPatient(Id);

            return View("ViewAppointments",appointments);
        }
    }
}
