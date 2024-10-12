// utils/api.ts
import { Address } from '@/models/Address';
import { DepartmentRequest } from '@/models/Department';
import axios from 'axios';


const api = axios.create({
  baseURL: 'http://localhost:5000', // Замените на ваш базовый URL
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
