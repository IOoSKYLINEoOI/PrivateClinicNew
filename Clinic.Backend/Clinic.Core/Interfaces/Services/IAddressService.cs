using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services;

public interface IAddressService
{
    Task<Result> AddAddress(Address address);
    Task<Result> DeleteAddress(Guid id);
    Task<Result<Address>> GetByAddressId(Guid id);
    Task<Result> UpdateAddress(Guid id, string country, string region, string city, string street, int houseNumber, int apartmentNumber, string? description, string? pavilion);
}