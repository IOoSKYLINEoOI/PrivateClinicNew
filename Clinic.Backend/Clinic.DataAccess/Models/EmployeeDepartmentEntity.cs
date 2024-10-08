﻿namespace Clinic.DataAccess.Models;

public class EmployeeDepartmentEntity
{
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }
    public string? Description { get; set; }
    public int PositionId { get; set; }

    public PositionEntity? Position { get; set; }
}
