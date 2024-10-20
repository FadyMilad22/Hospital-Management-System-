using HospitalManagementSystem2.Models;
using HospitalManagementSystem2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem2.Repository
{
    public class StaffScheduleRepository : IStaffScheduleRepository
    {
        HospitalContext context;
        public StaffScheduleRepository(HospitalContext _context)
        {
            context = _context;
        }

        public List<Schedule> getAvailableTimeSlots(int staffId)
        {
            var staffSchedules =  context.StaffSchedules
               .Include(ss => ss.Schedule) 
               .Include(ss => ss.Staff) 
               .Where(ss => ss.StaffId == staffId) 
               .ToList();


            var appointments =  context.Appointments
           .Where(a => a.StaffId == staffId)
           .ToList();


            var availableSchedules = staffSchedules
            .Where(ss => !appointments.Any(appointment =>
                appointment.AppointmentDateTime.Date == ss.Schedule.Date && // Match the date
                TimeOnly.FromDateTime(appointment.AppointmentDateTime) >= ss.Schedule.AvailableFrom && // Check appointment start time
                TimeOnly.FromDateTime(appointment.AppointmentDateTime) <= ss.Schedule.AvailableTo)) // Check appointment end time
            .Select(ss => ss.Schedule) 
            .ToList();


            return availableSchedules;
        }
    }
}
