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












          /*  var availableschedules =
                 context.StaffSchedules
                 .Include(ss => ss.Schedule)
                 .Include(ss => ss.Staff)
                 .Where(ss => ss.StaffId == staffId)
                 .Select(ss => new
                 {
                     Schedule = ss.Schedule,
                     Appointments = context.Appointments
                    .Where(a => a.StaffId == staffId &&
                                a.AppointmentDateTime.Date == ss.Schedule.Date) // Match only on the Date
                    .AsEnumerable() // Switch to client-side evaluation for the TimeOnly comparison
                    .Where(a => TimeOnly.FromDateTime(a.AppointmentDateTime) >= ss.Schedule.AvailableFrom && // Check if appointment time is after available from
                                TimeOnly.FromDateTime(a.AppointmentDateTime) <= ss.Schedule.AvailableTo) // Check if appointment time is before available to
                    .ToList()
                 })
                 .Where(x => !x.Appointments.Any()) // Check for no appointments (not booked)
                 .Select(x => x.Schedule)
                .ToList();*/
            return availableSchedules;
        }
    }
}
