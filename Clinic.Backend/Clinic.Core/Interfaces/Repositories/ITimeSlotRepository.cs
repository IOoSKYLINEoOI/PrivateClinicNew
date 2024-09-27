using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface ITimeSlotRepository
{
    Task Add(TimeSlot timeSlot);
    Task<TimeSlot?> GetById(Guid id);
    Task<List<TimeSlot>> GetAll();
    Task Update(Guid id, TimeOnly startTime, TimeOnly endTime, bool isAvailable);
    Task Delete(Guid id);
}
