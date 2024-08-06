"use client";

import Button from "antd/es/button/button";
import { useEffect, useState } from "react";
import Title from "antd/es/typography/Title";
import { createEmployee, deleteEmployee, EmployeeRequest, getAllEmployees, updateEmployee } from "../services/employees";
import { CreateUpdateEmployee, ModeEmployee } from "../components/CreateUpdateEmployee";
import { Employees } from "../components/Employees";

export default function EmployeesPage(){
    const defaultValues = {
        hiringDate: "",
    } as Employee;

    const[values, setValues] = useState<Employee>(defaultValues);

    const [employees, setEmployees] = useState<Employee[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [mode, setMode] = useState(ModeEmployee.Create);

    useEffect(() => {
        const getEmployees = async () =>{
            const employees = await getAllEmployees();
            setLoading(false);
            setEmployees(employees);
        }

        getEmployees();
    }, [])

    const handleCreateEmployee = async (request: EmployeeRequest) =>{
        await createEmployee(request);
        closeModal();

        const employee = await getAllEmployees();
        setEmployees(employees);
    }

    const handleUpdateEmployee = async (id: string, request: EmployeeRequest) =>{
        await updateEmployee(id, request);
        closeModal();

        const employees = await getAllEmployees();
        setEmployees(employees);
    }

    const handleDeleteEmployee = async (id: string) => {
        await deleteEmployee(id);
        closeModal();

        const employees = await getAllEmployees();
        setEmployees(employees);
    }

    const openModal = () => {
        setMode(ModeEmployee.Create);
        setIsModalOpen(true);
    }

    const closeModal = () => {
        setValues(defaultValues);
        setIsModalOpen(false);
    }

    const openModalEdit = (employee: Employee) =>{
        setMode(ModeEmployee.Edit);
        setValues(employee);
        setIsModalOpen(true);
    }

    return(
        <div>
            <Button
                type="primary"
                style={{marginTop: "30px"}}
                size="large"
                onClick = {openModal}
            >Добавить</Button>

            <CreateUpdateEmployee 
                mode={mode} 
                value={values} 
                isModalOpen={isModalOpen} 
                handleCreate={handleCreateEmployee}
                handleUpdate={handleUpdateEmployee}
                handleCancel={closeModal}
            />


            {loading ? <Title>Loading...</Title> : <Employees employees = {employees} handleOpen={openModalEdit} handleDelete={handleDeleteEmployee}/>}
        </div>
    )
}