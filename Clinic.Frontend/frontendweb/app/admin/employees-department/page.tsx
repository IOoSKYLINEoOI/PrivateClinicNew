'use client';
import React, { useEffect, useState } from 'react';
import { Table, Button, message, Input } from 'antd';
import { getEmployeesDepartments, deleteEmployeesDepartments } from '@/utils/api';
import { EmployeeDepartmentResponse } from '@/models/EmployeeDepartment';
import styles from './page.module.css';
import DepartmentEmployeeFormModal from '@/components/employeesDepartment/DepartmentEmployeeFormModal';

const DepartmentEmployeeList: React.FC = () => {
  const [data, setData] = useState<EmployeeDepartmentResponse[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalVisible, setModalVisible] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState<EmployeeDepartmentResponse | null>(null);
  const [searchId, setSearchId] = useState<string>('');

  const fetchEmployeesDepartments = async () => {
    setLoading(true);
    try {
      const result = await getEmployeesDepartments();
      setData(result);
    } catch (error) {
      message.error('Не удалось загрузить данные.');
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchEmployeesDepartments();
  }, []);

  const handleSearch = () => {
    if (searchId) {
      const filteredData = data.filter((item) => item.EmployeeId === searchId || item.DepartmentId === searchId);
      setData(filteredData);
    } else {
      fetchEmployeesDepartments();
    }
  };

  const handleAdd = () => {
    setSelectedEmployee(null);
    setModalVisible(true);
  };

  const handleEdit = (record: EmployeeDepartmentResponse) => {
    setSelectedEmployee(record);
    setModalVisible(true);
  };

  const handleDelete = async (employeeId: string, departmentId: string) => {
    try {
      await deleteEmployeesDepartments(employeeId, departmentId);
      message.success('Связь сотрудника с департаментом удалена');
      fetchEmployeesDepartments();
    } catch {
      message.error('Не удалось удалить связь');
    }
  };

  const columns = [
    { title: 'ID Сотрудника', dataIndex: 'EmployeeId', key: 'employeeId' },
    { title: 'ID Департамента', dataIndex: 'DepartmentId', key: 'departmentId' },
    { title: 'Должность', dataIndex: 'PositionId', key: 'positionId' },
    { title: 'Описание', dataIndex: 'Description', key: 'description' },
    {
      title: 'Действия',
      key: 'actions',
      render: (_: any, record: EmployeeDepartmentResponse) => (
        <div className={styles.actions}>
          <Button onClick={() => handleEdit(record)}>Редактировать</Button>
          <Button onClick={() => handleDelete(record.EmployeeId, record.DepartmentId)} danger>Удалить</Button>
        </div>
      ),
    },
  ];

  return (
    <div className={styles.container}>
      <div className={styles.searchSection}>
        <Input
          placeholder="Поиск по ID"
          value={searchId}
          onChange={(e) => setSearchId(e.target.value)}
        />
        <Button onClick={handleSearch}>Поиск</Button>
      </div>
      <Button type="primary" onClick={handleAdd} className={styles.addButton}>Добавить связь</Button>
      <Table
        dataSource={data}
        columns={columns}
        rowKey={(record) => `${record.EmployeeId}-${record.DepartmentId}`}
        loading={loading}
      />
      <DepartmentEmployeeFormModal
        visible={modalVisible}
        onClose={() => setModalVisible(false)}
        onSave={fetchEmployeesDepartments}
        employeeDepartment={selectedEmployee}
      />
    </div>
  );
};

export default DepartmentEmployeeList;
