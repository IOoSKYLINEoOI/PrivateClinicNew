using CSharpFunctionalExtensions;

namespace Clinic.Core.Models
{
    public class Address
    {
        public const int MaxAddressLength = 60;
        public const int MaxNumberAddressLength = 99999;
        public const int MaxDescriptionAddressLength = 250;

        private Address(
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
            Id = id;
            Country = country;
            Region = region;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
            Description = description;
            Pavilion = pavilion;
        }

        public Guid Id { get; }
        public string Country { get; } = string.Empty;
        public string Region { get; } = string.Empty;
        public string City { get; } = string.Empty;
        public string Street { get; } = string.Empty;
        public int HouseNumber { get; }
        public string? Pavilion { get; }
        public int ApartmentNumber { get; }
        public string? Description { get; }

        public static Result<Address> Create(
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
            if (string.IsNullOrEmpty(country) || country.Length > MaxAddressLength)
            {
                return Result.Failure<Address>($"'{nameof(country)}' cannot be null, empty or more than {MaxAddressLength} characters.");
            }
            if (string.IsNullOrEmpty(region) || region.Length > MaxAddressLength)
            {
                return Result.Failure<Address>($"'{nameof(region)}' cannot be null, empty or more than {MaxAddressLength} characters.");
            }
            if (string.IsNullOrEmpty(city) || city.Length > MaxAddressLength)
            {
                return Result.Failure<Address>($"'{nameof(city)}' cannot be null, empty or more than {MaxAddressLength} characters.");
            }
            if (string.IsNullOrEmpty(street) || street.Length > MaxAddressLength)
            {
                return Result.Failure<Address>($"'{nameof(street)}' cannot be null, empty or more than {MaxAddressLength} characters.");
            }
            if (houseNumber < 0 || houseNumber > MaxNumberAddressLength)
            {
                return Result.Failure<Address>($"'{nameof(houseNumber)}' cannot be less than 0 or more than {MaxNumberAddressLength}.");
            }
            if (apartmentNumber < 0 || apartmentNumber > MaxNumberAddressLength)
            {
                return Result.Failure<Address>($"'{nameof(apartmentNumber)}' cannot be less than 0 or more than {MaxNumberAddressLength}.");
            }
            if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionAddressLength)
            {
                return Result.Failure<Address>($"'{nameof(description)}' cannot be more than {MaxDescriptionAddressLength} characters.");
            }
            if (!string.IsNullOrEmpty(pavilion) && pavilion.Length > MaxAddressLength)
            {
                return Result.Failure<Address>($"'{nameof(pavilion)}' cannot be more than {MaxAddressLength} characters.");
            }

            var address = new Address(
                id,
                country,
                region,
                city,
                street,
                houseNumber,
                apartmentNumber,
                description,
                pavilion);

            return Result.Success(address);
        }
    }
}
