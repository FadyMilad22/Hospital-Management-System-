using HospitalManagementSystem2.Models;

namespace HospitalManagementSystem2.Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        public List<Appointment>GetAllByPatient(int patientId);
    }
}
