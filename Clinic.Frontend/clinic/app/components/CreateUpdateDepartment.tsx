import Modal from "antd/es/modal/Modal";
import { DepartmentRequest } from "../services/departments";
import Input from "antd/es/input/Input";
import { useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";

interface Props {
    mode: Mode;
    value: Department;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: DepartmentRequest) => void;
    handleUpdate: (id: string, request: DepartmentRequest) => void;
}

export enum Mode {
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

    useEffect(() => {
        setName(value.name);
        setDescription(value.description ?? "");
        setAddressId(value.addressId);
    }, [value]);

    const handleOnOk = async () => {
        const departmentRequest: DepartmentRequest = { name, description, addressId };
        if (mode === Mode.Create) {
            handleCreate(departmentRequest);
        } else {
            handleUpdate(value.id, departmentRequest);
        }
    };

    return (
        <Modal
            title={mode === Mode.Create ? "Добавить департамент" : "Редактировать департамент"}
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
                <Input
                    value={addressId}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setAddressId(e.target.value)}
                    placeholder={"Адрес"}
                />
            </div>
        </Modal>
    );
};
