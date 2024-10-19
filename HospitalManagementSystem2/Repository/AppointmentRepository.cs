﻿using HospitalManagementSystem2.Models;
using HospitalManagementSystem2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem2.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        HospitalContext context;
        public AppointmentRepository(HospitalContext _context)
        {
            context = _context;
        }

        public List<Appointment> GetAllByPatient(int patientId)
        {
            List<Appointment> appointmnets = context.Appointments.Where(ap => ap.PatientId == patientId)

                  .Include(ap => ap.Department)
                  .Include(ap => ap.Staff)
                    .ThenInclude(s => s.User)
                  .ToList();

            return appointmnets;
        }
        public List<Appointment> GetAllByDoctor(int StaffId)
        {
            List<Appointment> appointmnets = 
                context.Appointments
                .Where(ap => ap.StaffId == StaffId)
                .Include(ap => ap.Department)
                .Include(ap => ap.Patient)
                  .ThenInclude(p => p.User)
                .ToList();

            return appointmnets;
        }


        public Appointment GetById(int AppointmentId)
        {

            Appointment? appointment = context.Appointments
                .Include(ap => ap.Department)
                .Include(ap => ap.Staff)
                    .ThenInclude(s => s.User)
                .FirstOrDefault(ap => ap.Id == AppointmentId);
                 
            if (appointment == null)
            {
            throw new KeyNotFoundException($"Appointment with ID {appointment.Id} not found.");
            }

            return appointment;

           
        }
    }
}