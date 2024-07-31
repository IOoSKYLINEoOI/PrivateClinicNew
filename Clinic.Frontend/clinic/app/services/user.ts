export interface LoginUserRequest {
    email: string;
    password: string;
}

export interface RegisterUserRequest {
    firstName: string;
    lastName: string;
    fatherName?: string;
    dateOfBirth: string;
    email: string;
    phoneNumber: string;
    password: string;
}

export interface UpdateUserRequest {
    firstName: string;
    lastName: string;
    fatherName?: string;
    dateOfBirth: string;
    addressId?: string;
    fileName?: string;
}

export const registerUser = async (registerUserRequest: RegisterUserRequest): Promise<void> => {
    const response = await fetch("https://localhost:7179/users/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(registerUserRequest),
    });

    if (!response.ok) {
        throw new Error('Failed to register user');
    }
};

export const loginUser = async (loginUserRequest: LoginUserRequest): Promise<string> => {
    const response = await fetch("https://localhost:7179/users/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(loginUserRequest),
    });

    if (!response.ok) {
        throw new Error('Failed to log in');
    }

    const data = await response.json();
    return data.id; 
};

export const updateUser = async (id: string, updateRequest: UpdateUserRequest): Promise<void> => {
    await fetch(`https://localhost:7179/updateProfile/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json", // Исправлено "content-type" на "Content-Type"
        },
        body: JSON.stringify(updateRequest),
    });
};
