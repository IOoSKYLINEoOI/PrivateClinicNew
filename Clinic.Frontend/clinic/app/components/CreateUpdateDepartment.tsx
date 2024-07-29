import Modal from "antd/es/modal/Modal";
import Input from "antd/es/input/Input";
import { useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";
import { Button } from "antd";
import { CreateUpdateAddress, ModeAddress } from "./CreateUpdateAddress";
import { AddressRequest, createAddress, deleteAddress, updateAddress } from "../services/addresses";
import { DepartmentRequest } from "../services/departments";
import { Address } from "../Models/Address";

interface Department {
    id: string;
    name: string;
    description?: string;
    addressId: string;
}

interface Props {
    mode: ModeDepartment;
    value: Department;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: DepartmentRequest) => void;
    handleUpdate: (id: string, request: DepartmentRequest) => void;
}

export enum ModeDepartment {
    Create,
    Edit,
}

export const CreateUpdateDepartment = ({
    mode,
    value,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const [name, setName] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [addressId, setAddressId] = useState<string>("");

    const defaultValuesAddress: Address = {
        id: "",
        country: "",
        region: "",
        city: "",
        street: "",
        houseNumber: 1,
        apartmentNumber: 1,
        description: "",
        pavilion: ""
    };

    const [valuesAddress, setValuesAddress] = useState<Address>(defaultValuesAddress);
    const [isModalAddressOpen, setIsModalAddressOpen] = useState<boolean>(false);
    const [modeAddress, setModeAddress] = useState<ModeAddress>(ModeAddress.Create);

    useEffect(() => {
        setName(value.name);
        setDescription(value.description ?? "");
        setAddressId(value.addressId);
    }, [value]);

    const handleOnOk = async () => {
        const departmentRequest: DepartmentRequest = { name, description, addressId };
        if (mode === ModeDepartment.Create) {
            handleCreate(departmentRequest);
        } else {
            handleUpdate(value.id, departmentRequest);
        }
    };

    const handleCreateAddress = async (request: AddressRequest) => {
        const newAddressId = await createAddress(request);
        setAddressId(newAddressId);
        closeModalAddress();
        return newAddressId;
    };

    const handleUpdateAddress = async (id: string, request: AddressRequest) => {
        await updateAddress(id, request);
        closeModalAddress();
    };

    const handleDeleteAddress = async (id: string) => {
        await deleteAddress(id);
        closeModalAddress();
    };

    const openModalAddress = () => {
        setModeAddress(ModeAddress.Create);
        setIsModalAddressOpen(true);
    };

    const closeModalAddress = () => {
        setValuesAddress(defaultValuesAddress);
        setIsModalAddressOpen(false);
    };

    const openModalEditAddress = (address: Address) => {
        setModeAddress(ModeAddress.Edit);
        setValuesAddress(address);
        setIsModalAddressOpen(true);
    };

    return (
        <Modal
            title={mode === ModeDepartment.Create ? "Добавить департамент" : "Редактировать департамент"}
            open={isModalOpen}
            cancelText={"Отмена"}
            onOk={handleOnOk}
            onCancel={handleCancel}
        >
            <div className="department_modal">
                <Input
                    value={name}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setName(e.target.value)}
                    placeholder={"Название"}
                />
                <TextArea
                    value={description}
                    onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setDescription(e.target.value)}
                    placeholder={"Описание"}
                />
                <Button
                    type="primary"
                    style={{ marginTop: "30px" }}
                    size="large"
                    onClick={openModalAddress}
                >
                    Добавить
                </Button>
                <CreateUpdateAddress
                    mode={modeAddress}
                    value={valuesAddress}
                    isModalOpen={isModalAddressOpen}
                    handleCreate={handleCreateAddress}
                    handleUpdate={handleUpdateAddress}
                    handleCancel={closeModalAddress}
                />
            </div>
        </Modal>
    );
};
