using Clinic.Application.Services;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;

public class AddressService : IAddressService
{
    private readonly IAddressesRepository _addressesRepository;

    public AddressService(IAddressesRepository addressesRepository)
    {
        _addressesRepository = addressesRepository;
    }

    public async Task<Result> AddAddress(Address address)
    {
        await _addressesRepository.Add(address);
        return Result.Success();
    }

    public async Task<Result<Address>> GetByAddressId(Guid id)
    {
        var address = await _addressesRepository.GetById(id);
        return address != null ? Result.Success(address) : Result.Failure<Address>("Address not found");
    }

    public async Task<Result> UpdateAddress(
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
        await _addressesRepository.Update(id, country, region, city, street, houseNumber, apartmentNumber, description, pavilion);
        return Result.Success();
    }

    public async Task<Result> DeleteAddress(Guid id)
    {
        await _addressesRepository.Delete(id);
        return Result.Success();
    }
}