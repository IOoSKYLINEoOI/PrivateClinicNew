using CSharpFunctionalExtensions;

namespace Clinic.Core.Models
{
    public class Department
    {
        public const int MaxDepartmentLength = 60;
        public const int MaxDescriptionDepartmentLength = 250;

        private Department(Guid id, string name, string? description, Address address)
        {
            Id = id;
            Name = name;
            Description = description;
            Address = address;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public Address Address { get; private set; }

        public static Result<Department> Create(Guid id, string name, string? description, Address address)
        {
            if (string.IsNullOrEmpty(name) || name.Length > MaxDepartmentLength)
            {
                return Result.Failure<Department>($"'{nameof(name)}' cannot be null, empty or more than {MaxDepartmentLength} characters.");
            }
            if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionDepartmentLength)
            {
                return Result.Failure<Department>($"'{nameof(description)}' cannot be more than {MaxDescriptionDepartmentLength} characters.");
            }

            var department = new Department(id, name, description, address);
            return Result.Success(department);
        }
    }
}
