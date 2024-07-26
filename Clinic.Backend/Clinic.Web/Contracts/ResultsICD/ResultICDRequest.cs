namespace Clinic.Web.Contracts.ResultsICD;

public record ResultICDRequest(
    string ICDCode,
    string? Description,
    Guid ReceptionId);
