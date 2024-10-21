using HospitalManagementSystem2.Models;
using HospitalManagementSystem2.Repository;
using HospitalManagementSystem2.Repository.Interfaces;
using HospitalManagementSystem2.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem2.Controllers
{
    public class AppointmentController : Controller
    {
      private readonly  IAppointmentRepository appointmentRepository;
     private readonly   IStaffScheduleRepository scheduleRepository;
     private readonly   IEmailSender emailSender;
        public AppointmentController(IAppointmentRepository _appointmnetrepo, IStaffScheduleRepository _scheduleRepository,IEmailSender _emailSender)
        {
           appointmentRepository= _appointmnetrepo;
            scheduleRepository= _scheduleRepository;
            emailSender = _emailSender;
        }
        //Appointment/Test
        public IActionResult Test() { 
          return View();
        }

        //Appointment/GetAppointmentsByPatient?PatientId=
        public IActionResult GetAppointmentsByPatient(int PatientId)
        {
            
            List <Appointment> appointments=appointmentRepository.GetAllByPatient(PatientId);

            return View("ViewAppointments",appointments);
        }

        //Appointment/GetAppointmentsByDoctor?StaffId=
        public IActionResult GetAppointmentsByDoctor(int StaffId)
        {

            List<Appointment> appointments = appointmentRepository.GetAllByDoctor(StaffId);

            return View("ViewAppointments", appointments);
        }

        //Appointment/GetAppointmentsById/
        
        public IActionResult GetAppointmentById(int Id)
        {

            Appointment appointment = appointmentRepository.GetById(Id);

            return View("SpecificAppointment", appointment);
        }
        //Appointment/GetAvailableTimeSlots?Id=1
        public IActionResult GetAvailableTimeSlots(int Id)//
        {
            List<Schedule> schedule = scheduleRepository.getAvailableTimeSlots(Id);

            return View("DoctorAvailableTimeSlots", schedule);
        }

        public async Task<IActionResult> TriggerEmail()
        {
         
            try
            {
                // Send the email
                await emailSender.SendEmailAsync("marwa.elfayoumy9@gmail.com","Test", "\"<h1>This is an automated notification</h1><p>Your appointment is scheduled for tomorrow.</p>");
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                // Handle any errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
