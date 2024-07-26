namespace Clinic.Web.Contracts.ResultsICD;

public record ResultICDResponse(
    Guid Id,
    string ICDCode,
    string? Description,
    Guid ReceptionId);
