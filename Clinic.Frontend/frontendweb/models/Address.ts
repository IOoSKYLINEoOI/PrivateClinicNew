export interface Address {
    id: string;
    country: string;
    region: string;
    city: string;
    street: string;
    houseNumber: number;
    apartmentNumber: number;
    description?: string;
    pavilion?: string;
}

export interface AddressRequest {
    country: string;
    region: string;
    city: string;
    street: string;
    houseNumber: number;
    apartmentNumber: number;
    description?: string;
    pavilion?: string;
  }