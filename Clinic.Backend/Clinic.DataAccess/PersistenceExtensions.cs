using Clinic.Core.Interfaces.Repositories;
using Clinic.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.DataAccess;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ClinicContext")
            ?? throw new InvalidOperationException("Connection string 'ClinicContext' not found.")));

        services.AddScoped<IAddressesRepository, AddressesRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
        services.AddScoped<IEmployeesDepartmentsRepository, EmployeesDepartmentsRepository>();
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<IImagesRepository, ImagesRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPositionsRepository, PositionsRepository>();
        services.AddScoped<IReceptionsRepository, ReceptionsRepository>();
        services.AddScoped<IResultsICDRepository, ResultsICDRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();

        return services;
    }
}
