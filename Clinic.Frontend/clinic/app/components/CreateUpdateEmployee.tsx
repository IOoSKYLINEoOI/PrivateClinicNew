import Modal from "antd/es/modal/Modal";
import Input from "antd/es/input/Input";
import { useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";
import { Button, DatePicker, Form } from "antd";
import { UserOutlined } from "@ant-design/icons";
import { createEmployee, deleteEmployee, EmployeeRequest, updateEmployee } from "../services/employees";

interface Props {
    mode: ModeEmployee;
    //visible: boolean;
    value: Employee;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: EmployeeRequest) => void;
    handleUpdate: (id: string, request: EmployeeRequest) => void;
}

export enum ModeEmployee {
    Create,
    Edit,
}

export const CreateUpdateEmployee = ({
    mode,
    value,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const [hiringDate, setHiringDate] = useState<string>("");
    const [dateOfDismissal, setDateOfDismissal] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [userId, setUserId] = useState<string>("");

    const defaultValuesEmployee: Employee = {
        id: "",
        hiringDate: "",
        userId: ""     
    };

    const [valuesEmployee, setValuesEmployee] = useState<Employee>(defaultValuesEmployee);
    const [isModalEmployeeOpen, setIsModalEmployeeOpen] = useState<boolean>(false);
    const [modeEmployee, setModeEmployee] = useState<ModeEmployee>(ModeEmployee.Create);

    useEffect(() => {
        setHiringDate(value.hiringDate);
        setDateOfDismissal(value.dateOfDismissal ?? "");
        setDescription(value.description ?? "");
        setUserId(value.userId)

    }, [value]);

    const handleOnOk = async () => {
        const employeeRequest: EmployeeRequest = {
            hiringDate,
            dateOfDismissal,
            description,
            userId
        };
        if (mode === ModeEmployee.Create) {
            const newEmployeeId = await handleCreate(employeeRequest);
            console.log(`New address ID: ${newEmployeeId}`);
        } else {
            handleUpdate(value.id, employeeRequest);
        }
    };

    const handleCreateEmployee = async (request: EmployeeRequest) => {
        const newEmployeeId = await createEmployee(request);
        closeModalEmployee();
        return newEmployeeId;
    };

    const handleUpdateEmployee = async (id: string, request: EmployeeRequest) => {
        await updateEmployee(id, request);
        closeModalEmployee();
    };

    const handleDeleteEmployee = async (id: string) => {
        await deleteEmployee(id);
        closeModalEmployee();
    };

    const openModalEmployee = () => {
        setModeEmployee(ModeEmployee.Create);
        setIsModalEmployeeOpen(true);
    };

    const closeModalEmployee = () => {
        setValuesEmployee(defaultValuesEmployee);
        setIsModalEmployeeOpen(false);
    };

    const openModalEditEmployee = (employee: Employee) => {
        setModeEmployee(ModeEmployee.Edit);
        setValuesEmployee(employee);
        setIsModalEmployeeOpen(true);
    };


return (
    <Modal
      title={mode === ModeEmployee.Create ? "Добавить врача" : "Редактировать врача"}
      open={isModalOpen}
      onOk={handleOnOk}
      onCancel={handleCancel}
      footer={[
        <Button key="back" onClick={handleCancel}>
          Отмена
        </Button>,
        <Button key="submit" type="primary" onClick={handleOnOk}>
          Добавить
        </Button>,
      ]}
    >
        <Form.Item
          name="hiringDate"
          label="Дата назначения"
          rules={[{ required: true, message: "Пожалуйста, выберите дату назначения!" }]}
        >
          <DatePicker style={{ width: "100%" }} placeholder="Дата назначения" />
        </Form.Item>

        <Form.Item
          name="dateOfDismissal"
          label="Дата назначения"
          rules={[{ required: true, message: "Пожалуйста, выберите дату увольнения!" }]}
        >
          <DatePicker style={{ width: "100%" }} placeholder="Дата назначения" />
        </Form.Item>

        <Form.Item
          name="description"
          label="Подробности"
          rules={[{ required: false, message: "Подробности" }]}
        >
          <Input prefix={<UserOutlined />} placeholder="Подробности" />
        </Form.Item>
    </Modal>
  );
};