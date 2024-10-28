import axios from 'axios';
import { DepartmentRequest, DepartmentResponse } from '@/models/Department';
import { RegisterUserRequest, LoginUserRequest, UserUpdateRequest, UserResponse } from '@/models/User';
import { EmployeeRequest, EmployeeResponse } from '@/models/Employee';
import { EmployeeDepartmentRequest, EmployeeDepartmentResponse } from '@/models/EmployeeDepartment';

const api = axios.create({
  baseURL: 'https://localhost:7179', 
  withCredentials: true, 
});

//Department
export const getDepartments = async (): Promise<DepartmentResponse[]> => {
  const response = await api.get('/departments');
  return response.data;
};

export const createDepartment = async (data: DepartmentRequest): Promise<string> => {
  const response = await api.post('/departments', data);
  return response.data;
};

export const updateDepartment = async (id: string, data: DepartmentRequest): Promise<string> => {
  const response = await api.put(`/departments/${id}`, data);
  return response.data;
};

export const deleteDepartment = async (id: string): Promise<string> => {
  const response = await api.delete(`/departments/${id}`);
  return response.data;
};

//User
export const registerUser = async (data: RegisterUserRequest): Promise<void> => {
  await api.post('/users/register', data);
};

export const loginUser = async (data: LoginUserRequest): Promise<void> => {
  await api.post('/users/login', data);
};

export const updateUserProfile = async (id: string, data: UserUpdateRequest): Promise<void> => {
  await api.post(`/users/updateProfile?id=${id}`, data);
};

export const logoutUser = async (): Promise<void> => {
  await api.post(`/users/logout`);
};

export const getUserProfile = async (): Promise<UserResponse> => {
  const response = await api.get('users/me');
  return response.data;
};

export const getUserProfileId = async (id: string): Promise<UserResponse> => {
  const response = await api.post(`/employee/${id}`);
  return response.data;
};

export const getUserEmail = async (id: string): Promise<UserResponse> => {
  const response = await api.post(`/employee/${id}`);
  return response.data;
};

//Employee
export const createEmployee = async (data: EmployeeRequest): Promise<string> => {
  const response = await api.post('/employees', data);
  return response.data;
};

export const getEmployees = async (): Promise<EmployeeResponse[]> => {
  const response = await api.get('/employees');
  return response.data;
};

export const updateEmployee = async (id: string, data: EmployeeRequest): Promise<string> => {
  const response = await api.put(`/employees/${id}`, data);
  return response.data;
};

export const deleteEmployee = async (id: string): Promise<string> => {
  const response = await api.delete(`/employees/${id}`);
  return response.data;
};

//EmployeesDepartments
export const createEmployeesDepartments = async (data: EmployeeDepartmentRequest): Promise<string> => {
  const response = await api.post('/employeesdepartments', data);
  return response.data;
};

export const getEmployeesDepartments = async (): Promise<EmployeeDepartmentResponse[]> => {
  const response = await api.get('/employeesdepartments');
  return response.data;
};

export const updateEmployeesDepartments = async (id: string, data: EmployeeDepartmentRequest): Promise<string> => {
  const response = await api.put(`/employeesdepartments/${id}`, data);
  return response.data;
};

export const deleteEmployeesDepartments = async (employeeId: string, departmentId: string): Promise<string> => {
  const response = await api.delete(`/employeesdepartments/${employeeId}/${departmentId}`);
  return response.data;
};

