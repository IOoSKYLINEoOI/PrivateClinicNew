export interface EmployeeDepartmentRequest {
    EmployeeId: string; 
    DepartmentId: string;
    Description?: string;
    PositionId: number;
}

export interface EmployeeDepartmentResponse extends EmployeeDepartmentRequest {
}