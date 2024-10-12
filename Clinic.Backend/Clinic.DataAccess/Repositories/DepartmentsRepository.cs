using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly ClinicDbContext _context;

    public DepartmentsRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(Department department, Address address)
    {

        var addressEntity = new AddressEntity()
        {
            Country = address.Country,
            Region = address.Region,
            City = address.City,
            Street = address.Street,
            HouseNumber = address.HouseNumber,
            ApartmentNumber = address.ApartmentNumber,
            Description = address.Description,
            Pavilion = address.Pavilion,
        };

        await _context.Addresses.AddAsync(addressEntity);
        await _context.SaveChangesAsync();

        var departmentEntity = new DepartmentEntity()
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            AddressId = department.AddressId,
            Address = addressEntity
        };

        await _context.Departments.AddAsync(departmentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Department department, Address address)
    {
        var departmentEntity = await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == department.Id);

        if (departmentEntity == null)
        {
            throw new Exception($"Department with ID {department.Id} not found.");
        }

        departmentEntity.Name = department.Name;
        departmentEntity.Description = department.Description;

        var addressEntity = await _context.Addresses
            .FirstOrDefaultAsync(a => a.Id == department.AddressId);

        if (addressEntity == null)
        {
            addressEntity = new AddressEntity
            {
                Country = address.Country,
                Region = address.Region,
                City = address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                ApartmentNumber = address.ApartmentNumber,
                Description = address.Description,
                Pavilion = address.Pavilion
            };

            await _context.Addresses.AddAsync(addressEntity);
        }
        else
        {
            addressEntity.Country = address.Country;
            addressEntity.Region = address.Region;
            addressEntity.City = address.City;
            addressEntity.Street = address.Street;
            addressEntity.HouseNumber = address.HouseNumber;
            addressEntity.ApartmentNumber = address.ApartmentNumber;
            addressEntity.Description = address.Description;
            addressEntity.Pavilion = address.Pavilion;
        }

        departmentEntity.AddressId = addressEntity.Id;

        await _context.SaveChangesAsync(); 
    }


    public async Task<List<Department>> GetAll()
    {
        var departmentEntities = await _context.Departments
            .AsNoTracking()
            .ToListAsync();

        var departments = departmentEntities
            .Select(de => Department.Create(de.Id, de.Name, de.Description, de.AddressId).Value)
            .ToList();

        return departments;
    }

    public async Task Delete(Guid id)
    {
        await _context.Departments
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}