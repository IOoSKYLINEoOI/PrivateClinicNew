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
        // Подключение к базе данных
        services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ClinicContext")
            ?? throw new InvalidOperationException("Connection string 'ClinicContext' not found.")));

        // Регистрация зависимостей
        services.AddScoped<IAddressesRepository, AddressesRepository>();
        services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
        services.AddScoped<IEmployeesDepartmentsRepository, EmployeesDepartmentsRepository>();
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<IImagesRepository, ImagesRepository>();
        services.AddScoped<IPositionsRepository, PositionsRepository>();
        services.AddScoped<IReceptionsRepository, ReceptionsRepository>();
        services.AddScoped<IResultsICDRepository, ResultsICDRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}
