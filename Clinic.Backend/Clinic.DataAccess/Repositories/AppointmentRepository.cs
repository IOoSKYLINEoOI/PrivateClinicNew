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

        public async Task Update(
            Guid id,
            Guid userId,
            Guid? receptionId,
            Guid timeSlotId,
            DateTime dateOfBooking,
            int statusAppointmentId)
        {
            await _context.Appointments
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.UserId, userId)
                    .SetProperty(x => x.ReceptionId, receptionId)
                    .SetProperty(x => x.TimeSlotId, timeSlotId)
                    .SetProperty(x => x.DateOfBooking, dateOfBooking)
                    .SetProperty(x => x.StatusAppointmentId, statusAppointmentId));
        }

        public async Task<Appointment> GetById(Guid id)
        {
            var appointmentEntity = await _context.Appointments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id) ?? throw new Exception($"Appointment with ID {id} not found.");

            var appointment = Appointment.Create(
                appointmentEntity.Id,
                appointmentEntity.UserId,
                appointmentEntity.ReceptionId,
                appointmentEntity.TimeSlotId,
                appointmentEntity.DateOfBooking,
                appointmentEntity.StatusAppointmentId).Value;

            return appointment;
        }

        public async Task Delete(Guid id)
        {
            await _context.Appointments
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Appointment>> GetAll()
        {
            var appointmentEntities = await _context.Appointments
                .AsNoTracking()
                .ToListAsync();

            var appointments = appointmentEntities
                .Select(a => Appointment.Create(a.Id, a.UserId, a.ReceptionId, a.TimeSlotId, a.DateOfBooking, a.StatusAppointmentId).Value)
                .ToList();

            return appointments;
        }
    }
}
