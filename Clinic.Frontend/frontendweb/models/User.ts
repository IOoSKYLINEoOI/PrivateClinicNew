export interface RegisterUserRequest {
    firstName: string;
    lastName: string;
    fatherName?: string;
    dateOfBirth: string; // Для DateOnly используем строку в формате 'yyyy-MM-dd'
    email: string;
    phoneNumber: string;
    password: string;
  }
  
  export interface LoginUserRequest {
    email: string;
    password: string;
  }
  
  export interface UserUpdateRequest {
    firstName: string;
    lastName: string;
    fatherName?: string;
    dateOfBirth: string; // Для DateOnly используем строку в формате 'yyyy-MM-dd'
    addressId?: string;
    fileName?: File | null;
  }
  