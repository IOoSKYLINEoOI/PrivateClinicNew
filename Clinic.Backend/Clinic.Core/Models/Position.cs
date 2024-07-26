using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class Position
{
    public const int MaxPositionLength = 60;
    public const int MaxDescriptionPositionLength = 250;

    private Position(Guid id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Description { get; }

    public static Result<Position> Create(Guid id, string name, string? description)
    {
        if (string.IsNullOrEmpty(name) || name.Length > MaxPositionLength)
        {
            return Result.Failure<Position>($"'{nameof(name)}' cannot be null, empty or more than {MaxPositionLength} characters.");
        }
        if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionPositionLength)
        {
            return Result.Failure<Position>($"'{nameof(description)}' cannot be more than {MaxDescriptionPositionLength} characters.");
        }

        var position = new Position(id, name, description);

        return Result.Success(position);
    }
}