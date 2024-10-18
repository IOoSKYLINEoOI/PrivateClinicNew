using Clinic.Web.Contracts.Addresses;

namespace Clinic.Web.Contracts.Departments;

public record DepartmentResponse(
    Guid id,
    string name,
    string? description,
    AddressResponse address);

