using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicDbContext _context;

        public AppointmentRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task Add(Appointment appointment)
        {
            var appointmentEntity = new AppointmentEntity
            {
                Id = appointment.Id,
                UserId = appointment.UserId,
                ReceptionId = appointment.ReceptionId,
                TimeSlotId = appointment.TimeSlotId,
                DateOfBooking = appointment.DateOfBooking,
                StatusAppointmentId = appointment.StatusAppointmentId
            };

            await _context.Appointments.AddAsync(appointmentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Appointment updatedAppointment)
        {
            var existingAppointment = await _context.Appointments.FindAsync(id);
            if (existingAppointment == null)
            {
                throw new Exception($"Appointment with ID {id} not found.");
            }

            existingAppointment.UserId = updatedAppointment.UserId;
            existingAppointment.ReceptionId = updatedAppointment.ReceptionId;
            existingAppointment.TimeSlotId = updatedAppointment.TimeSlotId;
            existingAppointment.DateOfBooking = updatedAppointment.DateOfBooking;
            existingAppointment.StatusAppointmentId = updatedAppointment.StatusAppointmentId;

            await _context.SaveChangesAsync();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            var appointmentEntity = await _context.Appointments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return appointmentEntity == null ? null : new Appointment(
                appointmentEntity.Id,
                appointmentEntity.UserId,
                appointmentEntity.ReceptionId,
                appointmentEntity.TimeSlotId,
                appointmentEntity.DateOfBooking,
                appointmentEntity.StatusAppointmentId);
        }

        public async Task Delete(Guid id)
        {
            var existingAppointment = await _context.Appointments.FindAsync(id);
            if (existingAppointment == null)
            {
                throw new Exception($"Appointment with ID {id} not found.");
            }

            _context.Appointments.Remove(existingAppointment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetAll()
        {
            var appointmentEntities = await _context.Appointments
                .AsNoTracking()
                .ToListAsync();

            return appointmentEntities.Select(a => new Appointment(
                a.Id,
                a.UserId,
                a.ReceptionId,
                a.TimeSlotId,
                a.DateOfBooking,
                a.StatusAppointmentId)).ToList();
        }
    }
}
