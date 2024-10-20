using HospitalManagementSystem2.Models;

namespace HospitalManagementSystem2.Repository.Interfaces
{
    public interface IStaffScheduleRepository
    {
        public List<Schedule> getAvailableTimeSlots(int staffid);
    }
}
