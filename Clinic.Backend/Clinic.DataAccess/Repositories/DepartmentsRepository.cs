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
            AddressId = addressEntity.Id,
            Address = addressEntity
        };

        await _context.Departments.AddAsync(departmentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Department department, Address address)
    {
        var departmentEntity = await _context.Departments
            .Include(d => d.Address) // включаем адрес для обновления
            .FirstOrDefaultAsync(d => d.Id == department.Id);

        if (departmentEntity == null)
        {
            throw new Exception($"Department with ID {department.Id} not found.");
        }

        departmentEntity.Name = department.Name;
        departmentEntity.Description = department.Description;

        if (departmentEntity.Address != null)
        {
            departmentEntity.Address.Country = address.Country;
            departmentEntity.Address.Region = address.Region;
            departmentEntity.Address.City = address.City;
            departmentEntity.Address.Street = address.Street;
            departmentEntity.Address.HouseNumber = address.HouseNumber;
            departmentEntity.Address.ApartmentNumber = address.ApartmentNumber;
            departmentEntity.Address.Description = address.Description;
            departmentEntity.Address.Pavilion = address.Pavilion;
        }
        else
        {
            var addressEntity = new AddressEntity
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
            departmentEntity.AddressId = addressEntity.Id; 
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Department>> GetAll()
    {
        var departmentEntities = await _context.Departments
            .Include(d => d.Address)
            .AsNoTracking()
            .ToListAsync();

        var departments = departmentEntities
            .Select(de =>
            {
                if (de.Address == null)
                {
                    throw new Exception($"Address for Department with ID {de.Id} is null.");
                }

                var addressResult = Address.Create(
                    de.Address.Id,
                    de.Address.Country,
                    de.Address.Region,
                    de.Address.City,
                    de.Address.Street,
                    de.Address.HouseNumber,
                    de.Address.ApartmentNumber,
                    de.Address.Description,
                    de.Address.Pavilion);

                var departmentResult = Department.Create(
                    de.Id,
                    de.Name,
                    de.Description,
                    addressResult.Value); 


                if (departmentResult.IsFailure)
                {
                    throw new Exception(departmentResult.Error); 
                }

                return departmentResult.Value;
            })
            .ToList();

        return departments;
    }



    public async Task Delete(Guid id)
    {
        var departmentEntity = await _context.Departments.FindAsync(id);
        if (departmentEntity != null)
        {
            _context.Departments.Remove(departmentEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Department> GetById(Guid id)
    {
        var departmentEntity = await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id) ?? throw new Exception($"Department with ID {id} not found.");

        if (departmentEntity.Address == null)
        {
            throw new Exception($"Address for Department with ID {departmentEntity.Id} is null.");
        }

        var address = Address.Create(
                    departmentEntity.Address.Id,
                    departmentEntity.Address.Country,
                    departmentEntity.Address.Region,
                    departmentEntity.Address.City,
                    departmentEntity.Address.Street,
                    departmentEntity.Address.HouseNumber,
                    departmentEntity.Address.ApartmentNumber,
                    departmentEntity.Address.Description,
                    departmentEntity.Address.Pavilion);

        var department = Department.Create(
            departmentEntity.Id,
            departmentEntity.Name,
            departmentEntity.Description,
            address.Value).Value;

        return department;
    }

}
