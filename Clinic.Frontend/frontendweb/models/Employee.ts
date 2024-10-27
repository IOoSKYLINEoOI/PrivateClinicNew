export interface EmployeeRequest {
    HiringDate: string; 
    DateOfDismissal?: string;
    Description?: string;
    UserId: string;
}

export interface EmployeeResponse extends EmployeeRequest {
    id: string;
}
