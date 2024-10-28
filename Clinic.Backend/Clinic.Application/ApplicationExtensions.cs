using Clinic.Application.Services;
using Clinic.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Application;


public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeDepartmentService, EmployeeDepartmentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IReceptionService, ReceptionService>();
        services.AddScoped<IResultICDService, ResultICDService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<ITimeSlotService, TimeSlotService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}