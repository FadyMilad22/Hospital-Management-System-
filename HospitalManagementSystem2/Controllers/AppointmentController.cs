using HospitalManagementSystem2.Models;
using HospitalManagementSystem2.Repository;
using HospitalManagementSystem2.Repository.Interfaces;
using HospitalManagementSystem2.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem2.Controllers
{
    public class AppointmentController : Controller
    {
        IAppointmentRepository appointmentRepository;
        IStaffScheduleRepository scheduleRepository;

        public AppointmentController(IAppointmentRepository _appointmnetrepo, IStaffScheduleRepository _scheduleRepository)
        {
           appointmentRepository= _appointmnetrepo;
            scheduleRepository= _scheduleRepository;
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
        //Appointment/GetAvailableTimeSlots?StaffId=1
        public IActionResult GetAvailableTimeSlots(int StaffId)//
        {
            List<Schedule> schedule = scheduleRepository.getAvailableTimeSlots(StaffId);

            return View("DoctorAvailableTimeSlots", schedule);
        }

    }
}
