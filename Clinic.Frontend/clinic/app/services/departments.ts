export interface DepartmentRequest{
    name: string;
    description: string;
    addressId : string;
}

export const getAllDepartments = async () => {
    const response = await fetch("https://localhost:7179/departments");

    return response.json();
};

export const createDepartment = async (departmentRequest: DepartmentRequest) =>{
    await fetch("https://localhost:7179/departments", {
        method: "POST",
        headers: {
            "content-type": "aplication/json",
        },
        body: JSON.stringify(departmentRequest),
    });
};

export const updateDepartment = async (id: string, departmentRequest: DepartmentRequest) =>{
    await fetch(`https://localhost:7179/departments/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "aplication/json",
        },
        body: JSON.stringify(departmentRequest),
    });
};

export const deleteDepartment = async (id: string) =>{
    await fetch(`https://localhost:7179/departments/${id}`, {
        method: "DELETE",
    });
};