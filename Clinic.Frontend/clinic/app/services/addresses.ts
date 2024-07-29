export interface AddressRequest{
    country: string;
    region: string;
    city: string;
    street: string;
    houseNumber: number;
    apartmentNumber: number;
    description?: string;
    pavilion?: string;
}

export const getIdAddress = async (id: string) => {
    const addressId = await fetch(`https://localhost:7179/addresses/${id}`);

    return addressId.json();
};

export const createAddress = async (addressRequest: AddressRequest): Promise<string> => {
    const response = await fetch("https://localhost:7179/addresses", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(addressRequest),
    });

    if (!response.ok) {
        throw new Error('Failed to create address');
    }

    const data = await response.json();
    return data.id; 
};

export const updateAddress = async (id: string, addressRequest: AddressRequest) =>{
    await fetch(`https://localhost:7179/addresses/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "aplication/json",
        },
        body: JSON.stringify(addressRequest),
    });
};

export const deleteAddress = async (id: string) =>{
    await fetch(`https://localhost:7179/addresses/${id}`, {
        method: "DELETE",
    });
};