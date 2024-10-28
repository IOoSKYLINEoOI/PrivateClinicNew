export interface RegisterUserRequest {
    firstName: string;
    lastName: string;
    fatherName?: string;
    dateOfBirth: string; 
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
    dateOfBirth: string; 
    addressId?: string;
    fileName?: File | null;
  }

  interface RegisterFormValues extends RegisterUserRequest {
    confirmPassword: string;
  }

  export interface UserResponse {
    id: string; 
    firstName: string;
    lastName: string;
    fatherName?: string;
    phoneNumber: string;
    dateOfBirth: string; 
    imageId?: string;
    email: string;
    description?: string;
  }
  