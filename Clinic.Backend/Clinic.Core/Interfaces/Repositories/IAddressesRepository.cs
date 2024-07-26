using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IAddressesRepository
{
    Task Add(Address address);
    Task Delete(Guid id);
    Task<Address> GetById(Guid id);
    Task Update(
        Guid id, 
        string country, 
        string region, 
        string city, 
        string street, 
        int houseNumber, 
        int apartmentNumber, 
        string? description, 
        string? pavilion);
}
