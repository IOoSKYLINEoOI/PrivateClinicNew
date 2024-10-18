// utils/api.ts
import axios from 'axios';
import { Address } from '@/models/Address';
import { DepartmentRequest } from '@/models/Department';
import { RegisterUserRequest, LoginUserRequest, UserUpdateRequest } from '@/models/User';

const api = axios.create({
  baseURL: 'https://localhost:7179', // Замените на ваш базовый URL
  withCredentials: true, // Чтобы отправлять JWT из куки
});

// Типы ответов
interface DepartmentResponse {
  id: string;
  name: string;
  description: string;
  addressId: string;
  address: Address;
}

// Получение всех департаментов
export const getDepartments = async (): Promise<DepartmentResponse[]> => {
  const response = await api.get('/departments');
  return response.data;
};

// Создание департамента
export const createDepartment = async (data: DepartmentRequest): Promise<string> => {
  const response = await api.post('/departments', data);
  return response.data;
};

// Обновление департамента
export const updateDepartment = async (id: string, data: DepartmentRequest): Promise<string> => {
  const response = await api.put(`/departments/${id}`, data);
  return response.data;
};

// Удаление департамента
export const deleteDepartment = async (id: string): Promise<string> => {
  const response = await api.delete(`/departments/${id}`);
  return response.data;
};

// ** Пользовательские запросы **

// Регистрация пользователя
export const registerUser = async (data: RegisterUserRequest): Promise<void> => {
  await api.post('/users/register', data);
};

// Вход пользователя
export const loginUser = async (data: LoginUserRequest): Promise<void> => {
  await api.post('/users/login', data);
};

// Обновление профиля пользователя
export const updateUserProfile = async (id: string, data: UserUpdateRequest): Promise<void> => {
  await api.post(`/users/updateProfile?id=${id}`, data);
};
