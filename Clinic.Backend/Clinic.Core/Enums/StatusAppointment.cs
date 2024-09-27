namespace Clinic.Core.Enums;

public enum StatusAppointment
{
    Scheduled = 1,    // Запланировано
    Confirmed = 2,    // Подтверждено
    CheckedIn = 3,    // Пациент пришел
    InProgress = 4,   // Прием в процессе
    Completed = 5,    // Завершено
    Cancelled = 6,    // Отменено
    NoShow = 7,       // Не явился
    Rescheduled = 8,  // Перенесено
    Pending = 9,      // Ожидает подтверждения
    Declined = 10     // Отклонено
}

