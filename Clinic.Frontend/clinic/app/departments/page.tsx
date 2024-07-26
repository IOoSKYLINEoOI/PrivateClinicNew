"use client";

import Button from "antd/es/button/button";
import { Departments } from "../components/Departments";
import { useEffect, useState } from "react";
import { createDepartment, deleteDepartment, DepartmentRequest, getAllDepartments, updateDepartment } from "../services/departments";
import Title from "antd/es/typography/Title";
import { CreateUpdateDepartment, Mode } from "../components/CreateUpdateDepartment";

export default function DepartmentsPage(){
    const defaultValues = {
        name: "",
        description: "",
        addressId: "",
    } as Department;

    const[values, setValues] = useState<Department>(defaultValues);

    const [departments, setDepartments] = useState<Department[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [mode, setMode] = useState(Mode.Create);

    useEffect(() => {
        const getDepartments = async () =>{
            const departments = await getAllDepartments();
            setLoading(false);
            setDepartments(departments);
        }

        getDepartments();
    }, [])

    const handleCreateDepartment = async (request: DepartmentRequest) =>{
        await createDepartment(request);
        closeModal();

        const departments = await getAllDepartments();
        setDepartments(departments);
    }

    const handleUpdateDepartment = async (id: string, request: DepartmentRequest) =>{
        await updateDepartment(id, request);
        closeModal();

        const departments = await getAllDepartments();
        setDepartments(departments);
    }

    const handleDeleteDepartment = async (id: string) => {
        await deleteDepartment(id);
        closeModal();

        const departments = await getAllDepartments();
        setDepartments(departments);
    }

    const openModal = () => {
        setMode(Mode.Create);
        setIsModalOpen(true);
    }

    const closeModal = () => {
        setValues(defaultValues);
        setIsModalOpen(false);
    }

    const openModalEdit = (department: Department) =>{
        setMode(Mode.Edit);
        setValues(department);
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

            <CreateUpdateDepartment 
                mode={mode} 
                value={values} 
                isModalOpen={isModalOpen} 
                handleCreate={handleCreateDepartment}
                handleUpdate={handleUpdateDepartment}
                handleCancel={closeModal}
            />


            {loading ? <Title>Loading...</Title> : <Departments departments = {departments} handleOpen={openModalEdit} handleDelete={handleDeleteDepartment}/>}
        </div>
    )
}