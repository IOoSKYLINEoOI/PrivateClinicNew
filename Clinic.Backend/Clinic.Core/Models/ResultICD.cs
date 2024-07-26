using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class ResultICD
{
    public const int MaxIcdLength = 60;
    public const int MaxDescriptionResultLength = 250;

    private ResultICD(Guid id, string icdCode, string? description, Guid receptionId)
    {
        Id = id;
        ICDCode = icdCode;
        Description = description;
        ReceptionId = receptionId;
    }

    public Guid Id { get; set; }
    public string ICDCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid ReceptionId { get; set; }

    public static Result<ResultICD> Create(Guid id, string icdCode, string? description, Guid receptionId)
    {
        if (string.IsNullOrEmpty(icdCode) || icdCode.Length > MaxIcdLength)
        {
            return Result.Failure<ResultICD>($"'{nameof(icdCode)}' cannot be null, empty, or more than {MaxIcdLength} characters.");
        }
        if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionResultLength)
        {
            return Result.Failure<ResultICD>($"'{nameof(description)}' cannot be more than {MaxDescriptionResultLength} characters.");
        }

        var resultICD = new ResultICD(id, icdCode, description, receptionId);

        return Result.Success(resultICD);
    }
}