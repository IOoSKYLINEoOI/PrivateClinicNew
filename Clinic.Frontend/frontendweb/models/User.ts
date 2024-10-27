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

  interface RegisterFormValues extends RegisterUserRequest {
    confirmPassword: string;
  }

  export interface UserResponse {
    id: string; // или Guid, в зависимости от того, как вы хотите это использовать
    firstName: string;
    lastName: string;
    fatherName?: string;
    phoneNumber: string;
    dateOfBirth: string; // или Date, в зависимости от обработки даты
    imageId?: string;
    email: string;
    description?: string;
  }
  