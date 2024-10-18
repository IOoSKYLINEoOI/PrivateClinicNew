import { Address, AddressRequest } from "./Address";

export interface DepartmentResponse {
    id: string;
    name: string;
    description: string;
    address: Address;
}

export interface DepartmentRequest {
    name: string;
    description: string;
    address: AddressRequest;
}