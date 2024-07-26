using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class AddressesRepository : IAddressesRepository
{
    private readonly ClinicDbContext _context;

    public AddressesRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(Address address)
    {
        var addressEntity = new AddressEntity()
        {
            Id = address.Id,
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
    }

    public async Task Update(
        Guid id,
        string country,
        string region,
        string city,
        string street,
        int houseNumber,
        int apartmentNumber,
        string? description,
        string? pavilion)
    {
        await _context.Addresses
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Country, country)
                .SetProperty(x => x.Region, region)
                .SetProperty(x => x.City, city)
                .SetProperty(x => x.Street, street)
                .SetProperty(x => x.HouseNumber, houseNumber)
                .SetProperty(x => x.ApartmentNumber, apartmentNumber)
                .SetProperty(x => x.Description, description)
                .SetProperty(x => x.Pavilion, pavilion));
    }

    public async Task<Address> GetById(Guid id)
    {
        var addressEntity = await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception($"Address with ID {id} not found.");

        var address = Address.Create(
            addressEntity.Id,
            addressEntity.Country,
            addressEntity.Region,
            addressEntity.City,
            addressEntity.Street,
            addressEntity.HouseNumber,
            addressEntity.ApartmentNumber,
            addressEntity.Description,
            addressEntity.Pavilion).Value;

        return address;
    }

    public async Task Delete(Guid id)
    {
        await _context.Addresses
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}