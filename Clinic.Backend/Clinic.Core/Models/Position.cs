using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class Position
{
    public const int MaxPositionNameLength = 100;
    public const int MaxDescriptionPositionLength = 255;

    private Position(int id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public int Id { get; }
    public string Name { get; }
    public string? Description { get; }

    public static Result<Position> Create(int id, string name, string? description)
    {
        if (string.IsNullOrEmpty(name) || name.Length > MaxPositionNameLength)
        {
            return Result.Failure<Position>($"'{nameof(name)}' cannot be null, empty or more than {MaxPositionNameLength} characters.");
        }
        if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionPositionLength)
        {
            return Result.Failure<Position>($"'{nameof(description)}' cannot be more than {MaxDescriptionPositionLength} characters.");
        }

        var position = new Position(id, name, description);

        return Result.Success(position);
    }
}