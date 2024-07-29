import Modal from "antd/es/modal/Modal";
import Input from "antd/es/input/Input";
import { useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";
import InputNumber from "antd/es/input-number";
import { AddressRequest } from "../services/addresses";
import { Address } from "../Models/Address";

interface Props {
    mode: ModeAddress;
    value: Address;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: AddressRequest) => Promise<string>;
    handleUpdate: (id: string, request: AddressRequest) => void;
}

export enum ModeAddress {
    Create,
    Edit,
}

export const CreateUpdateAddress = ({
    mode,
    value,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const [country, setCountry] = useState<string>("");
    const [region, setRegion] = useState<string>("");
    const [city, setCity] = useState<string>("");
    const [street, setStreet] = useState<string>("");
    const [houseNumber, setHouseNumber] = useState<number>(1);
    const [apartmentNumber, setApartmentNumber] = useState<number>(1);
    const [description, setDescription] = useState<string>("");
    const [pavilion, setPavilion] = useState<string>("");

    useEffect(() => {
        setCountry(value.country);
        setRegion(value.region);
        setCity(value.city);
        setStreet(value.street);
        setHouseNumber(value.houseNumber);
        setApartmentNumber(value.apartmentNumber);
        setDescription(value.description ?? "");
        setPavilion(value.pavilion ?? "");
    }, [value]);

    const handleOnOk = async () => {
        const addressRequest: AddressRequest = {
            country,
            region,
            city,
            street,
            houseNumber,
            apartmentNumber,
            description,
            pavilion
        };
        if (mode === ModeAddress.Create) {
            const newAddressId = await handleCreate(addressRequest);
            console.log(`New address ID: ${newAddressId}`);
        } else {
            handleUpdate(value.id, addressRequest);
        }
    };

    return (
        <Modal
            title={mode === ModeAddress.Create ? "Добавить адрес" : "Изменить адрес"}
            open={isModalOpen}
            cancelText={"Отмена"}
            onOk={handleOnOk}
            onCancel={handleCancel}
        >
            <div className="address_modal">
                <Input
                    value={country}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setCountry(e.target.value)}
                    placeholder={"Страна"}
                />
                <Input
                    value={region}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setRegion(e.target.value)}
                    placeholder={"Регион"}
                />
                <Input
                    value={city}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setCity(e.target.value)}
                    placeholder={"Город"}
                />
                <Input
                    value={street}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setStreet(e.target.value)}
                    placeholder={"Улица"}
                />
                <InputNumber
                    value={houseNumber}
                    onChange={(value: number) => setHouseNumber(value)}
                    placeholder={"Номер дома"}
                />
                <InputNumber
                    value={apartmentNumber}
                    onChange={(value: number) => setApartmentNumber(value)}
                    placeholder={"Номер квартиры"}
                />
                <TextArea
                    value={description}
                    onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setDescription(e.target.value)}
                    placeholder={"Описание"}
                />
                <Input
                    value={pavilion}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPavilion(e.target.value)}
                    placeholder={"Павильон"}
                />
            </div>
        </Modal>
    );
};
