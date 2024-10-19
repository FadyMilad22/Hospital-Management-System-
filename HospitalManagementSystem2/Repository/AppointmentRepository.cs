using HospitalManagementSystem2.Models;
using HospitalManagementSystem2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem2.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        HospitalContext context=new HospitalContext();
        public List<Appointment> GetAllByPatient(int patientId)
        {
          List<Appointment> appointmnets=  context.Appointments.Where(ap=>ap.PatientId == patientId)
              //el appointment 3andha tamam navigation property :
              //lel department patient,staff bas msh ha2dar a3ml include le property gowahom
              //y3ny msh ha2dar a3ml include name
                .Include(ap=>ap.Department)
                .Include(ap=>ap.Patient)
                  .ThenInclude(p=>p.User)
                   
                .Include(ap=>ap.Staff)
                  .ThenInclude(s=>s.User)
                    
                .ToList();

            return appointmnets;
        }
    }
}
