export interface EmployeeRequest{
    hiringDate: string;
    dateOfDismissal?: string;
    description?: string;
    userId: string;
}

export const getAllEmployees = async () => {
    const response = await fetch("https://localhost:7179/employees");

    return response.json();
};

export const createEmployee = async (employeeRequest: EmployeeRequest) =>{
    await fetch("https://localhost:7179/employees", {
        method: "POST",
        headers: {
            "content-type": "aplication/json",
        },
        body: JSON.stringify(employeeRequest),
    });
};

export const updateEmployee = async (id: string, employeeRequest: EmployeeRequest) =>{
    await fetch(`https://localhost:7179/employees/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "aplication/json",
        },
        body: JSON.stringify(employeeRequest),
    });
};

export const deleteEmployee = async (id: string) =>{
    await fetch(`https://localhost:7179/employees/${id}`, {
        method: "DELETE",
    });
};